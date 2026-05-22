# 🤖 AI-Assisted Development — Hearth & Hex

> **Important:** AI is a **development tool**, not a runtime feature.
> No part of the shipping game calls an LLM at runtime. Every villager line,
> every Granny's-journal narration, every festival crowd murmur is hand-authored
> into `DialogueNodeSO` / `LineBankSO` ScriptableObjects.
> Claude (via Claude Code and Claude Agents) is used by the studio to speed up
> design, code, asset wiring, dialogue writing, and QA — never by the player.

## 1. What the studio uses Claude for

| Phase | What Claude does | What humans do |
|---|---|---|
| Concept & trend research | Pulls cozy-genre signals from r/CozyGamers, r/gamedev, Steam trend feeds | Final greenlight |
| GDD authoring | Drafts scene-by-scene Mission 1–6 breakdowns, NPC voice guides, balance tables | Pillar checks, fun verdict |
| Code generation | Produces ScriptableObject definitions, gameplay loops (FarmPlot, HearthController, TimeOfDay), UI controllers, save layer, pooling | Senior dev review + Unity Editor wiring |
| Refactoring | Removes anti-patterns, applies pooling, tightens GC allocations | Approve diff |
| Asset wiring instructions | Click-by-click Unity Editor steps for Harvest Garden / BoZo Characters / Bamao GUI import + prefab construction | Buys assets, drags them into project |
| Dialogue writing | Drafts 30–60 hand-authored lines per villager per node tree (Elra, Benn, Rosa, Tilda, Marek, Iva, Hara) | Writers polish, voice direction |
| QA & playtesting scripts | Authors checklists, edge-case bug repros | Manual play, fix bugs |

## 2. Why we removed runtime LLM features (v0.1 → v0.2)

The original v0.1 design plugged Claude into the running game (live villager chat). v0.2 retires this for clear reasons:

| Concern | v0.1 (runtime LLM villagers) | v0.2 (hand-authored DialogueNodeSO) |
|---|---|---|
| Player experience consistency | LLM may go off-tone, drift accent, modern slang slip | 100% authored lines, tuned by cozy-game writers |
| Internet dependency | Phones home every E-press | Fully offline |
| Per-DAU API cost | ~$0.007 per DAU | $0 |
| Steam policy disclosure | Required, plus risk of restriction | None — game is fully self-contained |
| Latency | 600–2,000 ms per villager reply | Instant |
| Determinism for QA | Hard to repro AI quirks | Deterministic; bugs are reproducible |
| Privacy | Player typed input processed off-device | No player input leaves device |
| Accessibility | Typing dependency was a barrier | Branching reply buttons (and a free-walk close) |

## 3. How Claude shows up in the dev workflow

1. **Plan** — design personas draft GDDs in the studio chat; Claude expands them into scene-by-scene breakdowns (see `docs/02`).
2. **Code** — Claude Code generates Unity C# (ScriptableObjects, services, controllers) directly into branch PRs (see `Assets/_Project/Scripts/`).
3. **Review** — Critic & Review Board (Claude personas) audit each diff before merge (see `docs/06`).
4. **Wire** — Claude writes the exact Unity Editor click-by-click steps the human follows after asset import (see `docs/07`).
5. **Author content** — Writers (human + Claude pair) draft DialogueNodeSO / LineBankSO content directly in YAML-like ScriptableObject inspectors.
6. **Test** — Claude drafts QA checklists per mission; humans execute.
7. **Ship** — CI builds the Unity project; only the compiled game ships — never the LLM, never a network proxy.

## 4. The shipping game's dialogue stack

| Type | When | Author tool |
|---|---|---|
| `DialogueNodeSO` | Villager conversations (E to talk) | Inspector edit + Claude drafts lines |
| `LineBankSO` | Granny's journal narrations, festival crowd, hearth ambient lines | Same |
| `IScriptedDialogueService` | The runtime entry point (registered in `GameBootstrap`) | (code only) |

## 5. Example: authoring Elra's opening node with Claude in the loop

1. Writer asks Claude: *"Draft the root dialogue node for Elra, our cozy innkeeper. M1 first-meeting. 4 reply options. One reply should mention the harvest moon and report objective `m1_meet_elra`. No modern words."*
2. Claude returns YAML-like fields ready to paste into the ScriptableObject inspector.
3. Writer pastes into `Node_Elra_Root.asset`, hand-edits the warmth.
4. Human plays, iterates. **No proxy, no API key, no token cost.**

## 6. What replaced what

| v0.1 (deleted) | v0.2 (replacement) |
|---|---|
| `Persona_<Name>.asset` (systemPrompt + memory toggle) | `Node_<Name>_Root.asset` tree of `DialogueNodeSO`s |
| `AICopilotPersonaSO.cs` | (deleted — no replacement needed) |
| `ClaudeCopilotService.cs` | `ScriptedDialogueService.cs` |
| `server/copilot-proxy/` (Node + Anthropic) | (deleted — no replacement needed) |
| Dialogue System OpenAI Addon (Asset Store, $45) | Not needed |

## 7. What the user must do after cloning

1. Open the Unity project per `docs/07_UNITY_SETUP_GUIDE.md`.
2. Buy & import asset packs listed in `docs/03_ASSET_PLAN.md`.
3. Drag prefabs / wire scenes per `docs/07`.
4. **No proxy server, no API key, no internet config required.**

## 8. Ongoing AI-in-dev usage

The repo will continue to receive Claude-generated PRs for:
- New missions (data-only — adding scenes + ScriptableObjects)
- Bug fixes & tuning passes
- Localisation drafts
- Marketing / Steam-page copy

Every PR is reviewed by the Critic & Review Board persona panel before merge.
