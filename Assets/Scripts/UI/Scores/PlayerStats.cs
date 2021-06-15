using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static string name;
    private static int levelTry = 1;
    private static bool practice = false;
    public static int writeCount = 0;
 
   // [MenuItem("Tools/Write file")]
    public static void WriteString()
    {
        if (!practice)
        {
            string path = Application.dataPath + "/player.txt";

            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(name);

            writer.WriteLine(Score.getScore());
            writer.Close();

            //Re-import the file to update the reference in the editor
            Resources.Load(path);
            TextAsset asset = (TextAsset)Resources.Load("player");

            //Print the text from the file 
        }
    }

  //  [MenuItem("Tools/Read file")]
    public static ArrayList ReadString()
    {
        ArrayList arr = new ArrayList();

        try
        {
            print("peek loop 1");
            string path = Application.dataPath + "/player.txt";
            Debug.Log(path);
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);

            print("peek loop 2");
            // Read and display lines from the file until the end of 
            // the file is reached.
            while (reader.Peek() >= 0)
            {
                print("peek loop");
                arr.Add(reader.ReadLine());
            }

            reader.Close();
        }
        catch(Exception e)
        {
            e.ToString();
        }

        print(arr.Count);

        return arr;
    }

    public static void ClearScores()
    {
        string path = Application.dataPath + "/player.txt";

        StreamReader reader = new StreamReader(path);
        var lines = File.ReadAllLines(path).ToArray();
        reader.Close();
        File.WriteAllLines(path, null);
    }

    public static void SetName(string playerName)
    {
        name = playerName;
        print(name);
    }

    public static int GetLevelTry()
    {
        return levelTry;
    }

    public static void UpdateLevelTry()
    {
        ++levelTry;  
    }

    public static void ResetLevelTry()
    {
        levelTry = 1;
    }
    public static void SetPractice(bool status)
    {
        if (status)
        {
            practice = true;
        }
        else
        {
            practice = false;
        }
    }

    public static bool GetPractice()
    {
        return practice;
    }
}
