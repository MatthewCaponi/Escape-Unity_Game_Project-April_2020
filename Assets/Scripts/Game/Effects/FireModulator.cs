using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModulator : MonoBehaviour
{
    [SerializeField] float flameWait;
    [SerializeField] float flameTime;
    [SerializeField] float speed;
    [SerializeField] float flameRestart;
    [SerializeField] float animationBegin;
    [SerializeField] float readyToStartTime;
    [SerializeField] AudioClip fireBreathing;

    [SerializeField] GameObject audioManager;

    SoundEffectManager soundManager;
    private ParticleSystem flames;
    private Animation fireBall;
   
    private bool readyToStart = false;
    private int count = 0;
    private int readyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        flames = GetComponentInChildren<ParticleSystem>();
        fireBall = GetComponent<Animation>();
        fireBall["Fly Fireball Shoot"].speed /= speed;
        soundManager = audioManager.GetComponent<SoundEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToStart && readyCount < 1)
        {
            Invoke("ReadyToStart", readyToStartTime);
            ++readyCount;
        }
        
        if (readyToStart)
        {
            if (count == 0)
            {
                Invoke("ActivateAnimation", animationBegin);
                Invoke("ActivateFlame", flameWait);
                count = 1;
            }
        }
    }

    private void AnimationBegin()
    {
        fireBall.Play();
    }

    private void ActivateFlame()
    {
        flames.Play();
        soundManager.playAudioClip(fireBreathing, 1);
        Invoke("StopFlame", flameTime);
    }

    private void StopFlame()
    {
        flames.Stop();
    }

    private void ReadyToStart()
    {
        readyToStart = true;
    }
}
