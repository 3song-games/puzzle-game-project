using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public void Pass() 
    {
        int currentStage= SceneManager.GetActiveScene().buildIndex;

        if(currentStage >= PlayerPrefs.GetInt("levelsUnlocked")) 
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentStage+1);
        }

        //ifclear창 활성화와 동시에 다음레벨잠금해제 수행됨. 
    }
}
