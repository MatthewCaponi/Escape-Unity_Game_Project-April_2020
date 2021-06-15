using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScore;
    ArrayList ScoreItems;
    ArrayList ScoreList;
    ScoreItem maxScore;

    public GameObject ContentPanel;
    public GameObject ListItemPrefab;

    private TextAsset playerText;
 
    private int max = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxScore = new ScoreItem("", 0);
        ScoreItems = new ArrayList();
        ScoreList = new ArrayList();
       
        print("here0");
        ScoreList = PlayerStats.ReadString();
 
        if (ScoreList.Count == 0)
        {
            highScore.text = "No scores yet";
        }
        else
        {
            for (int i = 0; i < (ScoreList.Count); i += 2)
            {
                print("Here");
                string name;
                string score;

                name = (string)ScoreList[i];
                score = (string)ScoreList[i + 1];

                int intScorer = int.Parse(score);
                int intScore = 0;
                bool res = int.TryParse(score, out intScore);

                if (res)
                {
                    if (int.Parse(score) > max)
                    {
                        maxScore.Name = name;
                        maxScore.Score = intScorer;
                        max = int.Parse(score);
                        max = int.Parse(score);
                    }

                    ScoreItem scoreItem = new ScoreItem(name, intScorer);
                    ScoreItems.Add(scoreItem);
                }
                else
                {
                    print("int conversion not proper");
                }

            }

            if (max > 0)
            {
                highScore.text = maxScore.Name + ": " + maxScore.Score;
            }

            print(ScoreItems.Count);
            int count = 0;
            ScoreItems.Sort(new SortScores());
            foreach (ScoreItem scoreItem in ScoreItems)
            {
                print("Start");
                GameObject newScore = Instantiate(ListItemPrefab);
                ListItemController controller = newScore.GetComponent<ListItemController>();
                controller.Name.text = scoreItem.Name;
                controller.Score.text = scoreItem.Score.ToString();
                newScore.transform.parent = ContentPanel.transform;
                newScore.transform.localScale = Vector3.one;
            }
        }
    }

    public void ClearScores()
    {
        highScore.text = "No scores yet";
        GameObject.Find("Scroll View").SetActive(false);
        
        PlayerStats.ClearScores();
    }
}
