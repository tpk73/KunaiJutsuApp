using System;
using System.Collections;
using UnityEngine;

public class levelManager : MonoBehaviour
{

    public static levelManager Instance;

    public Wheel[] wheels;
    public Boss[] bosses;

    [SerializeField] private GameObject kunaiPrefab;

    [Header("Wheel Settings")]
    [SerializeField] private Transform wheelSpawnPosition;
    [Range(0, 1)] [SerializeField] private float wheelScale;

    [SerializeField] private Transform kunaiSpawnPosition;
    [Range(0, 1)] [SerializeField] private float kunaiScale;

    private string bossName;
    public bool kunaiTF;
    private Wheel currWheel;
    private Kunai currKunai;

    public int totalSpawnKunai { get; set; }
    public string BossName { get => bossName; }

    private void Awake()
    {
        SoundManager.Instance.playGameMenuButton();
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        initializeGame();
    }

    private void initializeGame()
    {
        gameManager.Instance.isGameOver = false;
        gameManager.Instance.score = 0;
        gameManager.Instance.stage = 1;

        setupGame();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && kunaiTF == true)
        {
            kunaiTF = false;
            kunaiCounter.Instance.kunaiHit(totalSpawnKunai);
            StartCoroutine(generateKunai());
            currKunai.throwKunai();
            Debug.Log("Gen Kunai");
        }
    }

    private void setupGame()
    {
        spawnWheel();
        Debug.Log("Spawn Wheel");

        kunaiCounter.Instance.setupKunai(currWheel.AvailableKunai);

        totalSpawnKunai = 0;
        StartCoroutine(generateKunai());
    }

    private void spawnWheel()
    {
        GameObject tempWheel = new GameObject();

        if(gameManager.Instance.stage % 25 == 0)
        {
            // create boss wheel
            Boss newBoss = bosses[UnityEngine.Random.Range(0, bosses.Length)];
            tempWheel = Instantiate(newBoss.bossPrefab, wheelSpawnPosition.position, Quaternion.identity, wheelSpawnPosition).gameObject;
            bossName = "Boss " + newBoss.bossName;
        }
        else
        {
            // Fix This
            tempWheel = Instantiate(wheels[gameManager.Instance.stage - 1], wheelSpawnPosition.position, Quaternion.identity, wheelSpawnPosition).gameObject;
        }

        float wheelScaleInScreen = gameManager.Instance.screenWidth * wheelScale / tempWheel.GetComponent<SpriteRenderer>().bounds.size.x;
        tempWheel.transform.localScale = Vector3.one * wheelScaleInScreen;
        currWheel = tempWheel.GetComponent<Wheel>();
    }

    private IEnumerator generateKunai()
    {
        yield return new WaitUntil( (() => kunaiSpawnPosition.childCount == 0));

        if (currWheel.AvailableKunai > totalSpawnKunai && !gameManager.Instance.isGameOver)
        {
            totalSpawnKunai++;
            GameObject tempKunai = new GameObject();

            if(gameManager.Instance.selectedKunaiPrefab == null)
            {
                tempKunai = Instantiate(kunaiPrefab, kunaiSpawnPosition.position, Quaternion.identity, kunaiSpawnPosition).gameObject;
            }
            else
            {
                tempKunai = Instantiate(gameManager.Instance.selectedKunaiPrefab, kunaiSpawnPosition.position, Quaternion.identity, kunaiSpawnPosition).gameObject;
            }

            float kunaiScaleInScreen = gameManager.Instance.screenHeight * kunaiScale / tempKunai.GetComponent<SpriteRenderer>().bounds.size.y;
            tempKunai.transform.localScale = Vector3.one * kunaiScaleInScreen;
            currKunai = tempKunai.GetComponent<Kunai>();
        }
        yield return new WaitForSeconds(.02f);
        kunaiTF = true;
    }

    public void nextLevel()
    {
        if(currWheel != null)
        {
            currWheel.destroyKnife();
        }
        if(gameManager.Instance.stage % 25 == 0)
        {
            gameManager.Instance.stage++;
            StartCoroutine(BossDefeated());
        }
        else
        {
            gameManager.Instance.stage++;
            if(gameManager.Instance.stage % 25 == 0)
            {
                // UImanager display boss start fight
                StartCoroutine(BossFight());
            }
            else
            {
                Invoke(nameof(setupGame), .3f);
            }
        }
    }
    private IEnumerator BossFight()
    {
        StartCoroutine(UIManager.Instance.BossStart());
        yield return new WaitForSeconds(2f);
        setupGame();
    }
    private IEnumerator BossDefeated()
    {
        StartCoroutine(UIManager.Instance.BossDefeated());
        yield return new WaitForSeconds(2f);
        setupGame();
    }
}

[Serializable]
public class Boss
{
    public GameObject bossPrefab;
    public string bossName;
}
