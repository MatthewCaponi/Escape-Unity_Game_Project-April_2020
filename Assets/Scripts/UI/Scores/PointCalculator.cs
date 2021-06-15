using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointCalculator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject play;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject mainMenu;
    [SerializeField] TextMeshProUGUI gameOver;
    [SerializeField] TextMeshProUGUI firstTitle;

    Score score;
    private bool once = false;
    
    // Start is called before the first frame update
    void Start()
    {
        firstTitle.gameObject.SetActive(false);
        play.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!once)
        {
            once = true;
            Invoke("StartTextCalc", 2);
        }
    }

    private void ScoreCalc()
    {
        print("here");
        score = GetComponent<Score>();
        scoreText.gameObject.GetComponent<TextMeshProUGUI>().SetText(Score.getScore() + " mythics!");
        Invoke("AppearButtons", 1);
    }

    private void AppearButtons()
    {
        play.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
    }

    private void StartTextCalc()
    {
        firstTitle.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);

        Invoke("ScoreCalc", 2);
    }
}
