using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    public static MusicClass instance;
    public static bool continueSong;

    // Start is called before the first frame update
    private AudioSource audioSource;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip song)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = song;
        audioSource.Play();
        print(audioSource.isPlaying);

        if (!audioSource.isPlaying)
        {
            audioSource.enabled = true;
            audioSource.clip = song;
            audioSource.Play();
        }
    } 
}
