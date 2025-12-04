using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss3Audiomanager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource[] audioSource;
    public static Boss3Audiomanager instance;
    void Awake()
    {
        audioSource = GetComponents<AudioSource>();
        if (audioClips == null || audioClips.Length == 0)
        {
            audioClips = new AudioClip[5];
            audioClips[0] = Resources.Load<AudioClip>("1 3 patten");
            audioClips[1] = Resources.Load<AudioClip>("2 patten");
            audioClips[2] = Resources.Load<AudioClip>("5 patten");
            audioClips[3] = Resources.Load<AudioClip>("2 2 patten");
            audioClips[4] = Resources.Load<AudioClip>("3 patten");
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource[1].Stop();
    }
    public void PlayAudio(int i)
    {
        audioSource[0].PlayOneShot(audioClips[i]);
    }

    public void PlayBGM()
    {
        audioSource[0].Stop();
        audioSource[1].Play();
        audioSource[1].loop = true;
    }
}
