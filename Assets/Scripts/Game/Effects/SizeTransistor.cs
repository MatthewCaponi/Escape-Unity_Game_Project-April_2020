using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeTransistor : MonoBehaviour
{
    [SerializeField] GameObject[] creaturesToShrink;
    [SerializeField] ParticleSystem [] replacements;
    [SerializeField] float divideFactor;
    [SerializeField] float shrinkWait;

    Vector3[] replacementSize;

    int shrinkCount = 0;
    bool startShrink = false;

    // Start is called before the first frame update
    void Start()
    {
        replacementSize = new Vector3[replacements.Length];
        for (int i = 0; i < replacements.Length; ++i)
        {
            replacementSize[i] = replacements[i].transform.localScale;
            replacements[i].transform.localScale /= 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shrinkCount < creaturesToShrink.Length)
        {
            Invoke("StartShrink", shrinkWait);
            ++shrinkCount;
        }

        if (startShrink)
        {
            for (int i = 0; i < replacements.Length; ++i)
            {
                replacements[i].transform.position = creaturesToShrink[i].transform.position;
                replacements[i].Play();
            }

            for (int i = 0; i < creaturesToShrink.Length; ++i)
            {
                creaturesToShrink[i].transform.localScale /= divideFactor;
                replacements[i].transform.localScale *= divideFactor;

                if (replacements[i].transform.localScale.magnitude >= replacementSize[i].magnitude)
                {
                    startShrink = false;
                    creaturesToShrink[i].SetActive(false);
                } 
            }
        } 
    }

    public void StartShrink()
    {
        startShrink = true;
    }
}
