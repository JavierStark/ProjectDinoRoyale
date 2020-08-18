using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{

    [SerializeField] Slider musicVolSlider;
    [SerializeField] Slider fxVolSlider;

    private void Start() {
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        fxVolSlider.value = PlayerPrefs.GetFloat("FXVolume");
    }

    public void OnValueChangedMusic() {
        PlayerPrefs.SetFloat("MusicVolume", musicVolSlider.value);
        MusicManager.instance.ChangeVolume(musicVolSlider.value);
    }

    public void OnValueChangedFX() {
        PlayerPrefs.SetFloat("FXVolume", fxVolSlider.value);
    }
}
