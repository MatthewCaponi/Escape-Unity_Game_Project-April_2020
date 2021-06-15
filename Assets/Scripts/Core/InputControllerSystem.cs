using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputControllerSystem : MonoBehaviour, ISubmitHandler
{
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI nameQuestion;
    [SerializeField] GameObject next;

    private TMP_InputField nameText;

    bool nextIsActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        nameText = gameObject.GetComponent<TMP_InputField>();
        nameText.ActivateInputField();
        next.gameObject.SetActive(false);
    }

    private void ClearText()
    {
        print("here");
        nameText.text = "";
    }

    public void ApplyName()
    {
        nameQuestion.text = "Hi " + nameText.text;
      
        PlayerStats.SetName(nameText.text);

        ClearText();
        next.gameObject.SetActive(true);

        nextIsActive = true;
        gameObject.SetActive(false);
    }

    private void ClearUI()
    {
        nameText.gameObject.SetActive(false);
        nameQuestion.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (nameText.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            ApplyName();
        }  
    }

    public void OnSubmit(BaseEventData eventData)
    {
      
    }

    public bool getNextIsActive()
    {
        return nextIsActive;
    }
}
 