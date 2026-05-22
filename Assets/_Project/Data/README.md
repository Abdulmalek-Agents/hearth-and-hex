# Data folder — ScriptableObject authoring

## Subfolders to create

```
Data/
├── Missions/
│   ├── MissionDatabase.asset
│   ├── MissionData_M01.asset .. MissionData_M06.asset
├── Dialogue/                ← Branching DialogueNodeSO trees
│   ├── Node_Elra_Root.asset, Node_Elra_HarvestMoon.asset, ...
│   ├── Node_Benn_Root.asset, ...
├── LineBanks/               ← One-shot LineBankSO pools (greetings, journal narrations, etc.)
│   ├── LineBank_Granny_Journal.asset
│   ├── LineBank_Festival_Crowd.asset
├── Crops/
│   ├── Crop_Wheat.asset, Crop_Carrot.asset, Crop_Pumpkin.asset
├── Recipes/
└── Items/
```

## Mission 1 minimum required

1. MissionDatabase.asset with M01 in `missions` list.
2. MissionData_M01.asset with 8 objectives (GDD §6.2).
3. Node_Elra_Root.asset + Node_Benn_Root.asset with at least 4 nodes each.
4. Crop_Wheat.asset referencing Harvest Garden stage prefabs.

Missions 2–6: duplicate these assets, change values, no code changes.

> v0.2 note: Persona_*.asset files from v0.1 are no longer used.
> Branching `DialogueNodeSO` trees and `LineBankSO` pools replaced them.
