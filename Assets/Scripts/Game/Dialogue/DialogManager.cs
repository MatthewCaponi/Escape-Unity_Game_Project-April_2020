using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AddDialog();
    }

    private void AddDialog()
    {
        VikingCrew.Tools.UI.SpeechBubbleManager.Instance.AddSpeechBubble(transform, "Hello World");
    }
}
