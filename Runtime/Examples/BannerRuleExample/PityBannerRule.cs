using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PityRule", menuName = "GachaSystem/GachaRules/Create new PityRule")]
public class PityBannerRule : GachaBannerRules
{
    //Example rule that when the pulls count is equal to maxPity the next rewardEntry will be on of the pityRewards
    [SerializeField] private List<RewardEntry> pityRewards; //Needs to have rewardEntries that are also present in GachaBanners rewardEntries
    [SerializeField] private int maxPity;

    public List<RewardEntry> PityRewards { get { return pityRewards; } set { pityRewards = value; } }
    public int MaxPity => maxPity;

    void OnEnable()
    {
        BannerTargetRewards = PityRewards;
    }
}
