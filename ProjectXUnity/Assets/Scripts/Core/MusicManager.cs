using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    AudioSource audioSource;

    [SerializeField] AudioClip menuClip;
    [SerializeField] AudioClip gameClip;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = menuClip;
        audioSource.Play();
    }

    public void GameMusic(){
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        if(audioSource.clip == gameClip) return;
        else {
        audioSource.clip = gameClip;
        audioSource.Play();
        }
    }

    public void MenuMusic(){
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        if (audioSource.clip == menuClip) return;
        else {
        audioSource.clip = menuClip;
        audioSource.Play();
        }
    }

    public void ChangeVolume(float volume) {
        audioSource.volume = volume;
    }
}
