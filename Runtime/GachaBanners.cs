using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GachaBannerState
{
    [SerializeField] private int totalPulls;
    [SerializeField] private int pullsSinceHighestRarityReward;
    [SerializeField] private RewardEntry lastHighestReward;
    public int TotalPulls { get { return totalPulls; } set { totalPulls = value; } }
    public int PullsSinceHighestRarityReward { get { return pullsSinceHighestRarityReward; } set { pullsSinceHighestRarityReward = value; } }
    public RewardEntry LastHighestReward { get { return lastHighestReward; } set { lastHighestReward = value; } }
}

[CreateAssetMenu(fileName = "GachaBanners", menuName = "GachaSystem/Create new Gacha Banner")]
public class GachaBanners : ScriptableObject
{
    [SerializeField] private List<RewardEntry> rewardEntries;
    [SerializeField] private List<GachaBannerRules> rules;
    [SerializeField] private GachaBannerState gachaState;

    public List<RewardEntry> RewardEntries => rewardEntries;
    public List<GachaBannerRules> Rules => rules;
    public GachaBannerState GachaState { set { gachaState = value; } get { return gachaState; } }
}
