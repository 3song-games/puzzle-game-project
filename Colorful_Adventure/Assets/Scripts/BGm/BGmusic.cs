using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGmusic : MonoBehaviour
{
    public static BGmusic instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Ending")
            BGmusic.instance.GetComponent<AudioSource>().Pause();

    }
}
