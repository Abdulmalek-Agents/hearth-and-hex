# 🌻 Hearth & Hex

> A cozy farming life-sim laced with light witch magic.
> You inherit your grandmother's overgrown smallholding on the edge of a stylized village. Cultivate crops, brew minor enchantments, befriend the townsfolk, and uncover the gentle mystery of why the harvest moon never rises here.

| | |
|---|---|
| **Genre** | Cozy Farming Life-Sim + Light Magic |
| **Platforms** | PC (Steam) primary; iOS / Android stretch goal |
| **Engine** | Unity **6 LTS (6000.4.4f1)** + URP |
| **Target frame-rate** | 60 fps on integrated GPU |
| **Mission 1 scope** | Tutorial day → first crop → first villager friendship |
| **Designed for** | 6 missions (seasonal arcs) |
| **Runtime AI features** | **None** — the shipping game is fully offline and self-contained |
| **AI in development** | Claude Code & Claude Agents are used by the studio to draft GDDs, generate C#, and write dialogue. See [docs/05_AI_ASSISTED_DEVELOPMENT.md](docs/05_AI_ASSISTED_DEVELOPMENT.md). |

---

## Why this game

| Signal | Source |
|---|---|
| Cozy game audience is durable and large | Stardew Valley has sold 30M+ copies as a solo-dev project |
| 2026 is a peak year for cozy releases | Witchbrook, Coral Island 1.0, multiple farm-magic hybrids announced |
| Magic + farming is a proven combo | Witchbrook hype, Garden Witch Life, Potion Permit |
| Steam now hosts an official "Cozy Games" category | Confirms platform-level demand |

Detailed market evidence is in [docs/01_IDEATION_AND_TRENDS.md](docs/01_IDEATION_AND_TRENDS.md).

---

## Repo layout

```
hearth-and-hex/
├── README.md                        ← you are here
├── CHANGELOG.md
├── LICENSE                          ← MIT (covers original code/docs only)
├── .gitignore                       ← Unity-standard
├── docs/                            ← all design + planning documents
│   ├── 01_IDEATION_AND_TRENDS.md
│   ├── 02_GAME_DESIGN_DOCUMENT.md
│   ├── 03_ASSET_PLAN.md
│   ├── 04_TECHNICAL_ARCHITECTURE.md
│   ├── 05_AI_ASSISTED_DEVELOPMENT.md   ← Claude in the dev workflow (NOT in-game)
│   ├── 06_CRITIC_REVIEW_CYCLES.md
│   └── 07_UNITY_SETUP_GUIDE.md
└── Assets/_Project/                 ← drop these straight into your Unity project
    ├── Scripts/                     ← ready-to-compile C#
    │   ├── Core/                    ← GameBootstrap, ServiceLocator, Mission, Save, Audio, Checkpoint, Pooling, Events
    │   ├── Dialogue/                ← DialogueNodeSO, LineBankSO, ScriptedDialogueService
    │   ├── Gameplay/                ← Farming, Player, NPC, Time, Mission01
    │   └── UI/                      ← MainMenuController, HUDController
    ├── Data/                        ← ScriptableObject authoring guide
    ├── Prefabs/                     ← (empty — author after asset import)
    ├── Scenes/                      ← (empty — author after asset import)
    ├── Art/                         ← (empty — destination for asset packs)
    └── Audio/
```

## Quick start

1. **Read** [docs/07_UNITY_SETUP_GUIDE.md](docs/07_UNITY_SETUP_GUIDE.md) — exact click-by-click setup.
2. **Create** a new Unity **6 LTS (6000.4.4f1)** URP project, then copy this repo's `Assets/_Project` folder into your Unity `Assets/` folder.
3. **Import** the asset packs listed in [docs/03_ASSET_PLAN.md](docs/03_ASSET_PLAN.md) (Harvest Garden, Medieval Village Megapack, Toon Town, Stylized Weather System, BoZo Fantasy Characters, Bamao Pack Fantasy GUI, Game UI & Puzzle Sound Effects Pack — already in your Inventix Asset Store inventory).
4. **Open** `Scenes/Bootstrap.unity` → Play.

> No proxy server, no API key, no internet config required.

## Status

| Stage | Status |
|---|---|
| Concept locked (3 critic cycles) | ✅ |
| GDD v1.0 approved | ✅ |
| Architecture & scripts | ✅ |
| v0.2 — runtime LLM removed, scripted dialogue stack | ✅ |
| v0.2.1 — Unity 6 LTS (6000.4.4f1) target | ✅ |
| Mission 1 scene authored | ⏳ requires asset import in Unity |
| Missions 2–6 outlined | ✅ data-driven, ready for content |
| Steam page | ⬜ |

> Maintained by Abdulmalek-Agents (Inventix Games). Pull requests welcome.
