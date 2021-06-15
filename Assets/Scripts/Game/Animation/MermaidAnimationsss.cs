using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidAnimationsss : MonoBehaviour
{
    Animation animate;
    // Start is called before the first frame update
    void Start()
    {
        animate = gameObject.transform.GetComponent<Animation>();
        animate.wrapMode = WrapMode.Loop;
    }

    public void stopAnimations()
    {
        animate.Stop();
    }

    public void playDieAnimation()
    {
        animate.Stop();
        animate.Play("sit");
    }
}
