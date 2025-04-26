using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soundText;
    private bool isActive;
    [SerializeField] private int activeIndex;
    [Header("Sounds")]
    [SerializeField] private AudioSource[] sounds;
    private void Start()
    {
        activeIndex = PlayerPrefs.GetInt("Sound Index");
        if (activeIndex == 0)
        {
            isActive = true;
            soundText.text = "Sound: ON";
        }
        else
        {
            isActive = false;
            soundText.text = "Sound: OFF";
        }
        SetSoundState(isActive);
    }
    private void SetSoundState(bool active)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].gameObject.GetComponent<AudioSource>().enabled = active;
        }
    }
    public void SetSound()
    {
        isActive = !isActive;

        if(isActive)
        {
            activeIndex = 0;
            soundText.text = "Sound: ON";
        }
        else
        {
            activeIndex = 1;
            soundText.text = "Sound: OFF";
        }

        PlayerPrefs.SetInt("Sound Index", activeIndex);
        SetSoundState(isActive);
    }
    public void PlaySound(AudioSource sound)
    {
        if (sound.enabled)
            sound.Play();
    }
}
