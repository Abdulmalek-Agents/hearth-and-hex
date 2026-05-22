# Data folder — ScriptableObject authoring

## Subfolders to create

```
Data/
├── Missions/
│   ├── MissionDatabase.asset
│   ├── MissionData_M01.asset .. MissionData_M06.asset
├── AICopilot/
│   ├── Persona_Elra.asset, Persona_Benn.asset, etc.
├── Crops/
│   ├── Crop_Wheat.asset, Crop_Carrot.asset, Crop_Pumpkin.asset
├── Recipes/
└── Items/
```

## Mission 1 minimum required

1. MissionDatabase.asset with M01 in `missions` list.
2. MissionData_M01.asset with 8 objectives (GDD §6.2).
3. Persona_Elra.asset + Persona_Benn.asset.
4. Crop_Wheat.asset referencing Harvest Garden stage prefabs.

Missions 2–6: duplicate these assets, change values, no code changes.
