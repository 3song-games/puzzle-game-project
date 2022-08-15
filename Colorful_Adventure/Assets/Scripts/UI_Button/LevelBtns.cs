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
            
            case LevelType.GotoLS2:
                GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("LevelScene2");
                break;

            
            case LevelType.GotoLS1:
                SceneManager.LoadScene("LevelScene1");
                GetComponent<AudioSource>().Play();
                break;

        }
    }
}
