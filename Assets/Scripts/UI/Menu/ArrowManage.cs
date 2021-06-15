using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowManage : MonoBehaviour
{
    [SerializeField] GameObject robot;
    [SerializeField] GameObject parentRobot;

    Stack<Object> leftStack;
    Stack<Object> rightStack;
    Stack<Object> tempLeftStack;
    Stack<Object> tempRightStack;

    Material currentColor;

    Object[] tempList;
    Object[] reverseList;

    Stack<Object> tempStorage;

    bool left;

    //private GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        print("here1");
        tempList = Resources.LoadAll("RobotColor");

        reverseList = new Object[tempList.Length];
        for (int i = tempList.Length - 1; i >= 0; --i)
        {
            reverseList[i] = tempList[(tempList.Length - 1) - i];
        }

        print("here2");

        leftStack = new Stack<Object>();
        rightStack = new Stack<Object>();
        for (int i = 0; i < tempList.Length; ++i)
        {
            
            leftStack.Push(tempList[i]);

            rightStack.Push(reverseList[i]);
        }

        print("Left Stack: " + leftStack.Count);
        print("Right Stack: " + rightStack.Count);

        tempStorage = new Stack<Object>();
        currentColor = null; 
        //  robot = GameObject.Find("LilRobotWhite");
    }

    // Update is called once per frame
    public void leftClick()
    {
        if (tempStorage.Count > 0)
        {
            rightStack.Push(tempStorage.Pop());
            
        }
  
        if (leftStack.Count > 0)
        {
            print("here");
            Material tempShader = (Material)leftStack.Pop();
            print("changed color to : " + tempShader.name);
            robot.GetComponent<SkinnedMeshRenderer>().material = tempShader;
            tempStorage.Push(tempShader);
            currentColor = tempShader;
            print("Left Stack: " + leftStack.Count);
            print("Right Stack: " + rightStack.Count);
        }
        else
        {
            ReplenishStack();

            Material tempShader = (Material)leftStack.Pop();
            print("changed color to : " + tempShader.name);
            robot.GetComponent<SkinnedMeshRenderer>().material = tempShader;
            tempStorage.Push(tempShader);
            currentColor = tempShader;
            print("Left Stack: " + leftStack.Count);
            print("Right Stack: " + rightStack.Count);
        }

        left = true;
    }

    public void rightClick()
    {
        if (tempStorage.Count > 0)
        {
           leftStack.Push(tempStorage.Pop());  
        }
        else
        {
            leftStack.Push(rightStack.Pop());
        }

        if (rightStack.Count > 0)
        {
            print("here");
            Material tempShader = (Material)rightStack.Pop();
            print("changed color to : " + tempShader.name);
            robot.GetComponent<SkinnedMeshRenderer>().material = tempShader;
            tempStorage.Push(tempShader);
            currentColor = tempShader;
            print("Left Stack: " + leftStack.Count);
            print("Right Stack: " + rightStack.Count);
        }
        else
        {
            ReplenishStack();
            print("here");
            Material tempShader = (Material)rightStack.Pop();
            print("changed color to : " + tempShader.name);
            robot.GetComponent<SkinnedMeshRenderer>().material = tempShader;
            tempStorage.Push(tempShader);
            currentColor = tempShader;
            print("Left Stack: " + leftStack.Count);
            print("Right Stack: " + rightStack.Count);
        }

        left = false;
    }

    private void ReplenishStack()
    {
        leftStack.Clear();
        rightStack.Clear();

        for (int i = 0; i < tempList.Length; ++i)
        {
            leftStack.Push(tempList[i]);
        }
       
        for (int i = 0; i < reverseList.Length; ++i)
        {
            rightStack.Push(reverseList[i]);
        }
    }

    public void Submit()
    {
        PlayerInfo.updateColor(currentColor);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }
}
