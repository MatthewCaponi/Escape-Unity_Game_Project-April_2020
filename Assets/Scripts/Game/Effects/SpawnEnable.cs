using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnable : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] AudioSource audioManager;
    
    [SerializeField] Vector3 explosionOffset;

    [SerializeField] ParticleSystem [] spawnExplosion;
    [SerializeField] ParticleSystem[] charges;
    [SerializeField] ParticleSystem[] dissolvers;

    [SerializeField] VikingCrew.Tools.UI.SpeechBubbleManager endSpeech;

    [SerializeField] float dissolveTime;

    SoundEffectManager soundEffect;
    GameObject[] dissolvedObjects;

    int chargeCount = 0;
    int dissolveCount = 0;
    int destroyCount = 0;
    int destroyed;

    int i = 0;
    int j = 0;

    private bool portalStop = false;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<SpawnEffect>().enabled = false;
        soundEffect = audioManager.GetComponent<SoundEffectManager>();
        GameObject [] dissolvedEffects = GameObject.FindGameObjectsWithTag("Dissolve");
        GameObject [] dissolvedOrbs = GameObject.FindGameObjectsWithTag("Orb");
        dissolvedObjects = dissolvedEffects.Concat(dissolvedOrbs).ToArray<GameObject>();
        destroyed = dissolvedObjects.Length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collllde");
        if (collision.gameObject.name == "LilRobotWhite")
        { 
            GetComponent<SpawnEffect>().enabled = true;

            if (!soundEffect.isPlaying())
            {
                soundEffect.playAudioClip(sounds[0]);
                soundEffect.playAudioClip(sounds[1]);

                Invoke("PortalSound", 3);
                Invoke("TurnOffDissolve", GetComponent<SpawnEffect>().spawnEffectTime);
            }

            soundEffect.playAudioClip(sounds[3]);
        }
    }

    private void PortalSound()
    {
        soundEffect.playAudioClip(sounds[2]);
        soundEffect.playAudioClip(sounds[4]);
        soundEffect.playAudioClip(sounds[2]);
        Invoke("PortalSound", 4);
    }

    private void TurnOffDissolve()
    {
        soundEffect.playAudioClip(sounds[5]);
        for (int i = 0; i < spawnExplosion.Length; ++i)
        {
            spawnExplosion[i].transform.position = gameObject.transform.position - explosionOffset;
            spawnExplosion[i].Play();
        }

        DissolveOperator(); 
    }

    public void DissolveOperator()
    {
        for (int i = 0; i < dissolvedObjects.Length; ++i)
        {
            if (chargeCount == charges.Length - 1)
            {
                chargeCount = 0;
            }

            if (dissolvedObjects[i].gameObject.name == "Rock Mermaid Left")
            {
                charges[chargeCount].transform.position = dissolvedObjects[i].transform.position + new Vector3(0, dissolvedObjects[i].transform.lossyScale.y, -5f);
            }
            else if (dissolvedObjects[i].gameObject.name == "Rock Mermaid Right")
            {
                print("here");
                charges[chargeCount].transform.position = dissolvedObjects[i].transform.position + new Vector3(-14f, dissolvedObjects[i].transform.lossyScale.y * 1.02f, -5f);
            }
            else
            {
                charges[chargeCount].transform.position = dissolvedObjects[i].transform.position + new Vector3(0, dissolvedObjects[i].transform.lossyScale.y, -7);
            }
           
            
            charges[chargeCount].Play();
            Invoke("Dissolve", dissolveTime);
            ++chargeCount;
            print(dissolvedObjects[i].name); 
        }

        print(dissolvedObjects.Length);
    }

    private void SetObjectFalse()
    {
        dissolvers[j].Stop();
        dissolvedObjects[destroyCount].SetActive(false);
        --destroyed;
        ++j;
        
        ++destroyCount;

        if (destroyed < 1)
        {

            endSpeech.gameObject.GetComponent<VikingCrew.Tools.UI.ScriptedDialogueBehaviour>().enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void Dissolve()
    {
        if (dissolveCount == dissolvers.Length - 1)
        {
            dissolveCount = 0;
        }
   
        dissolvers[dissolveCount].transform.position = dissolvedObjects[dissolveCount].gameObject.transform.position + new Vector3(0, dissolvedObjects[dissolveCount].transform.localScale.y / 2, 0);
        dissolvers[dissolveCount].Play();

        charges[i].Stop();
        ++i;
        Invoke("SetObjectFalse", .1f);

        ++dissolveCount;
    }
}


