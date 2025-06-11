using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    private List<RewardEntry> gachaEntries;
    private bool pityReached = false;
    private RateUpBannerRule rateUp;
    private FiftyFiftyBannerRule fiftyFifty;
    private PityBannerRule pityRule;
    public static GachaSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<RewardEntry> CustomPullSize(int pulls, GachaBanners gachaBanner) //Gives you a list of rewards based on the amount of pulls you make
    {
        List<RewardEntry> results = new List<RewardEntry>();
        for (int i = 0; i < pulls; i++)
        {
            RewardEntry result = RollTheGacha(gachaBanner);
            results.Add(result);
            UpdateGachaState(result, gachaBanner.GachaState);
        }
        return results;
    }

    private RewardEntry RollTheGacha(GachaBanners gachaBanner)
    {
        CheckWhatRulesAreActive(gachaBanner);
        gachaEntries = gachaBanner.RewardEntries;
        float totalPropability = gachaEntries.Sum(x => x.Probability); //To get a result from the list of rewards the banner holds based on the rates of each reward, the method uses weighted random selection system
        float roll = Random.Range(0, totalPropability);
        float cumulative = 0f;

        if (pityReached) //If the banner has pity rule and that pity rule has been reached it will continue
        {
            pityReached = false;
            if (fiftyFifty != null) //If the banner has fifity fifty rule it will go continue, The Fifity Rule currently activates if the banner also has a pity rule
            {
                return GetRewardFromTheFiftyFifty(gachaBanner.GachaState);
            }
            else
            {
                return GetPityReward();
            }
        }

        foreach (RewardEntry entry in gachaEntries)
        {

            if (ContainsRewardEntry(gachaBanner.Rules, entry)) //Checks if the reward from the list has its probability increased if the rateUP rule is present
            {
                cumulative += rateUp.RateUpProbability;
            }
            else
            {
                cumulative += entry.Probability;
            }

            if (roll <= cumulative)
            {
                return entry;
            }
        }
        return null;
    }

    private bool CheckIfPityReached(int maxPity, GachaBannerState state)
    {
        return state.PullsSinceHighestRarityReward >= maxPity;
    }

    private RewardEntry GetPityReward()
    {
        List<RewardEntry> pityRewards = pityRule.BannerTargetRewards;
        int randomNum = Random.Range(0, pityRewards.Count - 1);
        return pityRewards[randomNum];
    }

    private RewardEntry GetRewardFromTheFiftyFifty(GachaBannerState state)
    {
        if (CheckIfFiftyFiftyWon(state))
        {
            return fiftyFifty.Target;
        }
        else
        {
            List<RewardEntry> highestRarityPool = new List<RewardEntry>();
            foreach (RewardEntry entry in gachaEntries)
            {
                if (entry.IsHighestRarity)
                {
                    if (entry != fiftyFifty.Target)
                    {
                        highestRarityPool.Add(entry);
                    }
                }
            }
            int randomNum = Random.Range(0, highestRarityPool.Count - 1);
            return highestRarityPool[randomNum];
        }
    }

    private bool CheckIfFiftyFiftyWon(GachaBannerState state)
    {
        bool fiftyFiftyWon;
        float roll = Random.Range(0f, 100f);
        fiftyFiftyWon = roll < fiftyFifty.ChanceToWin;
        if (fiftyFifty.Target != state.LastHighestReward && state.LastHighestReward != null)
        {
            fiftyFiftyWon = true;
        }
        return fiftyFiftyWon;
    }

    private void CheckWhatRulesAreActive(GachaBanners gachaBanner)
    {
        List<GachaBannerRules> bannerRules = gachaBanner.Rules;
        foreach (GachaBannerRules rule in bannerRules)
        {
            switch (rule)
            {
                case PityBannerRule pityrule:
                    pityReached = CheckIfPityReached(pityrule.MaxPity, gachaBanner.GachaState);
                    pityRule = pityrule;
                    break;
                case FiftyFiftyBannerRule fiftyrule:
                    fiftyFifty = fiftyrule;
                    break;
                case RateUpBannerRule rateuprule:
                    rateUp = rateuprule;
                    break;
            }
        }
    }

    private bool ContainsRewardEntry(List<GachaBannerRules> rules, RewardEntry target) 
    {
        return rules
            .OfType<RateUpBannerRule>()
            .Any(rule => rule.BannerTargetRewards.Contains(target));
    }

    private void UpdateGachaState(RewardEntry entry, GachaBannerState state) //logs the state of the banner
    {
        state.TotalPulls++;
        state.PullsSinceHighestRarityReward++;
        if (entry.IsHighestRarity)
        {
            state.LastHighestReward = entry;
            state.PullsSinceHighestRarityReward = 0;
        }
    }
}