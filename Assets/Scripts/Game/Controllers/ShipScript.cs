using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] ParticleSystem[] cannonFire;
    [SerializeField] ParticleSystem[] muzzleFlash;
    [SerializeField] float fireDifference;
    [SerializeField] AudioClip cannonFireSound;
    AudioSource audioSource;

    private int cannonTurn;
    // Start is called before the first frame update
    void Start()
    {
        cannonTurn = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cannonTurn == 0)
        {
            takeFire();
        }
    }

    private void takeFire()
    {
        if (cannonTurn < cannonFire.Length)
        { 
            muzzleFlash[cannonTurn].Play();
            cannonFire[cannonTurn].Play();
            audioSource.PlayOneShot(cannonFireSound, .5f);
            ++cannonTurn;
            Invoke("takeFire", fireDifference);
        }
        else
        {
            cannonTurn = 0;
        }
    }    
}
