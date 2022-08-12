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

        Debug.Log("LEVEL "+(PlayerPrefs.GetInt("levelsUnlocked")-11)+ "UNLOCKED");
        SceneManager.LoadScene((currentStage)+1);
    }
}
