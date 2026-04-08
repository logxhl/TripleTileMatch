using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource clickTileAudioSource;
    [SerializeField] private AudioSource clickButtonAudioSource;
    public void PlayBGAudioSource()
    {
        bgAudioSource.Play();
    }
    public void PlayClickTileAudioClip()
    {
        clickTileAudioSource.PlayOneShot(clickTileAudioSource.clip);
    }
    public void PlayClickButtonAudioSource()
    {
        clickButtonAudioSource.PlayOneShot(clickButtonAudioSource.clip);
    }
}
