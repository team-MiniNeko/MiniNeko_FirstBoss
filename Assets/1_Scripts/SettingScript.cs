using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{   
    public static float volume = 1;
    public Slider settingvolume;
    public AudioMixer mixer;
    void Start()
    {// 원하는 해상도 값
        settingvolume.value = volume;
    }
    void Update()
    {
        volume = settingvolume.value;
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
    }
}