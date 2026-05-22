# 🛠️ Unity Setup Guide — Hearth & Hex

> Step-by-step. Follow in order → end with a playable Mission 1.
> **v0.2.1: Unity 6 LTS (6000.4.4f1) target. No proxy server, no API key, no internet config required.**

## Prerequisites

- Unity Hub + Unity **6 LTS (6000.4.4f1)** with Windows IL2CPP module
- Each asset from `docs/03_ASSET_PLAN.md` in your Inventix Asset Store account

## Step 1 — Create the Unity project

1. Unity Hub → **New project**. In the Editor dropdown choose **6000.4.4f1**.
2. Template: **Universal 3D** (Unity 6's URP template; replaces the old "3D (URP) Core").
3. Name: `HearthAndHex` (no spaces). Choose drive with 20+ GB free.
4. **Create project**.

## Step 2 — Drop this repo in

```bash
git clone https://github.com/Abdulmalek-Agents/hearth-and-hex.git
```

Copy this repo's `Assets/_Project/` → `<UnityProject>/Assets/_Project/`.
Copy `.gitignore` to project root.
Unity recompiles — should be **zero errors**. If TMP errors: **Window → TextMeshPro → Import TMP Essentials**.

## Step 3 — Render Pipeline + Quality

1. **Project Settings → Graphics**: URP 17.x asset assigned (Unity 6 default).
2. **Project Settings → Quality**: High (PC default); make Low tier without HDR/Soft Particles.
3. **Project Settings → Player → Color Space → Linear**.

## Step 4 — Import assets (in order!)

Order matters (shared shader overwrites):

1. Bamao Pack Fantasy GUI
2. Heat Complete Modern UI
3. Stylized Weather System
4. Zephyr Dynamic Wind
5. Medieval Village Megapack
6. Toon Town
7. Harvest Garden
8. BoZo Stylized Modular Characters Fantasy
9. Eyes Animator
10. Casual RPG VFX
11. Spells Pack
12. Lumen Stylized Light FX 2
13. Cutscene Engine
14. Game UI & Puzzle SFX Pack
15. (optional) Hierarchy Designer + LightMap Fusion Pro

After each: delete the demo scene; move package folder to `Assets/_Project/Art/...`.

> **Unity 6 note:** If any package imports with pink materials, run **Edit → Rendering → Render Pipeline Converter → Built-in to URP** (this also converts Unity 2022–era assets to URP 17.x).

## Step 5 — Bootstrap scene

1. **New Scene → Empty** → save `Scenes/Bootstrap.unity`.
2. Create empty `[Game]` GameObject. Add `GameBootstrap` component.
3. Add to Build Settings **as index 0**.

## Step 6 — MainMenu scene

1. **New Scene → Empty** → save `Scenes/MainMenu.unity`.
2. Drag Heat UI main menu prefab in.
3. Add `MainMenuController` to menu root.
4. Wire Continue/NewGame/Quit buttons in Inspector.
5. **Create → Inventix → Mission → Mission Database** → drag into controller.
6. Build Settings index 1.

## Step 7 — Author Mission 1

1. **New Scene → 3D** → save `Scenes/Mission01_WelcomeHome.unity`.
2. Directional Light 'Sun', rotation `(45, -30, 0)`.
3. Drag Stylized Weather prefab; preset 'Misty Morning'.
4. Drop Medieval Village cottage at origin. Doorway BoxCollider as trigger `Trig_Door_Out`.
5. Drop Harvest Garden raised-bed; add 9 FarmPlot components (3×3).
6. 60m down path: 4-6 Toon Town buildings around a well.
7. Drop BoZo NPCs near inn (`NPC_Elra`) and shepherd house (`NPC_Benn`). Attach `VillagerNpc`, set `displayName`, drag `Node_Elra_Root.asset` (or `Node_Benn_Root.asset`) into `rootDialogue`, drag the `DialogueUI` reference, optionally set `meetObjectiveId`.
8. Drop Player_Witch.prefab at cottage spawn. **In Unity 6**: add a **CinemachineCamera** + **CinemachineOrbitalFollow** (the modern replacement for the old CinemachineFreeLook). Set Follow + LookAt to the player.
9. Empty `[Mission01Director]` GameObject; attach Mission01Director.cs; wire triggers.
10. **Create → Inventix → Mission → Mission Data**, name `MissionData_M01.asset`. Fill 8 objectives (see GDD §6).
11. Add to MissionDatabase. Build Settings index 2.

## Step 8 — Author the dialogue trees

For each NPC who has lines in M1 (Elra + Benn for tutorial):

1. **Create → Inventix → Dialogue → Node** → `Node_Elra_Root.asset`.
2. Fill `speakerDisplayName = "Elra"`, `speakerLine = "Oh love, you must be Elsa's granddaughter…"`, etc.
3. For each reply: add to `replies` array — set `playerLine`, drop the next `DialogueNodeSO` into `next` (or leave null to end), optionally fill `objectiveIdToReport` (e.g. `m1_meet_elra`).
4. Chain 3–5 follow-up nodes per villager for M1.
5. **Optional:** drop a wav into `voiceOver` if you have VO.

## Step 9 — First playtest

1. Open `Bootstrap.unity` → **Play**.
2. Bootstrap → MainMenu → New Game → MissionManager loads M01 scene.
3. Cottage → outside → till 3 → plant 3 → water 3 → walk to village → E on Elra → DialogueUI opens → click reply that matches `m1_meet_elra` → mission progress updates → return home → Mission complete.

## Troubleshooting

| Symptom | Fix |
|---|---|
| `IMissionService not registered` | Bootstrap not scene 0 |
| Pink materials | Render Pipeline Converter (Built-in → URP) |
| CinemachineFreeLook is missing | Unity 6 — use **CinemachineCamera + CinemachineOrbitalFollow** instead |
| Player falls through ground | Add MeshCollider to raised bed |
| Dialogue opens then immediately closes | `rootDialogue` not assigned on `VillagerNpc` |
| Reply buttons not appearing | `replyContainer` / `replyButtonPrefab` not assigned on `DialogueUI` |
| FPS drop in village | Mesh-combine statics, bake lightmaps |
| Weird eyes | Eyes Animator needs Left/Right Eye transforms per villager |

## After M1 plays

1. Tag `v0.2.1-mission1-playable`.
2. Author M2: duplicate scene, create `MissionData_M02.asset`, add Mission02Director.
3. Repeat M3-M6. **Zero core C# changes** required — that's the architecture win.
