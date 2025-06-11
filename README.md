# Gacha System Package

A customizable gacha system for Unity, supporting:
- Gacha banners
- Custom Rules like : Pity system, Rate-up mechanics
- Different types of rarity system (stars, grades)
- ScriptableObject-based reward entries
- The gacha system that gives you a reward based on probability and on the custom rules
- Generate the reward into vieable prefabs
- TextMeshPro integration
- Editor extensions

## Installation

Add the package via Unity Package Manager:


## Usage

- Create your custom rewards by creating scriptableobject of RewardEntry
- Create your custom GachaRules or Utulise the example rules by creating scriptableobject of the custom/existing rules
- Create your own GachaBanner  by creating scriptableobject of GachaBanner
- Utulise the GachaSystem script to get the results
- Utulise the GenerateResults script to viaulise the results

- See how these system work by Taking the Demo folder and adding it into your Asset Folder in order to access the example Scene
- New Scriptable Objects are currently needed to be created if you want to test various situations within the example scene, dont forget to swap for the newly created gachaBanner ScriptableObject in the buttons

## Dependencies

- Unity TextMeshPro

## Unity Version

Tested on Unity 2022.3.29f1

## Author

IasonasKW

## Notes

Be kind with the rates
