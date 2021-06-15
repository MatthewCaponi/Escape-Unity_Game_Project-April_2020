using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStuff : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(10, 0, 0) * Time.deltaTime);
    }
}
