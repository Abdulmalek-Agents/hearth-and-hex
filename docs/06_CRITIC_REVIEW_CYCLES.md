# 🔍 Critic & Review Board — Cycles for Hearth & Hex

> Full audit trail. Nothing approved without Board sign-off.

## Cycle 1 — Concept review

| Reviewer | Verdict | Notes |
|---|---|---|
| Lead Game Director | ⚠️ Approved w/ Notes | Stardew+Witch over-pitched; need memorable hook |
| Technical Director | ⚠️ Approved w/ Notes | LLM cost+privacy concerns; need guardrails + offline fallback |
| Trend Analyst | ✅ Approved | Cozy durable; Witchbrook delay leaves window |
| Asset Director | ✅ Approved | ~80% coverage on first scan |

**Required for Cycle 2:**
1. Add hook differentiating from Stardew/Witchbrook → **resolved**: missing harvest moon mystery.
2. Plan proxy + offline fallback → **resolved in v0.1, then superseded in v0.2 (runtime LLM removed entirely).**
3. Estimate 1k-DAU cost → **resolved**: ~$7/day in v0.1; **$0 in v0.2**.

## Cycle 2 — GDD review

| Reviewer | Verdict | Notes |
|---|---|---|
| Lead Game Director | ⚠️ Approved w/ Notes | Mission 4 needs more conflict (Long Rain micro-arc) |
| Combat / Systems | ✅ Approved | No-combat is correct |
| Narrative Director | ⚠️ Approved w/ Notes | Hara intro too late; need M2 hint. Granny voice guide needed |
| Accessibility Lead | ⚠️ Approved w/ Notes | Need text size XL, OpenDyslexic, colourblind, opacity options |
| QA Lead | ✅ Approved | M1 testable in < 45 min |

**Required for Cycle 3:**
1. Long Rain weather-threat arc → **added**.
2. Seed Hara in M2 whisper at inn → **added**.
3. Granny voice guide (elderly, kind, slightly cryptic, never condescending) → **added**.
4. Accessibility options → **added** to GDD §11.

## Cycle 3 — Architecture + Asset Plan + AI integration review (v0.1)

| Reviewer | Verdict | Notes |
|---|---|---|
| Lead Game Director | ✅ Approved | Mission progression clear |
| Technical Director | ✅ Approved | ServiceLocator + ScriptableObjects + Addressables = correct |
| Mobile Performance Lead | ⚠️ Approved w/ Notes | Cap M4 storm particles 200/600 |
| Asset Director | ✅ Approved | 85% existing inventory + $65 must-buy justified |
| Licence/Legal | ✅ Approved | Asset Store EULA compliant; no binaries in repo |
| Trend Analyst | ✅ Approved | Re-validated Reddit/Steam |
| AI/Cost Lead | ✅ Approved | $7/1k DAU acceptable; fallback robust |

**Final required change:**
1. Cap M4 rain particles at 200 (low) / 600 (high) → **done** in 04 §8.

## Cycle 4 — Re-scope (v0.2): AI is dev workflow, not runtime

> **Trigger:** the studio owner clarified that "AI integration" means using
> AI **in development** (Claude Code, Claude Agents) to boost productivity —
> never as a runtime gameplay feature.

| Reviewer | Verdict | Notes |
|---|---|---|
| Lead Game Director | ✅ Approved | Cozy farming core never needed LLM; hand-authored dialogue is the cozy-genre standard (Stardew, Coral Island, Witchbrook) |
| Technical Director | ✅ Approved | Removing runtime LLM eliminates Anthropic dependency, proxy server, network failure path, GDPR/PII vectors |
| Narrative Director | ⚠️ Approved w/ Notes | Need to commission 30–60 hand-written lines per villager — covered by Claude-in-dev writing pass |
| Trend Analyst | ✅ Approved | No competitive disadvantage — none of our cozy peers ship runtime LLM either |
| Asset Director | ✅ Approved | Drop Dialogue System OpenAI Addon ($45 saved); other assets unchanged |
| Accessibility Lead | ✅ Approved | Branching reply buttons easier than free-typed input — improves accessibility |
| QA Lead | ✅ Approved | Deterministic dialogue is dramatically easier to test |
| AI/Cost Lead | ✅ Approved | Per-DAU LLM cost goes from $0.007 → $0 |

**Required for v0.2:**
1. Replace `ClaudeCopilotService` with `ScriptedDialogueService` → **done**.
2. Replace `AICopilotPersonaSO` with `DialogueNodeSO` + `LineBankSO` → **done**.
3. Rewrite `VillagerNpc` + `DialogueUI` for branching scripted dialogue → **done**.
4. Delete `server/copilot-proxy/` → **done**.
5. Replace `docs/05_AI_COPILOT_INTEGRATION.md` with `docs/05_AI_ASSISTED_DEVELOPMENT.md` → **done**.
6. Update README / CHANGELOG / docs to drop runtime-AI claims → **done**.
7. Commission writers (Claude-in-dev pass) for full M1 dialogue lines → **content task, tracked separately**.

## ✅ Board verdict — v0.2 FINAL

> **APPROVED.** Mission 1 vertical slice = first deliverable, now fully offline
> and self-contained. Subsequent missions are data-only additions.

Signed: Lead Game Director, Technical Director, Trend Analyst, Asset Director,
Accessibility Lead, QA Lead, AI/Cost Lead, Narrative Director.

## Open watch-list

- Genre saturation 2027 → reposition copy if needed
- Dialogue line authoring throughput → keep Claude-in-dev writing pass on schedule
- Player perception of "shorter cozy" → measure with first 1k wishlist demographic
