using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageManager : MonoBehaviour
{
    public void GotoIntro() {
        SceneManager.LoadScene("IntroLoading");
    }
    // Start is called before the first frame update
}
