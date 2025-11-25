using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Audiomanager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    public static Boss3Audiomanager instance;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClips == null || audioClips.Length == 0)
        {
            audioClips = new AudioClip[3];
            audioClips[0] = Resources.Load<AudioClip>("1 3 patten");
            audioClips[1] = Resources.Load<AudioClip>("2 patten");
            audioClips[2] = Resources.Load<AudioClip>("5 patten");
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
    }
    public void PlayAudio(int i)
    {
        audioSource.PlayOneShot(audioClips[i]);
    }
}
