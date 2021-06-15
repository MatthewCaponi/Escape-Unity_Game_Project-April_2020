using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(0f, 0f, 0f);
    [SerializeField] Vector3 rotateVector = new Vector3(0f, 0f, 0f);
    [SerializeField] float period = 2f;
    [SerializeField] [Range(0, 1)] float rotateFactor;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] bool move;
    [SerializeField] bool rotate;
   
    Vector3 startingPos;
    Vector3 startingRot;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        startingRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        if (move)
        { 
            float cycles = Time.timeSinceLevelLoad / period; //grows continually from 0

            const float tau = Mathf.PI * 2; //about 6.28, all the way around cir
            float rawSinWave = Mathf.Sin(cycles * tau);

            movementFactor = rawSinWave / 2f + 0.5f;
            Vector3 offset = movementFactor * movementVector;
            transform.position = startingPos + offset;
            print(gameObject.name + " is moving " + period + "in " + movementVector.ToString() + " direction");
        }

        if (rotate)
        {   
            float cycles = Time.time / period; //grows continually from 0

            const float tau = Mathf.PI * 2; //about 6.28, all the way around cir
            float rawSinWave = Mathf.Sin(cycles * tau);

            rotateFactor = rawSinWave / 2f + 0.5f;
            Vector3 offset = rotateFactor * rotateVector;
            transform.eulerAngles = startingRot + offset;
            print(gameObject.name + " is rotating at " + period + "in " + movementVector.ToString() + " direction");
        }
    }
}
