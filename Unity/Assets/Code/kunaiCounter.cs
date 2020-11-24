using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kunaiCounter : MonoBehaviour
{

    public static kunaiCounter Instance;

    [SerializeField] private GameObject kunaiSprite;
    [SerializeField] private Color kunaiReadyColor;
    [SerializeField] private Color kunaiWastedColor;

    private List<GameObject> icons = new List<GameObject>();

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

    public void setupKunai (int amount)
    {
        foreach (var icon in icons)
        {
            Destroy(icon);
        }

        icons.Clear();

        for(int i = 0; i < amount; i++)
        {
            GameObject icon = Instantiate(kunaiSprite, transform);
            icon.GetComponent<Image>().color = kunaiReadyColor;
            icons.Add(icon);
        }
    }

    public void kunaiHit(int amount)
    {
        for(int i =0; i < icons.Count; i++)
        {
            icons[i].GetComponent<Image>().color = i < amount ? kunaiWastedColor : kunaiReadyColor;
        }
    }
}
