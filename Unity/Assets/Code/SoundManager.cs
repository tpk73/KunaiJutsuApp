using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip gameMenuClip;
    [SerializeField] private AudioClip kunaiThrowClip;
    [SerializeField] private AudioClip kunaiHitClip;
    [SerializeField] private AudioClip objectKilledClip;

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

    private void PlaySound(AudioClip clip, float vol = 1)
    {
        if (gameManager.Instance.soundSettings)
        {
            if(Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, vol);
            }
        }
    }

    public void playMainMenuButton()
    {
        PlaySound(mainMenuClip);
    }

    public void playGameMenuButton()
    {
        PlaySound(gameMenuClip);
    }

    public void playKunaiThrowButton()
    {
        PlaySound(kunaiThrowClip);
    }

    public void playKunaiHitButton()
    {
        PlaySound(kunaiHitClip);
    }

    public void playObjectKilledButton()
    {
        PlaySound(objectKilledClip);
    }

    public void Vibrate()
    {
        if (gameManager.Instance.vibrationSettings)
        {
            Handheld.Vibrate();
        }
    }

}
