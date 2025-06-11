using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GachaBannerRules : ScriptableObject
{
    //Is Abstract to let you extend it and create your own rules like the 3 rule examples that I made
    private List<RewardEntry> bannerTargetRewards;
    public List<RewardEntry> BannerTargetRewards { get{ return bannerTargetRewards; } set{bannerTargetRewards = value; } }
}
