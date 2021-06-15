using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static Material robotColor;

    // Start is called before the first frame update
    void Start()
    {
        if (robotColor != null)
        {
            gameObject.GetComponent<SkinnedMeshRenderer>().material = robotColor;
        }

    }

    public static void updateColor(Material mat)
    {
        robotColor = mat;
    }
}
