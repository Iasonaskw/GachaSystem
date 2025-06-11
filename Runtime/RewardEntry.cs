using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RewardTypes //Can be expanded with your own types
{
    Character, Weapon, Skin, Resource, Custom
}

public enum RarityType //Can be expanded with your own types
{
    StarNumber, Letter, Custom
}

public enum LetterRarityRanking //Can be edited to your liking
{
    C, R, SR, SSR
}

[CreateAssetMenu(fileName = "RewardEntry", menuName = "GachaSystem/Create new Reward")]
public class RewardEntry : ScriptableObject
{
    [SerializeField] private string rewardName;
    [SerializeField] private Sprite rewardSprite;
    [SerializeField] private RewardTypes rewardType;
    [SerializeField] private RarityType rarityType;
    [SerializeField, Range(1, 6)] private int stars; //Change the range to fit your Amount of Rarities
    [SerializeField] private LetterRarityRanking letterRanking;
    [SerializeField, Range(0, 100)] private int probability;
    [SerializeField, HideInInspector] private bool isHighestRarity;

    public string RewardName => rewardName;
    public Sprite RewardSprite => rewardSprite;
    public RewardTypes RewardType => rewardType;
    public RarityType RarityType => rarityType;
    public int Stars => stars;
    public LetterRarityRanking LetterRanking => letterRanking;
    public int Probability => probability;
    public bool IsHighestRarity => isHighestRarity;

    public string GetLabel()
    {
        switch (rarityType)
        {
            case RarityType.StarNumber: return $"{stars}★";
            case RarityType.Letter: return $"{letterRanking}";
        }
        return rarityType.ToString();
    }

    public Color GetColor()
    {
        if (rarityType == RarityType.StarNumber)
        {
            switch (stars)
            {
                case 1: return Color.gray;
                case 2: return Color.green;
                case 3: return Color.blue;
                case 4: return new Color(0.5f, 0f, 0.5f); // purple
                case 5: return new Color(1f, 0.5f, 0f);   // orange
                case 6: return Color.yellow;
                default: return Color.white;
            }
        }
        else if (rarityType == RarityType.Letter)
        {
            switch (LetterRanking)
            {
                case LetterRarityRanking.C: return Color.green;
                case LetterRarityRanking.R: return Color.blue;                
                case LetterRarityRanking.SR: return Color.magenta;
                case LetterRarityRanking.SSR: return Color.yellow;
                default: return Color.white;
            }
        }
        else
        {
            return Color.white;
        }
    }

    private void OnValidate()
    {
        switch (rarityType)
        {
            case RarityType.StarNumber:
                isHighestRarity = stars == 6;
                break;
            case RarityType.Letter:
                isHighestRarity = letterRanking == GetMaxLetterRarity();
                break;
            case RarityType.Custom:
                isHighestRarity = false;
                break;
        }
    }

    private LetterRarityRanking GetMaxLetterRarity()
    {
        return Enum.GetValues(typeof(LetterRarityRanking))
                   .Cast<LetterRarityRanking>()
                   .OrderByDescending(x => x)
                   .First();
    }
}
