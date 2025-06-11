using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FiftyFiftyRule", menuName = "GachaSystem/GachaRules/Create new FiftyFiftyRule")]
public class FiftyFiftyBannerRule : GachaBannerRules
{
    //Example rule that when the RewardEntry that you get will be either the special target or the other high rarity rewards from the GachaBanners rewardEntries
    [SerializeField] private RewardEntry target; //Needs to be a rewardEntry that is also present in GachaBanners rewardEntries
    [SerializeField] private int chanceToWin; //Doesnt have to be 50% to win, you can make it higher (70 - 30)

    public RewardEntry Target => target;
    public int ChanceToWin => chanceToWin;
}
