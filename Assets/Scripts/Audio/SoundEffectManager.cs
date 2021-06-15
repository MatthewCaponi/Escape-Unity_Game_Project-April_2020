using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playAudioClip(AudioClip aud)
    {
        print("Playing");
        audioSource.PlayOneShot(aud);
    }

    public void stopAudio()
    {
        audioSource.Stop();
    }

    public void playFromList(int index)
    {
        audioSource.PlayOneShot(sounds[index]);
    }

    public void playAudioClip(AudioClip aud, float volume)
    {
        print("Playing");
        audioSource.PlayOneShot(aud, volume);
    }

    public bool isPlaying()
    {
        return audioSource.isPlaying;
    }

    public void pause(string status)
    {
        if (status == "pause")
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}
