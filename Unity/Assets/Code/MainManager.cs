using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    [SerializeField] private Image selectedKunai;



    public Image SelectedKunai => selectedKunai;

    void Start()
    {
        SoundManager.Instance.playMainMenuButton();
    }

    public void Play()
    {
        SoundManager.Instance.playKunaiHitButton();
        SceneManager.LoadScene("HiddenLeaf");
    }


}
