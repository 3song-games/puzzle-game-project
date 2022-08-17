using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    int levelsUnlocked;
    public Button[] buttons;

    //public static bool frontClear=false;
    //public Scene Level10;
    // Start is called before the first frame update
    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 12);

        for(int i = 0; i < buttons.Length; i++) 
            {
                buttons[i].interactable = false;
            }
        //PlayerPrefs.DeleteAll(); //씬 잘 돌아가나 한번씩 확인하려면 잠금 해제 레벨 초기화 필요하므로 그때마다 이거 살려서 프로그램 돌리면 됨

        for(int i = 0; i < levelsUnlocked-11; i++) 
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
