using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelBtns : MonoBehaviour
{
    public LevelType chooseLevel;

    public GameObject Parent1;
    public GameObject Parent2;

    public void OnBtnClick() {
        switch(chooseLevel)
        {
            
            case LevelType.GotoLS2:
                GetComponent<AudioSource>().Play();
                Parent1.transform.Find("Levels1").gameObject.SetActive(false);
                Parent2.transform.Find("Levels2").gameObject.SetActive(true);
                //transform.Find("default").gameObject.

                //SceneManager.LoadScene("LevelScene2");
                break;

            
            case LevelType.GotoLS1:
                //SceneManager.LoadScene("LevelScene1");
                Parent1.transform.Find("Levels1").gameObject.SetActive(true);
                Parent2.transform.Find("Levels2").gameObject.SetActive(false);
                GetComponent<AudioSource>().Play();
                break;

        }
    }
}
