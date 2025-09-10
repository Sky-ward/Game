# Side-scrolling Roguelike Action Game Design Document

> English translation of [GDD.md](GDD.md). Keep this file in sync with the Chinese version.

## Core Gameplay Overview
- 2D side-scrolling action roguelike with roguelite permanent progression elements
- Each run has procedurally generated levels; character resets on death but retains some resources

## Inspiration
- **Dead Cells**: Smooth combat and pixel art style
- **Hades**: Random skill combinations integrated with narrative
- **Rogue Legacy**: Permanent upgrades and generational progression

## Game Objectives & Setting
- Play as a special-ability adventurer exploring shifting labyrinths and defeating bosses
- Discover the world's truth through environmental storytelling and events

## Core Loop
1. Leave the base to enter random levels
2. Fight and explore to collect temporary rewards and resources
3. On death return to base, spend resources for permanent upgrades
4. Challenge deeper levels again

## Combat System
- Real-time side-scrolling action with movement, jumping, dodging, melee/ranged attacks and skills
- Enemy types: small, heavy, ranged, elite, boss
- Damage formula: `actual damage = attack - defense (minimum 1)`
- Emphasize hit feel with visuals, audio and screen shake

## Skills & Abilities
- **In-run skills**: Random abilities acquired during the run, stack to build playstyles
- **Out-of-run skill tree**: Unlock permanent stats and functions at the base using resources

## Items & Equipment
- Weapons are melee/ranged with multiple rarities and random affixes
- Armor and accessories offer defense or passive effects
- Consumables like potions, bombs and keys
- Simple equipment slots: two weapons, one armor, several item slots

## Levels & Maps
- Chapter-based structure, multiple rooms per chapter
- Modular procedural rooms including shops, chests, challenges, hidden rooms, etc.
- Difficulty increases by chapter with traps and stronger enemies

## RNG Systems
- Map layouts, enemy groups, loot drops and events are all random
- Smart drop system prevents extreme RNG
- Seed mode supported to reproduce specific runs

## User Interface
- HUD shows health, energy, skill bar, resources and mini-map
- Menus, inventory, skill tree, shops use clean pixel/cartoon style
- Dialog pop-ups and result screens provide feedback

## Art & Technology
- Pixel or cartoon style; AI can generate concept art drafts
- Unity or Godot recommended with built-in tilemap, physics, and input systems
- Data-driven design using config tables for numbers and text

## Project Iteration
1. **Prototype**: Verify core gameplay using placeholder assets
2. **Core Completion**: Add systems and content, replace final assets
3. **Content Expansion**: Add enemies, weapons, skills and polish
4. **Release**: Ship and plan post-launch updates

## Internationalization
- Use multilingual text tables supporting Chinese and English
- Fonts and layouts consider different language lengths
- Use AI for initial translation drafts followed by manual proofreading

