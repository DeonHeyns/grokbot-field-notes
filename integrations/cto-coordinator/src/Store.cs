using System.Text.Json;
using Microsoft.Data.Sqlite;
namespace CtoCoordinator;

public sealed class Store
{
    private readonly string connectionString;
    public Store(Settings settings)
    {
        var path = Path.GetFullPath(settings.DatabasePath);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        connectionString = new SqliteConnectionStringBuilder { DataSource = path, DefaultTimeout = 10 }.ToString();
        using var db = Open();
        using var cmd = db.CreateCommand();
        cmd.CommandText = """
            PRAGMA journal_mode=WAL;
            CREATE TABLE IF NOT EXISTS runs (id TEXT PRIMARY KEY, body TEXT NOT NULL);
            CREATE TABLE IF NOT EXISTS reports (id TEXT PRIMARY KEY, body TEXT NOT NULL);
            """;
        cmd.ExecuteNonQuery();
    }
    private SqliteConnection Open() { var db = new SqliteConnection(connectionString); db.Open(); return db; }
    public Run? GetRun(string id) => Read<Run>("runs", id);
    public Report? GetReport(string id) => Read<Report>("reports", id);
    private T? Read<T>(string table, string id)
    {
        using var db = Open(); using var cmd = db.CreateCommand();
        cmd.CommandText = $"SELECT body FROM {table} WHERE id=$id"; cmd.Parameters.AddWithValue("$id", id);
        var json = cmd.ExecuteScalar() as string;
        return json == null ? default : JsonSerializer.Deserialize<T>(json);
    }
    public IReadOnlyList<Run> ListRuns(string cycleId)
    {
        using var db = Open(); using var cmd = db.CreateCommand();
        cmd.CommandText = "SELECT body FROM runs WHERE json_extract(body, '$.CycleId')=$cycle ORDER BY json_extract(body, '$.CreatedAt')";
        cmd.Parameters.AddWithValue("$cycle", cycleId);
        using var reader = cmd.ExecuteReader(); var results = new List<Run>();
        while (reader.Read()) results.Add(JsonSerializer.Deserialize<Run>(reader.GetString(0))!);
        return results;
    }
    public void SaveRun(Run run)
    {
        using var db = Open(); using var cmd = db.CreateCommand();
        cmd.CommandText = "INSERT INTO runs VALUES ($id,$body) ON CONFLICT(id) DO UPDATE SET body=$body";
        cmd.Parameters.AddWithValue("$id", run.RequestId); cmd.Parameters.AddWithValue("$body", JsonSerializer.Serialize(run));
        cmd.ExecuteNonQuery();
    }
    public Report SaveReport(Report report)
    {
        using var db = Open(); using var tx = db.BeginTransaction(); using var cmd = db.CreateCommand();
        cmd.Transaction = tx;
        cmd.CommandText = "INSERT INTO reports VALUES ($id,$body) ON CONFLICT(id) DO NOTHING";
        cmd.Parameters.AddWithValue("$id", report.RequestId); cmd.Parameters.AddWithValue("$body", JsonSerializer.Serialize(report));
        cmd.ExecuteNonQuery();
        cmd.CommandText = "SELECT body FROM reports WHERE id=$id";
        var saved = JsonSerializer.Deserialize<Report>((string)cmd.ExecuteScalar()!)!;
        if (saved.Markdown != report.Markdown || saved.Outcome != report.Outcome || saved.AsOf != report.AsOf)
            throw new CoordinationException("A different report already exists. Reports are immutable; request a follow-up run.");
        tx.Commit(); return saved;
    }
}
