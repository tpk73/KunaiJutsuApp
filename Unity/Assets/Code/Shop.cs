using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private KunaiItem itemPrefab;
    [SerializeField] private GameObject shopContainer;

    [Header("Text")]
    [SerializeField] private Text counter;
    [SerializeField] private Text price;

    [Header("Kunais")]
    [SerializeField] private Image kunaiUnlocked;
    [SerializeField] private Image kunaiLocked;

    [SerializeField] private MainManager mainManager;

    private List<KunaiItem> shopItems;
    public Kunai[] kunaiList;

    public KunaiItem kunaiSelectedItem { get; set; }

    public KunaiItem selectedItem
    {
        get
        {
            return shopItems.Find(x => x.isSelected);
        }
    }

    private void Start()
    {
        setup();
    }
    private void Update()
    {
        price.text = selectedItem.Price.ToString();
    }

    private void setup()
    {
        shopItems = new List<KunaiItem>();
        for (int i = 0; i < kunaiList.Length; i++)
        {
            KunaiItem item = Instantiate(itemPrefab, shopContainer.transform);
            item.setup(i, this);
            shopItems.Add(item);
        }
        shopItems[gameManager.Instance.selectedKunai].OnClick();
    }

    public void unlockKunai()
    {
        if (gameManager.Instance.totalScrolls > selectedItem.Price)
        {
            gameManager.Instance.totalScrolls -= selectedItem.Price;
            selectedItem.isUnlocked = true;
            selectedItem.UpdateUI();
            gameManager.Instance.selectedKunai = selectedItem.Index;
            updateShop();
            SoundManager.Instance.playKunaiThrowButton();
        }
    }

    public void updateShop()
    {
        kunaiUnlocked.sprite = selectedItem.KunaiImage.sprite;
        kunaiLocked.sprite = selectedItem.KunaiImage.sprite;

        kunaiUnlocked.gameObject.SetActive(selectedItem.isUnlocked);
        kunaiLocked.gameObject.SetActive(!selectedItem.isUnlocked);

        int itemsUnlocked = 0;

        itemsUnlocked = shopItems.FindAll(x => x.isUnlocked).Count;

        counter.text = itemsUnlocked + "/" + kunaiList.Length;

        gameManager.Instance.selectedKunaiPrefab = kunaiList[gameManager.Instance.selectedKunai];

        if (mainManager != null && gameManager.Instance != null)
        {
            mainManager.SelectedKunai.sprite = gameManager.Instance.selectedKunaiPrefab.GetComponent<SpriteRenderer>().sprite;
        }

    }

}
