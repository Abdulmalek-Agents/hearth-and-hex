# 🔍 Critic & Review Board — 3 Cycles for Hearth & Hex

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
2. Plan proxy + offline fallback → **resolved**: see 05.
3. Estimate 1k-DAU cost → **resolved**: ~$7/day.

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

## Cycle 3 — Architecture + Asset Plan + AI integration review

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

## ✅ Board verdict — FINAL

> **APPROVED.** Mission 1 vertical slice = first deliverable. Subsequent missions are data-only additions.

Signed: Lead Game Director, Technical Director, Trend Analyst, Asset Director, Accessibility Lead, AI/Cost Lead.

## Open watch-list

- Genre saturation 2027 → reposition copy if needed
- LLM token-cost shifts → swap Sonnet → Haiku for non-critical NPCs
- Player typing fatigue → add quick-reply chips if telemetry < 30% engagement
