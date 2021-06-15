using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MovementUpScript : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(0f, 0f, 0f);
    [SerializeField] float period = 2f;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] private bool move;
    [SerializeField] float delay = 0f;
    [SerializeField] float startDelay = 0f;

    Vector3 startingPos;
    private float cycles;
    private float cycleFactor;
    private bool started = false;
    private bool firstFrame = true;
    private bool printedOrNot = false;
    private bool reachPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        cycleFactor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (period <= Mathf.Epsilon)
            {
                return;
            }

            cycles = Time.timeSinceLevelLoad / period; //grows continually from 0

            //const float tau = Mathf.PI * 2; //about 6.28, all the way around cir
            // float rawSinWave = Mathf.Sin(cycles * tau);

            //movementFactor = rawSinWave / 2f + 0.5f;
            float rawSinWave = Mathf.Sin(cycles);
            movementFactor = cycles;
            Vector3 offset = movementFactor * movementVector;
            transform.position = startingPos + offset;
        }

        if (movementFactor > 1)
        {
            reachPoint = true;
        }
    } 
}
