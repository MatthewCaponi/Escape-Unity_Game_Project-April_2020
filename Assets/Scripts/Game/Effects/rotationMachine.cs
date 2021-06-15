using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationMachine : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationSpeedy;
    [SerializeField] float rotationSpeedZ;
    [SerializeField] int direction;

    private Vector3 eulerAngle;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed, rotationSpeedy, rotationSpeedZ);
    }
}
