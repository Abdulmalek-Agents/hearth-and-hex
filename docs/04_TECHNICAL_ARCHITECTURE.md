# 🧱 Technical Architecture — Hearth & Hex

> **👨‍💻 Senior Unity Developer:** Mobile-grade, scalable-from-day-1. Mission 1 ships on the same code paths as Mission 6.
> v0.2: removed runtime LLM dependency. Dialogue is hand-authored ScriptableObjects.
> v0.2.1: Unity 6 LTS (6000.4.4f1) target.

## 1. Stack

| Layer | Choice |
|---|---|
| Engine | Unity **6 LTS (6000.4.4f1)** |
| Render | **URP 17.x** (Unity 6 default) |
| Scripting | C# 9, .NET Standard 2.1 |
| Input | New Input System |
| UI | Canvas + TextMeshPro |
| Async loading | Addressables |
| Save | JsonUtility → persistentDataPath |
| Dialogue | Hand-authored `DialogueNodeSO` + `LineBankSO` (ScriptableObjects) |
| Camera | Cinemachine 3.x (Unity 6 default) |
| Source control | Git + Unity Smart Merge + LFS |

## 2. Folder layout (`Assets/_Project/Scripts/`)

```
Core/        ← shared (every Inventix game)
  GameBootstrap.cs, ServiceLocator.cs, SceneLoader.cs
  Mission/    MissionDataSO, MissionDatabaseSO, MissionManager
  Save/       SaveService
  Audio/      AudioService
  Checkpoint/ CheckpointService
  Events/     GameEventChannelSO
  Pooling/    ObjectPool
Dialogue/    DialogueNodeSO, LineBankSO, ScriptedDialogueService
UI/          MainMenuController, HUDController
Gameplay/    ← Hearth & Hex specific
  Farming/    FarmPlot, CropDataSO
  Player/     PlayerController, PlayerInteractor
  NPC/        VillagerNpc, DialogueUI
  Time/       TimeOfDay
  Mission01/  Mission01Director
```

## 3. Scenes

| Scene | Build idx | Notes |
|---|---|---|
| Bootstrap | 0 | Spawns GameBootstrap → MainMenu |
| MainMenu | 1 | Heat UI + MissionDatabaseSO |
| Mission01_WelcomeHome | 2 | Cottage + farm + village |
| Mission02..06 | 3-7 | Placeholders (data-driven additions) |

## 4. Mission flow

```
MainMenu.OnContinue → IMissionService.StartMission('M01')
  → database.Get('M01') → SceneLoader.LoadSceneAsync
  → OnMissionStarted → HUDController updates title
Player interacts FarmPlot → ReportObjectiveProgress('m1_till_plot')
  → When count==target → AllRequiredObjectivesComplete → CompleteMission
  → ISaveService.MarkMissionComplete
```

## 5. Game-specific systems

- **Farming:** FarmPlot state machine (Wild→Tilled→Planted→Watered→Growing→Ready). CropDataSO drives growth time, stages, yield.
- **Time of day:** TimeOfDay runs 0-1440 min counter per in-game day; default 24 in-game min per real-life min.
- **Hearth brewing:** HearthController accepts RecipeSO; success raises RecipeBrewedChannelSO event.
- **NPC dialogue:** `VillagerNpc` holds a root `DialogueNodeSO`; E opens `DialogueUI` which renders the tree with player reply buttons; replies may report mission objectives.

## 6. Scalability checks ✅

| Concern | Resolution |
|---|---|
| Adding M7 requires C#? | ❌ No — author MissionData_M07.asset + scene |
| New NPC requires C#? | ❌ No — author `Node_<Name>_Root.asset` tree + drag prefab |
| Save format breaks on update? | ✅ kv dictionary preserves older fields |
| GC during walk? | ✅ Pooling for VFX/SFX |
| URP cost on integrated GPU? | ✅ Stylized Lit shaders only |
| Internet outage breaks game? | ❌ No — fully offline |

## 7. Unity 6 (6000.4.4f1) compatibility notes

- **URP** upgraded from 14.x (Unity 2022) to 17.x. Stylized Lit shaders ship Unity-6-ready; if an Asset Store pack imports with pink materials, run **Edit → Rendering → Render Pipeline Converter → Built-in to URP**.
- **Cinemachine** is now 3.x. The old `CinemachineFreeLook` is replaced by `CinemachineCamera` with a `CinemachineOrbitalFollow` component. The setup steps in `docs/07` use the Unity 6 names.
- **Render Graph API** is opt-in for URP performance; not required for M1 but available for later optimisation.
- **Addressables**, **TextMeshPro**, **New Input System**, **Splines**, **NavMesh** all work as-is.
- **Photon PUN 2** (used elsewhere in the slate) ships with Unity 6 support; Photon Voice 2 likewise.

## 8. Build targets

Windows x64 (primary), macOS (stretch), Android/iOS (stretch).

## 9. Performance budget (60 fps on Intel Iris Xe)

- Draw calls < 800
- Triangles < 1.2M
- Particles < 4,000
- Memory < 1 GB
- GC alloc/sec < 200 KB
- Audio voices < 24

Profile every scene with Unity Profiler before `main` merge.

## 10. CI (later)

GitHub Actions + `game-ci/unity-builder@v4` (Unity 6 supported); nightly Windows builds on `develop`.
