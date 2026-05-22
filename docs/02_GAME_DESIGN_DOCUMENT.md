# 📜 Game Design Document — Hearth & Hex

> **🗺️ Game & Level Designer:** GDD v1.0 — approved by Creative Director after 3 review cycles.

## 1. High-concept

You inherit your late grandmother's overgrown smallholding on the edge of **Larkhollow Village**. A gentle magic clings to the land. Cultivate crops, brew minor enchantments at the heirloom hearth, befriend villagers, and slowly uncover why the harvest moon hasn't risen here in seven years.

**Player fantasy:** 'I am the kind, capable witch every village wishes for.'

**Emotional journey:** Anxiety (overgrown ruin) → competence (first harvest) → belonging (first friendship) → wonder (first enchantment) → purpose (restoring the moon).

**Pillars (do not violate):**
1. **Warm, never punishing** — fail states are softened.
2. **Tactile** — every interaction has satisfying SFX and animation.
3. **Honest pacing** — a real evening (45–60 min) finishes one mission's day.

## 2. Core game loop

`Wake → Tend land → Walk to village (NPCs) → Mission beat → Brew at hearth → Sleep`

## 3. Player verbs

| Verb | Input | Unlocked | Feedback |
|---|---|---|---|
| Walk / run | WASD / Shift | M1 | Footstep SFX per surface |
| Interact | E | M1 | Bamao GUI prompt |
| Use tool | LMB + Q wheel | M1 | Animation + soil/water VFX |
| Talk to NPC | E on NPC | M1 | Bamao dialogue + Claude streaming text |
| Brew enchantment | At hearth | M2 | Casual RPG VFX + jingle |
| Cast field charm | Hotbar 1-4 | M3+ | Spells Pack VFX |

**No combat. No enemies.** Crows / slugs harass crops; you shoo or charm them — never harm.

## 4. Progression

| System | M1 reveal | Late-game ceiling |
|---|---|---|
| Stamina | Implied | Friendship boosts |
| Friendships | Meet 2 of 8 | Heart 1-10 |
| Enchantments | None | 15 charms |
| Farm expansion | 1 plot | 6 plots + greenhouse + beehive + henhouse |
| Hearth recipes | 0 | 30+ |
| Mystery clues | 1 | Restore the moon |

## 5. Mission structure (6 seasonal missions)

| # | Title | Season | Hook | New beat |
|---|---|---|---|---|
| **1** | *Welcome Home* | Late winter | Granny's journal | Tutorial farming, meet 2 villagers |
| 2 | *First Bloom* | Early spring | Hearth lights itself | Brewing introduced |
| 3 | *Sun Festival* | Late spring | Festival invitation | First field charm, meet all 8 |
| 4 | *Long Rain* | Summer | Persistent rain threatens harvest | Weather charm, greenhouse |
| 5 | *Last Embers* | Autumn | Villager confides a secret | Partial mystery reveal |
| 6 | *The Harvest Moon Rises* | Winter | Final ritual | Solve mystery, restore moon |

## 6. Mission 1 — *Welcome Home* (scene-by-scene)

**Goal:** Establish tone, tutorialise farming, deliver first warm friendship. **Duration:** 30–45 min.

**Flow:**
1. Cottage Interior — opening cutscene; Granny's journal glows; Claude-generated greeting.
2. Cottage Doorway — first objective to step outside.
3. Farm Plot — till 3 tiles, plant 3 seeds, water with can.
4. Village Path — meet Elra (innkeeper) and Benn (shepherd), Claude AI conversations.
5. Cottage at Sunset — return home, mission complete.

**Objectives (in MissionDataSO):**
- `m1_open_journal` (1)
- `m1_till_plot` (3)
- `m1_plant_seeds` (3)
- `m1_water_plants` (3)
- `m1_meet_elra` (1)
- `m1_meet_benn` (1)
- `m1_return_home` (1)
- `m1_optional_pet_cat` (1, optional)

**Checkpoints:** Auto-save on doorway exit, village entry, sleep.

## 7. NPC roster

| ID | Name | Role | Claude persona seed |
|---|---|---|---|
| `elra` | Elra | Innkeeper | Warm, gossipy; avoids talking about late husband |
| `benn` | Benn | Shepherd | Sparse, dry humour, loves dog Pippin |
| `rosa` | Rosa | Apothecary | Curious, lightly competitive |
| `tilda` | Tilda | Elder/mayor | Speaks in soft proverbs |
| `marek` | Marek | Travelling tinker | Brings letters and rumours |
| `iva` | Iva | Baker's daughter | Boundless enthusiasm, asks 'Why?' |
| `pell` | Pell | The cat | Meow logic only |
| `hara` | Hara | Hooded stranger | Locked until M5 |

## 8. Economy

- **Coin** — sell crops, buy seeds/tools/gifts.
- **Hearth Embers** — brewing + missions, unlock recipes.
- **Memory Pages** — found in environment, story unlocks.

## 9. UI

| Screen | Asset |
|---|---|
| Main menu | Heat UI + Bamao overlays |
| HUD | Bamao mini-bar |
| Inventory | Bamao grid |
| Dialogue | Bamao + Claude streaming text |
| Mission complete | Heat UI results |

## 10. Audio

- Music: Piano + acoustic guitar; commission OST (~$300).
- Ambience: Birdsong, crickets, wind. Stylized Weather + Game UI & Puzzle SFX Pack.

## 11. Accessibility

Text size XL, OpenDyslexic font, 3 colourblind presets, subtitle opacity, AI toggle.

## 12. Cut-list (if scope slips)

1. Beehive (M5 reward)
2. Greenhouse build sub-quest
3. Hara's full storyline (cameo only)
4. Cat petting Easter egg
5. Festival fireworks (replaced with light shafts)

**Never cut:** journal opening, first hearth lighting, M6 moonrise cutscene.

✅ **Approved by Creative Director.**
