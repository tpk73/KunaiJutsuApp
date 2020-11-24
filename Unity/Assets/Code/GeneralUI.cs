using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{
    public static GeneralUI Instance;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("UI")]
    [SerializeField] private GameObject soundON;
    [SerializeField] private GameObject soundOFF;
    [SerializeField] private GameObject vibON;
    [SerializeField] private GameObject vibOFF;
    [SerializeField] private Text totalScroll;
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
    }

    public void SoundOnOFF()
    {
        if (soundON.activeSelf)
        {
            soundON.SetActive(false);
            soundOFF.SetActive(true);
        }
        else
        {
            soundON.SetActive(true);
            soundOFF.SetActive(false);
        }
    }
    public void VibOnOFF()
    {
        if (vibON.activeSelf)
        {
            vibON.SetActive(false);
            vibOFF.SetActive(true);
        }
        else
        {
            vibON.SetActive(true);
            vibOFF.SetActive(false);
        }
    }
}
