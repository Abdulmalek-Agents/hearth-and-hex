# Changelog — Hearth & Hex

## [v0.2.1] — Unity 6 LTS target

### Changed
- Engine target bumped from Unity 2022.3 LTS → **Unity 6 LTS (6000.4.4f1)**.
- URP from 14.x → 17.x.
- Cinemachine references updated: the old `CinemachineFreeLook` is replaced by `CinemachineCamera + CinemachineOrbitalFollow` (Cinemachine 3.x).
- `docs/04_TECHNICAL_ARCHITECTURE.md` §1 + new §7 "Unity 6 compatibility notes".
- `docs/07_UNITY_SETUP_GUIDE.md` Step 1, Step 4 import note, Step 7 camera note, Troubleshooting.
- `README.md` Engine row.

No gameplay code changes — v0.2 C# is fully forward-compatible with Unity 6.

---

## [v0.2-mission1-no-runtime-ai] — Removed runtime LLM dependencies

### Changed
- **Critic & Review Board re-review (Cycle 4):** AI is now a development-workflow tool
  (Claude Code / Claude Agents), not a runtime gameplay feature. The shipping game no
  longer calls any LLM at runtime, contains no Anthropic API key, requires no proxy
  server, and is fully offline-playable.
- Replaced `ClaudeCopilotService` and `AICopilotPersonaSO` with `ScriptedDialogueService`,
  `DialogueNodeSO`, and `LineBankSO` (all hand-authored ScriptableObjects).
- `GameBootstrap.cs` now registers `IScriptedDialogueService` instead of `IAICopilotService`.
- `VillagerNpc` + `DialogueUI` rewritten to render branching `DialogueNodeSO` trees with
  hand-authored player reply buttons (no text input field).
- README/AI co-pilot row replaced with "AI in development" pointer to docs/05.
- `docs/05_AI_COPILOT_INTEGRATION.md` removed; replaced with `docs/05_AI_ASSISTED_DEVELOPMENT.md`.
- `docs/03_ASSET_PLAN.md` dropped "Dialogue System OpenAI Addon" line.
- `docs/07_UNITY_SETUP_GUIDE.md` dropped Node proxy + Anthropic API key steps.
- `docs/06_CRITIC_REVIEW_CYCLES.md` appended Cycle 4 verdict.

### Removed
- `Assets/_Project/Scripts/AI/AICopilotPersonaSO.cs`
- `Assets/_Project/Scripts/AI/ClaudeCopilotService.cs`
- `server/copilot-proxy/` (Node proxy, package.json, env example, README)

### Rationale
The studio's original "AI integration" requirement was about using AI **in development**
(Claude Code, Claude Agents) to boost productivity — not about shipping LLM features
inside the game. This release aligns the project with that intent.

---

## [v0.1-mission1-skeleton] — Initial scaffolding

### Added
- Concept locked through 3 Critic Review Board cycles.
- Game Design Document v1.0 (Mission 1 scene-by-scene + 6-mission overview).
- Asset Plan with 85% existing-inventory coverage and ~$65 must-buy gap list.
- Technical architecture (ScriptableObject-driven, ServiceLocator, Addressables-ready).
- Claude AI Co-pilot integration plan + Node proxy server. *(both removed in v0.2)*
- Core shared C# library (Mission, Save, Audio, Checkpoint, Pooling, Events).
- Hearth & Hex–specific scripts (Farming, Player, NPC, Dialogue UI, Time).
- Unity setup guide with step-by-step Mission 1 authoring instructions.
- MIT licence for original code + repository .gitignore tuned for Unity.
