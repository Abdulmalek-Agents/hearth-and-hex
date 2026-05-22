# 🎨 Asset Plan — Hearth & Hex

> **🎨 Unity Asset Engineer:** Net coverage from Inventix existing inventory: **~85%**.

## 1. Existing inventory used

| Asset | Used for | Critical |
|---|---|---|
| **Harvest Garden** ($25) | Crops, tools, raised beds, scarecrow | 🔴 Yes |
| **Medieval Village Megapack** | Cottage, village houses, fences, market | 🔴 Yes |
| **Toon Town** ($40) | Outer Larkhollow lanes | 🟡 Helpful |
| **Stylized Weather System** ($20) | Morning fog, light rain, sunset | 🔴 Yes |
| **Zephyr Dynamic Wind** ($40) | Foliage/cloth wind | 🟡 Helpful |
| **BoZo Modular Characters Fantasy** ($40) | Player + 8 NPCs | 🔴 Yes |
| **Eyes Animator** ($11.99) | NPC procedural eye warmth | 🔴 Yes |
| **Bamao Pack Fantasy GUI** ($25) | In-game UI | 🔴 Yes |
| **Heat Complete Modern UI** ($69.99) | Main menu, settings, results | 🔴 Yes |
| **Game UI & Puzzle SFX Pack** ($99) | UI clicks, jingles, ambient | 🔴 Yes |
| **Casual RPG VFX** ($25) | Coin sparkle, level-up, brew | 🔴 Yes |
| **Spells Pack** ($59.99) | Field charms (M3+) — pick gentle ones | 🟡 Helpful |
| **Lumen Stylized Light FX 2** ($35) | Hearth glow, lanterns, moonrise | 🟡 Helpful |
| **Cutscene Engine** ($35) | Journal opening, M5 confide, M6 moonrise | 🔴 Yes |
| **Dialogue System OpenAI Addon** ($45) | Adapted to Claude (see 05) | 🟡 Helpful |
| **Hierarchy Designer** ($30) | Editor productivity | 🟢 Optional |
| **LightMap Fusion Pro** ($50) | Seasonal lightmap switching | 🟢 Optional |

**Existing inventory value applied: ~$700 retail across 16 assets.**

## 2. Gap analysis — must-buy

| Gap | Suggested asset | ~Cost |
|---|---|---|
| Cottage interior props (bed, hearth, kitchen) | Stylized Cottage Interior pack | $30 |
| Farm animals (chicken, sheep, cow) | Quirky Series Animals (Omabuarts) | $25 |
| OST (4 short loopable tracks) | Commission Fiverr/itch musician | $300 |
| Babble greetings (Animal Crossing style) | Optional voice generator | $0–50 |
| Cat model + anims (Pell) | Asset Store Stylized Cat or free Itch | $10 |

**Must-buy total worst case: ~$365. With OST royalty-free for M1: ~$65.**

## 3. Folder organisation

```
Assets/_Project/
├── Art/{Characters,Environment,VFX,UI}
├── Audio/{Music,SFX,Ambient}
├── Animations/
├── Materials/
├── Prefabs/{Characters,Environment,UI,VFX}
├── Scenes/
├── Data/{Missions,NPCs,AICopilot,Items}
└── Scripts/
```

After import, move third-party packages into `Art/...` via Unity 'Move asset' (preserves prefab references).

## 4. Performance tweaks per asset

| Asset | Tweak |
|---|---|
| Harvest Garden | Single-material atlas; GPU instancing on crop prefab |
| Medieval Village | Mesh-combine static per house |
| Stylized Weather | Cap precipitation particles at 200 (low) / 600 (high) |
| Spells Pack | Wrap VFX in ObjectPool — never Instantiate per-frame |
| BoZo | LOD0/LOD1 baked; LOD2 via Simplygon for mobile |
| Bamao GUI | ASTC 6×6 mobile / BC7 PC compression |

## 5. Licence audit ✅

Unity Asset Store EULA covers all listed assets. Inventix already holds licences. **Asset binaries are NOT redistributed** in this repo (Asset Store EULA prohibition) — only references and import instructions.

## 6. Post-purchase checklist

- [ ] Import every package per §1
- [ ] Move to `_Project/Art/` per §3
- [ ] Rig materials to URP/Lit
- [ ] Add LOD groups on hero props
- [ ] Configure ASTC/BC7 compression
- [ ] Build Player_Witch.prefab from BoZo
- [ ] Build 8 villager prefabs
- [ ] Build Crop_Wheat/Carrot/Pumpkin variants from Harvest Garden
- [ ] Author Hearth.prefab with Lumen + Casual RPG VFX
- [ ] Author WeatherDirector.prefab
