using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyWhale : MonoBehaviour
{
    [SerializeField] ParticleSystem appearPortal;
    private float appearCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        print("Update");
        if (appearCount == 0)
        {
            Invoke("Appear", 6);
            ++appearCount;
        }
    }

    private void Appear()
    {
        gameObject.SetActive(true);
        appearPortal.Play();
    }
}
