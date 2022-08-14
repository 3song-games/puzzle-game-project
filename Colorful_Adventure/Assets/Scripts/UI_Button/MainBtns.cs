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
                GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("LevelScene1");
                break;
            

            case BTNType.HowtoPlay:
                Debug.Log("Enjoy the tutorials");
                GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("Tutorial_1");
                break;

            case BTNType.Quit:
                GetComponent<AudioSource>().Play();
                GameQuit();
                break;
            
            case BTNType.Reset:
                SceneManager.LoadScene(manager.stage);
                GetComponent<AudioSource>().Play();
                break;
            
            case BTNType.Stop:
                Parent_stop.transform.Find("StopPopup").gameObject.SetActive(true);
                GetComponent<AudioSource>().Play();
                break;
            
            case BTNType.Return:
                Parent_stop.transform.Find("StopPopup").gameObject.SetActive(false);
                GetComponent<AudioSource>().Play();
                break;
            
            case BTNType.Continue:
                SceneManager.LoadScene((manager.stage) +1);
                GetComponent<AudioSource>().Play();
                break;
            
            case BTNType.BacktoMainMenu:
                SceneManager.LoadScene("MainMenu");
                GetComponent<AudioSource>().Play();
                break;

        }
    }

    public void GameQuit() {
        Application.Quit();
    }
}
