using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalInputControl : MonoBehaviour
{
    [SerializeField] GameObject script;
    [SerializeField] GameObject menu;
    [SerializeField] bool allowEnter;
    [SerializeField] bool singleFunction;
    private bool allow = false;

    // Update is called once per frame
    void Update()
    {
        if (!singleFunction)
        {
            if (script.GetComponent<InputControllerSystem>().getNextIsActive())
            {
                Invoke("Allow", .1f);
            }
        }
       
       if (allow || allowEnter)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                menu.GetComponent<MenuScript>().LoadNextScene();
            }
        }
    }

    private void Allow()
    {
        allow = true;
    }
}
