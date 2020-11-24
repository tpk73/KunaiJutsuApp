using UnityEngine;
using System;

public class dailyReward : MonoBehaviour
{
    public static dailyReward Instance;

    [SerializeField] private int hrToReward = 6;
    [SerializeField] private int minToReward = 30;
    [SerializeField] private int secToReward = 45;

    private int minReward = 20;
    private int maxReward = 60;

    private const string next_Reward = "Reward Time";

    public DateTime nextRewardTime => getNextRewardTime();
    public TimeSpan TimeToReward => nextRewardTime.Subtract(DateTime.Now);

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool canReward()
    {
        return nextRewardTime <= DateTime.Now;
    }

    public int getRandomReward()
    {
        return UnityEngine.Random.Range(minReward, maxReward);
    }

    public void resetRewardTime()
    {
        DateTime nextReward = DateTime.Now.Add(new TimeSpan(hrToReward, minToReward, secToReward));
    }

    public void saveNextRewardTime(DateTime time)
    {
        PlayerPrefs.SetString(next_Reward, time.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private DateTime getNextRewardTime()
    {
        string nextReward = PlayerPrefs.GetString(next_Reward, string.Empty);
        if (!string.IsNullOrEmpty(nextReward))
        {
            return DateTime.FromBinary(Convert.ToInt64(nextReward));
        }
        else
        {
            return DateTime.Now;
        }
    }
}
