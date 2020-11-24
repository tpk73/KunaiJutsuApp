using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Settings")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text stageText;
    [SerializeField] private GameObject stageContainer;

    [SerializeField] private Color stageCompletedColor;
    [SerializeField] private Color stageNormalColor;

    public List<Image> stageIcons;

    [SerializeField] private GameObject bossFight;
    [SerializeField] private GameObject bossDefeated;

    [Header("GameOverUI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverScore;
    [SerializeField] private Text gameOverStage;
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
        scoreText.text = gameManager.Instance.score.ToString();
        gameOverScore.text = gameManager.Instance.score.ToString();

        stageText.text = "Stage " + gameManager.Instance.stage;
        gameOverStage.text = "Stage " + gameManager.Instance.stage;

        UpdateUI();
    }

    public IEnumerator BossStart()
    {
        bossFight.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossFight.SetActive(false);
    }
    public IEnumerator BossDefeated()
    {
        bossDefeated.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossDefeated.SetActive(false);
    }
    public void gameOver()
    {
        Debug.Log("Game Is Over Activate gameOver Screen");
        gameOverPanel.SetActive(true);
        stageContainer.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void openShop()
    {
        GenUI.Instance.openShop();
    }
    public void openOptions()
    {
        GenUI.Instance.openSettings();
    }

    public void goHome()
    {
        SceneManager.LoadScene("Menu");
    }

    private void UpdateUI()
    {
        if(gameManager.Instance.stage % 25 == 0)
        {
            foreach(var icon in stageIcons)
            {
                icon.gameObject.SetActive(false);

                stageIcons[stageIcons.Count - 1].color = stageNormalColor;
                stageText.text = "Boss " + levelManager.Instance.BossName;
            }
        }
        else
        {
            for (int i = 0; i < stageIcons.Count; i++)
            {
                stageIcons[i].gameObject.SetActive(true);
                stageIcons[i].color = gameManager.Instance.stage % 5 < i ? stageCompletedColor : stageNormalColor;

            }
        }
    }

}
