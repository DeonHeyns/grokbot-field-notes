# ChatGPT role catalogue

69 role contracts adapted from the original field notes. Choose a responsibility,
then use its **complete ChatGPT prompt**. A role card is not a running agent.

## Use a role

1. Open its generated prompt using the link in the role card.
2. Copy the whole instruction block, including sources and boundaries.
3. Fill placeholders, supply current evidence and verify the required tools.
4. Run one bounded task and inspect the result before saving recurring work.

The role card contains scope, source of truth, approval rules, intended triggers,
outputs and a core assignment. The generated prompt adds the shared runtime
instructions. Named collaborators use actual delegation or explicit handoffs.
Schedules and event subscriptions need a real saved configuration.

[Setup](../ports/chatgpt/README.md) · [Handoffs](../ports/chatgpt/HANDOFF.md) ·
[Product mapping](../reference/PRODUCT.md) · [Original attribution](../PROVENANCE.md)

## Sales & sales engineering

| Role | Responsibility |
|---|---|
| [Account Specialist (one per account)](account-specialist.md) | Maintains one account's current plan from authorized records and flags risks, opportunities and outstanding commitments. |
| [Battle Card Writer](battle-card-writer.md) | Combines the competitor agent's hands-on findings with the technical expert's codebase truth into short, SE-ready battle cards: competitor claim vs. our reality. |
| [Call Prep / Close](call-prep.md) | 15-20 minutes before a call (or the night before), hands you who's on it, what happened last time, their usage, what shipped since, a suggestion, and any bug on their site to open with. |
| [Case-Study / Slides Curator](case-study-curator.md) | Turns a customer blog post or call notes into a slide in your fixed template, fetches the right logo, inserts it into the master deck, and hides what's irrelevant for the next call. |
| [Competitive Intel](competitive-intel.md) | Compares current public competitor evidence and authorized product observations with a verified internal baseline. |
| [CRM Updater (next steps)](crm-updater.md) | Listens to the call, reads the thread, and writes the next-steps update in your exact format for you to approve and push; and reacts to stage changes. |
| [Demo Scripter / Talk Track](demo-scripter.md) | Builds no-hallucination demo scripts and call talk tracks that map a specific customer's pain to the live product flow, grounded in the technical expert. |
| [Enrichment & Company Research](enrichment.md) | Enriches approved business contacts and company records, labeling verification status and source limits. |
| [ICP Researcher](icp-researcher.md) | Builds a documented ideal-customer hypothesis from deal, customer and product evidence. |
| [Live Deck Curator](live-deck-curator.md) | After (or during) a discovery call, pulls the transcript and updates the deck with the use cases and next steps the customer actually said. |
| [Prospector (outbound)](prospector.md) | Picks accounts and contacts, finds personal hooks (X posts, podcasts, webinars; watched, not skimmed), ranks who to reach out to, and drafts the outreach in your voice. |
| [Signal Scanner (web research)](signal-scanner.md) | Finds new external signals for a bounded account list and returns deduplicated, dated evidence. |
| [Technical Expert (repo-grounded)](technical-expert.md) | Answers "how does the product actually do X" from the codebase, via Codex tasks, and rephrases it for a customer; without leaking IP. |
| [Usage Signals (PLG)](usage-signals.md) | Reads product usage to find who signed up, who the power users are, which teams adopted what, and which accounts are warm right now. |
| [Voice of Customer](voice-of-customer.md) | Holds why deals were won and lost; from call recordings and the CRM; so outreach and ranking can be tailored to what similar customers actually cared about. |
| [Voice (writes as you)](voice.md) | Learns how you actually write from what you actually sent; filtered, weighted to recent and successful; and drafts everything external in that voice, per audience. |

## Operations, events & finance

