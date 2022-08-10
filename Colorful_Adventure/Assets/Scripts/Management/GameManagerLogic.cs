using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerLogic : MonoBehaviour
{
    public int totalBallCount;
    public int stage;
    public Text totalBallText;
    public Text playerBallText;

    void Awake() {
        //Screen.SetResolution(1920, 1920*16/9, false);
        totalBallText.text = "/ " + totalBallCount.ToString();
    }
    
    // Start is called before the first frame update
    public void GetBall(int count)
    {
        playerBallText.text = count.ToString();
    }
}
