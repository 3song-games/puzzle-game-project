using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelBtns : MonoBehaviour
{
    public LevelType chooseLevel;
    public void OnBtnClick() {
        switch(chooseLevel)
        {
            case LevelType.L1btn:
                //SceneManager.LoadScene("Level_1");
                Debug.Log("start Level_1");
                break;
            case LevelType.L2btn:
                Debug.Log("start Level_2");
                break;
            case LevelType.L3btn:
                Debug.Log("start Level_3");
                break;
            case LevelType.L4btn:
                Debug.Log("start Level_4");
                break;
            case LevelType.L5btn:
                Debug.Log("start Level_5");
                break;
            
            case LevelType.L6btn:
                Debug.Log("start Level_6");
                break;
            case LevelType.L7btn:
                Debug.Log("start Level_7");
                break;
            case LevelType.L8btn:
                Debug.Log("start Level_8");
                break;
            case LevelType.L9btn:
                Debug.Log("start Level_9");
                break;
            case LevelType.L10btn:
                Debug.Log("start Level_10");
                break;
            
            case LevelType.L11btn:
                Debug.Log("start Level_11");
                break;
            case LevelType.L12btn:
                Debug.Log("start Level_12");
                break;
            case LevelType.L13btn:
                Debug.Log("start Level_13");
                break;
            case LevelType.L14btn:
                Debug.Log("start Level_14");
                break;
            case LevelType.L15btn:
                Debug.Log("start Level_15");
                break;
            
            case LevelType.L16btn:
                Debug.Log("start Level_16");
                break;
            case LevelType.L17btn:
                Debug.Log("start Level_17");
                break;
            case LevelType.L18btn:
                Debug.Log("start Level_18");
                break;
            case LevelType.L19btn:
                Debug.Log("start Level_19");
                break;
            case LevelType.L20btn:
                Debug.Log("start Level_20");
                break;
            
            case LevelType.GotoLS2:
                SceneManager.LoadScene("LevelScene2");
                break;
            
            case LevelType.GotoLS1:
                SceneManager.LoadScene("LevelScene1");
                break;
        }
    }
}
