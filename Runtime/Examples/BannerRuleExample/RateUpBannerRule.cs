using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RateUPRule", menuName = "GachaSystem/GachaRules/Create new RateUPRule")]
public class RateUpBannerRule : GachaBannerRules
{
    //Example rule that changes the probability to the chosen rewards
    [SerializeField] private List<RewardEntry> rateUpRewards; //Needs to have rewardEntries that are also present in GachaBanners rewardEntries
    [SerializeField, Range(0, 100)] private int rateUpProbability;

    public List<RewardEntry> RateUpRewards { get { return rateUpRewards; } set { rateUpRewards = value; } }
    public int RateUpProbability => rateUpProbability;

    void OnEnable()
    {
        BannerTargetRewards = RateUpRewards;
    }
}
