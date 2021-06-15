using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Camera cam2;

    [SerializeField] bool cinematicStart;
    [SerializeField] int cameraDelay;
    [SerializeField] float cameraMove;
    [SerializeField] float cameraRotate;

    private Camera mainCamComp;
    private Camera secondCamComp;

    bool cameraMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamComp = mainCam.gameObject.GetComponent<Camera>();
        mainCamComp.enabled = true;

        //if (cinematicStart)
        //{
        //    cinematicScene();
        //} 
    }

    // Update is called once per frame
    void Update()
    {
        //if (cameraMoving)
        //{
            //while (secondCamComp.transform.forward.magnitude > -58)
            //{
        mainCam.transform.Translate(Vector3.forward, Space.World);
                //Camera.current.transform.Rotate(0, 0, cameraRotate);
           // }

            //cameraMoving = false;
            //secondCamComp.enabled = false;
            //mainCamComp.enabled = true;
        //}
    }

    private void cinematicScene()
    {
        mainCamComp.enabled = false;
        secondCamComp.enabled = true;
        Invoke("CameraRestore", cameraDelay);
    }

    private void CameraRestore()
    {
        cameraMoving = true;
    }
}
