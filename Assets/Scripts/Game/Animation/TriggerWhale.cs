using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWhale : MonoBehaviour
{
    [SerializeField] ParticleSystem appearEffects;

    [SerializeField] float waitTime;
    [SerializeField] bool waitToAppear;

    GameObject rocket;
    GameObject whaley; 
    Texture normalFace;
    Texture happyFace;
    Texture shockedFace;

    float count = 0;
    private int deathFrameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        whaley = gameObject;
        happyFace = (Texture)Resources.Load("Face06");
        rocket = GameObject.Find("LilRobotWhite");
        shockedFace = (Texture)Resources.Load("Face09");
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToAppear && count < 1)
        {
            gameObject.SetActive(false);
            ++count;
            Invoke("appear", waitTime);
        }
        
        if (rocket.GetComponent<Rocket>().getState() == Rocket.State.Dying && deathFrameCount < 1)
        {
            whaley.GetComponent<Animator>().enabled = false;
            whaley.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = shockedFace;
            whaley.gameObject.GetComponent<Animation>().Play("Happy");
            Invoke("PlayNormalFace", 1.5f);
            ++deathFrameCount;
        }

        else if ((rocket.GetComponent<Rocket>().getState() == Rocket.State.Portaling || rocket.GetComponent<Rocket>().getState() == Rocket.State.Transcending) && deathFrameCount < 1)
        {
            whaley.GetComponent<Animator>().enabled = false;
            whaley.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = happyFace;
            whaley.gameObject.GetComponent<Animation>().Play("Jump");

            ++deathFrameCount;
            Invoke("PlayNormalFace", 1.5f);
        }
    }

    public void PlayNormalFace()
    {
        normalFace = (Texture) Resources.Load("Face17");

        whaley.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = normalFace;
        whaley.GetComponent<Animator>().enabled = true;
    }

    public void appear()
    {
        gameObject.SetActive(true);
        appearEffects.transform.position = gameObject.transform.position;
        appearEffects.Play();
    }
}
