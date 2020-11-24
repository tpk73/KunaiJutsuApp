using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KunaiItem : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image kunaiImage;

    [SerializeField] private GameObject selectedImage;

    [Header("Colors")]
    [SerializeField] private Color kunaiUnlockColor;
    [SerializeField] private Color kunaiLockColor;
    [SerializeField] private Color kunaiUnlockBGColor;
    [SerializeField] private Color kunaiLockBGColor;

    [SerializeField] private int price;

    private Shop shop;
    private Kunai kunai;

    private const string KUNAI_UNLOCKED = "Kunai_Unlocked";

    public int Index { get; set; }

    public bool isUnlocked
    {
        get
        {
            if (Index == 0)
            {
                return true;
            }
            return PlayerPrefs.GetInt(KUNAI_UNLOCKED + Index, 0) == 1;
        }

        set
        {
            PlayerPrefs.SetInt(KUNAI_UNLOCKED + Index, value ? 1 : 0);
        }
    }

    public bool isSelected
    {
        get => selectedImage.activeSelf;

        set{
            if (value)
            {
                if (shop.kunaiSelectedItem != null)
                {
                    shop.kunaiSelectedItem.isSelected = false;
                }

                shop.kunaiSelectedItem = this;
            }

            selectedImage.SetActive(false);
        }
    }

    public int Price => price;

    public Image KunaiImage => kunaiImage; 

    public void setup( int index, Shop shop)
    {
        this.shop = shop;
        Index = index;
        kunai = this.shop.kunaiList[Index];
        KunaiImage.sprite = kunai.GetComponent<SpriteRenderer>().sprite;
        UpdateUI();
    }

    public void OnClick()
    {
        if(isUnlocked && isSelected)
        {
            GenUI.Instance.closeShop();
        }

        if (!isSelected)
        {
            isSelected = true;
        }

        if (isUnlocked)
        {
            gameManager.Instance.selectedKunai = Index;
        }

        shop.updateShop();
    }

    public void UpdateUI()
    {
        backgroundImage.color = isUnlocked ? kunaiUnlockBGColor : kunaiLockBGColor;
        KunaiImage.GetComponent<Mask>().enabled = !isUnlocked;

        KunaiImage.transform.GetChild(0).GetComponent<Image>().color = isUnlocked ? kunaiUnlockColor : kunaiLockColor;

        KunaiImage.transform.GetChild(0).gameObject.SetActive(!isUnlocked);
    }
}