| Role | Responsibility |
|---|---|
| [Bookkeeper / CFO](bookkeeper.md) | Reconciles supplied receipts and read-only account data into a working ledger and exception report, without moving money. |
| [Contract / Policy Reviewer](contract-reviewer.md) | First-pass review of a venue contract or policy: flags unusual terms, missing protections and market-rate outliers; explicitly a draft for a human expert to finish. |
| [Event Planner](event-planner.md) | Owns a production budget for an event; venue, F&B, staffing, AV, marketing as a separate line; and sends the sub-agents (venue, permits, contracts) their parameters. |
| [Negotiator / Reseller](negotiator.md) | Lists items or requests quotes, and negotiates with counterparties inside a framework you set; floor price, walk-away rules; reporting back for anything outside it. |
| [Permit / Red-Tape Researcher](permit-researcher.md) | Researches the permits, licences and regulations for an event type in a specific jurisdiction, and produces a checklist with lead times. |
| [Venue Scout](venue-scout.md) | Finds venues that meet the event's criteria, sends RFPs by email, and negotiates within the budget you set. |

## Orchestration

| Role | Responsibility |
|---|---|
| [Agent Setup Designer](bot-factory.md) | Turns a recurring job into a practical role, context requirements and a tested workflow setup. |
| [Chief of Staff](chief-of-staff.md) | Coordinates a small set of responsibilities, maintains a task ledger and returns one evidence-based operating brief. |
| [Engineering Manager](engineering-manager.md) | Decomposes an approved specification, coordinates implementation and verifies integrated results. |
| [Knowledge Base Manager](knowledge-base-manager.md) | Proposes durable knowledge-base updates from supplied records and authorized sources, with explicit provenance. |
| [General Task Assistant](misc-bot.md) | Handles one-off questions and research without creating unnecessary persistent roles. |
| [Playbook Owner (head of operations)](playbook-owner.md) | Maintains versioned operating standards and prepares updates that future tasks can explicitly load. |
| [Project Manager](project-manager.md) | Coordinates one workflow from a supplied brief to a checked deliverable, using explicit handoffs and a current task board. |
| [Self-Improvement Scan (agent optimiser)](self-improvement-scan.md) | Audits how the human and the agents actually worked this week, proposes one automation, and feeds the draft-vs-sent delta back into the voice agent. |
| [Source of Truth](source-of-truth.md) | Answers questions from the canonical documentation with sources attached. Other agents ground their claims in it; the chief goes to it when an answer must be right. |
| [Batch Worker](sub-agent-army.md) | Processes one bounded slice of a batch and returns structured results to its coordinator. |

## Engineering

| Role | Responsibility |
|---|---|
| [CI / Alert Auto-Fix (on-call agent)](ci-autofix.md) | Investigates a real CI or alert event, prepares a scoped fix and escalates according to the service's incident policy. |
| [Comment Cleanup](comment-cleanup.md) | Deletes unnecessary code comments. Exists because agents use comments as a crutch for workarounds instead of fixing root causes. |
| [Domain Engineer (UI / DevX / infra / …)](domain-engineer.md) | Implements a scoped repository change with current instructions, tests and reviewable evidence. |
| [Feedback → PR](feedback-to-pr.md) | Takes a confirmed piece of customer feedback and turns it into a PR, using the real product for context and verification, within hours. |
| [Founding Engineer (PR watcher)](founding-engineer.md) | Owns a small repository's implementation and PR readiness, with explicit merge and release boundaries. |
| [Kanban / Task-Board Updater](kanban-updater.md) | Keeps the task board true: moves cards when PRs land, creates cards from decisions, and lets other agents pick up work by watching the board. |
| [Nightly Audit Engineer](nightly-audit-engineer.md) | Runs a bounded repository audit when invoked and prepares focused cleanup proposals or PRs. |
| [Playtester / QA](playtester.md) | Actually uses the product; clicks through it, plays it, breaks it on purpose; using available browser or test tools, and reports what's wrong before a PR merges or after a deploy. |
| [PR Reviewer](pr-reviewer.md) | Reviews the current PR head against requirements, code and evidence, reporting actionable findings. |
| [Prototyper](prototyper.md) | Builds throwaway prototypes fast; inline HTML in chat, or a Codex task on a scratch branch; to answer a design question, not to ship. |
| [Slack Mention Responder](slack-mention-responder.md) | Processes explicitly configured Slack events using a bounded action map and verified sender authority. |
| [Triage Validator](triage-validator.md) | Checks that the triage agent's understanding of a piece of feedback is correct before any autopilot fix proceeds. A second pair of eyes between users and code. |
| [Triage](triage.md) | Reads incoming feedback, reproduces the reported issue, and files a confirmed ticket; or discards it. Watches for prompt injection in the feedback. |

## Post-sales & personal ops

