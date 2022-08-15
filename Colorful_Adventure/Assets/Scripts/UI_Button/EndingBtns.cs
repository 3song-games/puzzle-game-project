using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndingBtns : MonoBehaviour
{
    public EndingType aboutEnding;
    public GameManagerLogic manager;
    public GameObject Korean;
    public GameObject English;
    
    public void OnBtnClick() {
        switch(aboutEnding)
        {
            case EndingType.MessageKor:
                Korean.SetActive(true);
                break;
            case EndingType.MessageEng:
                English.SetActive(true);
                break;
            
            case EndingType.EndingReturn:
                Korean.SetActive(false);
                English.SetActive(false);
                break;
            
            case EndingType.EndingHome:
                SceneManager.LoadScene("MainMenu");
                break;
            case EndingType.EndingQuit:
                GameQuit();
                break;
            
        }
    }
    public void GameQuit() {
        Application.Quit();
    }
}
