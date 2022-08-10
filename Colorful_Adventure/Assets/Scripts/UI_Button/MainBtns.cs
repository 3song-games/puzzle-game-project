using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainBtns : MonoBehaviour {

    public BTNType currentType;
    public GameManagerLogic manager;

    public GameObject Parent_stop;

    private void Start() {
    }
    public void OnBtnClick() {
        switch(currentType)
        {
            case BTNType.GameStart:
                Debug.Log("and then choose the level");
                SceneManager.LoadScene("LevelScene");
                break;
            

            case BTNType.HowtoPlay:
                Debug.Log("Enjoy the tutorials");
                SceneManager.LoadScene("Tutorial_1");
                break;

            case BTNType.Quit:
                GameQuit();
                break;
            
            case BTNType.Reset:
                SceneManager.LoadScene(manager.stage);
                break;
            
            case BTNType.Stop:
                Parent_stop.transform.Find("StopPopup").gameObject.SetActive(true);
                break;
            
            case BTNType.Return:
                Parent_stop.transform.Find("StopPopup").gameObject.SetActive(false);
                break;
            
            case BTNType.Continue:
                SceneManager.LoadScene((manager.stage) +1);
                break;
            
            case BTNType.BacktoMainMenu:
                SceneManager.LoadScene("MainMenu");
                break;

        }
    }

    public void GameQuit() {
        Application.Quit();
    }
}
