using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    [SerializeField] Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadMusicVolume();
        }
        else SetMusicVolume();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        //if (volume < 0.0001)
        //{
        //    GameController.Instance.gameScene.imgbtnSpeaker.sprite = GameController.Instance.gameScene.spriteOnSpeaker;
        //}
        //else
        //{
        //    GameController.Instance.gameScene.imgbtnSpeaker.sprite = GameController.Instance.gameScene.spriteOffSpeaker;
        //}
    }
    public void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume();
    }
}