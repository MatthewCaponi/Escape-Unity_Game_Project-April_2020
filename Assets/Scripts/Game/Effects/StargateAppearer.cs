using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StargateAppearer : MonoBehaviour
{
    [SerializeField] bool portal;
    [SerializeField] bool gamePlayer;
    [SerializeField] GameObject portalTraveler;
    [SerializeField] GameObject portalObject;
    [SerializeField] ParticleSystem appearEffect;
    [SerializeField] float portalAppearTime;
    [SerializeField] float portalGenerateTime;

    private float invokeCount = 0;
    private float portalInvokeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        portalObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (portal && portalInvokeCount == 0)
        {
            ++portalInvokeCount;
            this.gameObject.SetActive(false);
            PortalGeneration(portalGenerateTime);
        }
        if (gamePlayer && invokeCount == 0)
        {
            ++invokeCount;
            portalTraveler.SetActive(false);
            PortalAppear(portalAppearTime);   
        }
    }

    private void PortalAppear(float seconds)
    {  
        Invoke("PortalAppear", seconds);   
    }

    private void PortalAppear()
    {
        portalTraveler.SetActive(true);
        appearEffect.Play();
    }

    private void PortalGeneration(float seconds)
    {
        Invoke("PortalGeneration", portalGenerateTime);
    }

    private void PortalGeneration()
    {
        portalObject.SetActive(true);
    }
}
