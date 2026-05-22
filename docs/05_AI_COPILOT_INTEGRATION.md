# 🤖 Claude AI Co-pilot Integration — Hearth & Hex

> The studio's flagship differentiator. Villagers are alive, driven by Claude.

## 1. Why this matters

- *Suck Up!* proved LLM-driven NPCs are a viable retail experience — went viral on Reddit and indie communities for unpredictability.
- Inworld AI now powers AAA partners (Ubisoft, Disney, Xbox); small indies win on **soulful, niche** versions.
- Cozy players (r/CozyGamers polls) rank 'NPCs that remember me' top-3 wish-list.

Claude is well-suited for cozy: warm default voice, strong 'stay in character' steering via system prompts.

## 2. Touch points

| Surface | Role | Cost class |
|---|---|---|
| Villager dialogue (8 NPCs) | Persona + memory | Mid (Sonnet) |
| Granny journal VO | Short narrations per day | Low (Haiku) |
| Hearth recipe hints | Conversational help | Low (Haiku) |
| Mystery follow-ups | Gradual reveals | Mid |
| Festival flavour text | Crowd banter | Low (Haiku) |

**Offline fallback** for every surface: scripted lines if proxy unreachable. Gameplay never blocks.

## 3. Architecture

```
Unity Build → HTTPS POST → Node copilot-proxy → HTTPS POST → Anthropic Messages API
  (no API key, proxy holds it; rate-limited; per-IP guarded)
```

Short-term memory (last 32 facts/NPC) in RAM. Long-term via SaveData.kv.

## 4. Persona system (data-driven)

```
Assets/_Project/Data/AICopilot/
├── Persona_Elra.asset
├── Persona_Benn.asset
├── Persona_Rosa.asset
├── Persona_Tilda.asset
├── Persona_Marek.asset
└── Persona_Iva.asset
```

Edit any `systemPrompt` field — no recompile. Writers iterate freely.

## 5. Example: Elra's system prompt

```
You are Elra, innkeeper of Larkhollow Village in the cozy farming game 'Hearth & Hex'.

Voice: warm, gossipy, fond of dramatic exhales. Remembers everyone's inn meal. Avoids talking about late husband — if pressed, changes subject gently.

Setting: stylized 3D village with quiet magic. Player is the new witch on the hill, granddaughter of the late village witch Elsa.

Rules:
- Reply in 1–2 short sentences unless asked for a story.
- Never break character. Never reference being an AI/LLM.
- If asked about the harvest moon, say 'Oh love, that's a long story — buy me a hot cider some evening and I might tell you' but reveal no more in M1.
- Avoid modern words (phone, computer, wi-fi).
- Always be kind.
```

Keep prompts < 250 tokens for low latency (< 800 ms target).

## 6. Player flow

1. Player presses E on Elra.
2. Bamao dialogue UI opens.
3. VillagerNpc calls Ask(persona.systemPrompt + memorySummary, playerText, OnResponse).
4. Streamed response typewriter-types into UI.
5. Exchange added to NPC memory.
6. Recognised keywords (e.g., 'harvest moon', 'Elsa') may unlock objectives.

## 7. Safety & tone

- Pre-system-prompt directs refusals to be cozy and redirect to village life.
- Anthropic Usage Policy enforced.
- max_tokens=256 per response; clean sentence-boundary truncation.
- Profanity denylist on client.

## 8. Cost projection (1k DAU)

| Component | Tokens/day/player | $/day/player |
|---|---|---|
| Villager chats (8 turns × 200in/150out) | 2.8k | $0.005 |
| Granny journal (3 narrations) | 0.4k | $0.001 |
| Recipe hints | 0.3k | $0.0005 |
| **Total / DAU** | | **~$0.007** |

**Per 1k DAU: ~$7/day.** Easily offset by $14.99 one-time purchase.

## 9. Failure modes & UX

| Failure | UX |
|---|---|
| 502/network down | 'Elra is lost in thought…' + scripted fallback |
| 429 rate-limit | Same UX, queued retry |
| Latency > 3s | Typing-dots animation |
| Out-of-character slip | Hidden re-prompt 'Stay in character' |

## 10. Compliance ✅

- API key off client
- AI-NPCs toggle in main menu
- About panel discloses Anthropic processing
- No PII collected
- Transcripts scrubbed after 30 days
