using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private Text rewardText;
    [SerializeField] private ParticleSystem scrollPS;

    private void Update()
    {
        if (dailyReward.Instance.canReward())
        {
            rewardText.text = "Get Reward";
        }
        else
        {
            TimeSpan timeToReward = dailyReward.Instance.TimeToReward;
            rewardText.text = string.Format("{0:0}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
        }
    }

    public void rewardPlayer()
    {
        if (dailyReward.Instance.canReward())
        {
            int amount = dailyReward.Instance.getRandomReward();
            StartCoroutine(RewardPanel());
            dailyReward.Instance.resetRewardTime();

            gameManager.Instance.totalScrolls += amount;
        }
    }

    private IEnumerator RewardPanel()
    {
        rewardPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        Instantiate(scrollPS);
        yield return new WaitForSeconds(3f);
        rewardPanel.SetActive(false);

    }
}