| Role | Responsibility |
|---|---|
| [Commitment Tracker (promise keeper + ask watch)](commitment-tracker.md) | Two lists: what you said you'd do, and what you asked others for. Reminds you of the first and chases the second, so nothing falls into the abyss of email and Slack. |
| [Daily Digest](daily-digest.md) | Produces a concise, source-linked briefing from specified feeds, with coverage gaps and relevance made explicit. |
| [Follow-Up Desk](follow-up-desk.md) | Turns a supplied customer-call transcript into a follow-up pack of drafts and promised materials. |
| [Inbox Manager](inbox-manager.md) | Ranks overnight email, Slack and meeting invites into an action order every morning, drafts replies to the routine ones, and only escalates what matters. |
| [Internal Radar](internal-radar.md) | Watches the 30-40 internal channels and update emails you can't, and sends one daily digest of what's new, what you need to know, with links. |
| [Meeting Notes and Actions](meeting-attendee.md) | Turns an authorized meeting transcript or recording into decisions, commitments and follow-up drafts. |

## Product & design

| Role | Responsibility |
|---|---|
| [Creative Director / Media Explorer](creative-director.md) | Explores creative directions; music, motion, ad assets; as code where possible, and drops candidates somewhere the team can react (Notion, a playground). |
| [Critic](critic.md) | Reviews a piece of work against a rubric and says what's wrong, in plain English, before it ships. Feedback only; never edits the thing. |
| [Data Scientist / Analyst](data-scientist.md) | Answers data questions in plain English by writing and running the SQL against the warehouse, returns numbers and charts, and corrects the humans when they misread a chart. |
| [Designer](designer.md) | Produces on-brand mocks fast because it carries the design system, reference files and the team's accumulated no-no's. Delivers options, not one answer. |
| [Prioritizer](prioritizer.md) | Keeps a stack-ranked list of what to do next, scored by impact and effort, and re-ranks as new ideas and feedback arrive. |
| [Product Changes Tracker](product-changes-tracker.md) | Tells you what shipped, what was unshipped, and which implicit decisions got made, by reading PRs and issues *and* walking the live product using available browser or test tools. |
| [Recruiter](recruiter.md) | Sources candidates, manages the hiring pipeline, and drafts outreach; or, flipped around, finds the recruiters and warm contacts for a job seeker. |
| [Spec / PRD Writer](spec-writer.md) | Turns an insight plus product context into a crisp P0/P1/P2 spec optimised for getting to code fast, and iterates from comments in the doc. |

## Marketing & growth

| Role | Responsibility |
|---|---|
| [Growth Ideas Logger](growth-ideas-logger.md) | Captures growth ideas as they're said aloud, logs them to a growth playbook doc, and stack-ranks them by impact and effort. |
| [Market Researcher](market-researcher.md) | Studies your product and market, finds competitors' marketing sites, and names the positioning gaps and opportunities; the first agent in a campaign chain. |
| [Marketing Analyst](marketing-analyst.md) | Pulls the results of the last experiment from the ads platform, names the winner and the key metrics, and recommends how to update strategy and assets. |
| [Performance Marketer](performance-marketer.md) | Builds campaign shells in the ads platform, traffics the copy variants into them, and sends screenshots as it clicks; stopping short of spend without you. |
| [Product Marketer](product-marketer.md) | Takes the research and writes the positioning brief; one-liners, packaging, value statements; then the landing-page outline and ad-copy variants, iterating from your comments in the doc. |
| [Website Ops](website-ops.md) | Implements an approved marketing page and returns a checked preview or PR, with release handled separately. |

## Customer support

| Role | Responsibility |
|---|---|
| [Support Alert](support-alert.md) | Pinged by the reply agent (or on its own hourly scan) when a ticket matches an escalation rule; posts to a shared Slack channel and tags the human. |
| [Support Infra (build)](support-infra.md) | Designs and verifies support integrations, evaluation cases and action logs, implementing approved changes as code or configuration. |
| [Support Reply](support-reply.md) | Prepares source-backed support responses, escalating missing policy coverage and separating replies from account actions. |
| [Support Tuner (self-improvement)](support-tuner.md) | Proposes KB additions when the reply agent can't answer, reviews last week's tickets and traces for what could have gone better, and; with approval; makes the change. |
