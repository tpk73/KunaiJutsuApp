using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;

    private const string selected_Kunai = "Kunai";
    private const string high_Score = "High Score";
    private const string total_Scrolls = "Total Scrolls";
    private const string sound_Settings = "Sound Settings";
    private const string vibration_Settings = "Vibration Settings";

    public bool isGameOver { get; set; }
    public int stage { get; set; }
    public int score { get; set; }
    public Kunai selectedKunaiPrefab { get; set; }

    public float screenHeight => Camera.main.orthographicSize * 2;

    public float screenWidth => screenHeight / Screen.height * Screen.width;

    public int selectedKunai
    {
        get => PlayerPrefs.GetInt(selected_Kunai, 0);
        set => PlayerPrefs.SetInt(selected_Kunai, value);
    }

    public int highScore
    {
        get => PlayerPrefs.GetInt(high_Score, 0);
        set => PlayerPrefs.SetInt(high_Score, value);
    }

    public int totalScrolls
    {
        get => PlayerPrefs.GetInt(total_Scrolls, 0);
        set => PlayerPrefs.SetInt(total_Scrolls, value);
    }

    public bool soundSettings
    {
        get => PlayerPrefs.GetInt(sound_Settings, 1) == 1;
        set => PlayerPrefs.SetInt(sound_Settings, value ? 1 : 0);
    }
    public bool vibrationSettings
    {
        get => PlayerPrefs.GetInt(vibration_Settings, 1) == 1;
        set => PlayerPrefs.SetInt(vibration_Settings, value ? 1 : 0);
    }

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
}
