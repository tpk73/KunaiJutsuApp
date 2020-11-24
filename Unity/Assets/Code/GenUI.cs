using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenUI : MonoBehaviour
{
    public static GenUI Instance;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("UI")]
    [SerializeField] private GameObject soundON;
    [SerializeField] private GameObject soundOFF;
    [SerializeField] private GameObject vibON;
    [SerializeField] private GameObject vibOFF;
    [SerializeField] private Text totalScroll;

    [SerializeField] private GameObject settingsButton;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        totalScroll.text = gameManager.Instance.totalScrolls.ToString();
        updateSoundsUI();
        updateVibUI();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HiddenLeaf");
    }

    public void SoundOnOFF()
    {
        SoundManager.Instance.playKunaiThrowButton();

        if (gameManager.Instance.soundSettings)
        {
            gameManager.Instance.soundSettings = false;
        }
        else
        {
            gameManager.Instance.soundSettings = true;
        }
    }
    public void VibOnOFF()
    {
        SoundManager.Instance.playKunaiThrowButton();

        if (gameManager.Instance.vibrationSettings)
        {
            gameManager.Instance.vibrationSettings = false;
        }
        else
        {
            gameManager.Instance.vibrationSettings = true;
        }
    }

    public void openShop()
    {
        SoundManager.Instance.playKunaiThrowButton();
        settingsButton.SetActive(false);
        shopPanel.SetActive(true);
    }
    public void closeShop()
    {
        SoundManager.Instance.playKunaiThrowButton();
        settingsButton.SetActive(true);
        shopPanel.SetActive(false);
    }

    public void openSettings()
    {
        SoundManager.Instance.playKunaiThrowButton();
        settingsPanel.SetActive(true);
    }
    public void closeSettings()
    {
        SoundManager.Instance.playKunaiThrowButton();
        settingsPanel.SetActive(false);
    }

    private void updateSoundsUI()
    {
        if (gameManager.Instance.soundSettings)
        {
            soundON.SetActive(true);
            soundOFF.SetActive(false);
        }
        else
        {
            soundON.SetActive(false);
            soundOFF.SetActive(true);
        }
    }

    private void updateVibUI()
    {
        if (gameManager.Instance.vibrationSettings)
        {
            vibON.SetActive(true);
            vibOFF.SetActive(false);
        }
        else
        {
            vibON.SetActive(false);
            vibOFF.SetActive(true);
        }
    }
}