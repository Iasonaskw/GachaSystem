using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateResults : MonoBehaviour
{
    [SerializeField] private GameObject rewardResultPrefab;
    [SerializeField] private Image imageComponent;
    [SerializeField] private Transform rewardResultContainer;
    [SerializeField] private Sprite commonResultBorder;
    [SerializeField] private Sprite rareResultBorder;
    [SerializeField] private Sprite epicResultBorder;
    [SerializeField] private Sprite legendaryResultBorder;
    List<GameObject> activeResults = new List<GameObject>();
    private int pullCount = 1;

    public void changePullCount(int pullCount) //Used for the button that lets you do more than one pull
    {
        this.pullCount = pullCount;
    }

    public void GenerateRewards(GachaBanners gachaBanner) //Generates the rewards that appear after clicking the button to pull
    {
        imageComponent.enabled = true;
        List<RewardEntry> rewards = GachaSystem.instance.CustomPullSize(pullCount, gachaBanner);

        foreach (RewardEntry reward in rewards)
        {
            BuildPrefab(reward);
        }
    }

    public void CloseResults() //Destroys the Prefabs that where instantiated when you pulled
    {
        if (!imageComponent.enabled)
        {
            return;
        }

        for (int i = 0; i < activeResults.Count; i++)
        {
            Destroy(activeResults[i]);
        }
        activeResults.Clear();
        imageComponent.enabled = false;
        PlayChestAnimation.instance.ResetChest();
    }

    public void ResetBannerState(GachaBanners gachaBanner) //Resets the Banner State to 0, usefull for testing if rules you make work properly
    {
        GachaBannerState state = gachaBanner.GachaState;
        state.LastHighestReward = null;
        state.PullsSinceHighestRarityReward = 0;
        state.TotalPulls = 0;
    }

    private void BuildPrefab(RewardEntry reward) //Builds the prefab based of the reward you get, currently supports the example prefab I made with rewards that have letterRanking rarity
    {
        GameObject resultPrefab = Instantiate(rewardResultPrefab, rewardResultContainer);
        if (resultPrefab != null)
        {
            resultPrefab.transform.Find("RewardName").GetComponent<TextMeshProUGUI>().text = reward.RewardName;
            resultPrefab.transform.Find("RewardIcon").GetComponent<Image>().overrideSprite = reward.RewardSprite;
            TextMeshProUGUI textMesh = resultPrefab.transform.Find("RewardRarity").GetComponent<TextMeshProUGUI>();
            switch (reward.LetterRanking)
            {
                case LetterRarityRanking.C:
                    resultPrefab.GetComponent<Image>().overrideSprite = commonResultBorder;
                    textMesh.text = "C";
                    textMesh.color = reward.GetColor();
                    break;
                case LetterRarityRanking.R:
                    resultPrefab.GetComponent<Image>().overrideSprite = rareResultBorder;
                    textMesh.text = "R";
                    textMesh.color = reward.GetColor();
                    break;
                case LetterRarityRanking.SR:
                    resultPrefab.GetComponent<Image>().overrideSprite = epicResultBorder;
                    textMesh.text = "SR";
                    textMesh.color = reward.GetColor();
                    break;
                case LetterRarityRanking.SSR:
                    resultPrefab.GetComponent<Image>().overrideSprite = legendaryResultBorder;
                    textMesh.text = "SSR";
                    textMesh.color = reward.GetColor();
                    break;
            }
            activeResults.Add(resultPrefab);
        }        
    }
}
