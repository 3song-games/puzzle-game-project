using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    //public LevelManager levelmanager;
    public void Pass() 
    {
        //levelmanager.num = 1;
        int currentStage= SceneManager.GetActiveScene().buildIndex;

        if(currentStage >= PlayerPrefs.GetInt("levelsUnlocked")) 
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentStage+1);
        }

        /*if(currentStage >= 21) {
            levelmanager.num = 2;
        }*/

        //ifclear창 활성화와 동시에 다음레벨잠금해제 수행됨. (원래는 continue button누를 때를 기준으로 잠금해제 수행하도록 했는데 수정함
        //컨티뉴 버튼은 예전처럼, 누르면 그저 다음레벨로 이동하는 역할만 수행함)
    }
}
