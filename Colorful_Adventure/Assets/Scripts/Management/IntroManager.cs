using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Image backscene, logo, mission = null;
    [SerializeField] Slider silder = null;
    [SerializeField] TextMeshProUGUI text_percentage = null;

    private float time_loading = 6;
    private float time_current;
    private float time_start;
    //로딩 중 팀 로고도 보이게 할 거면 여기에 같은 맥락으로 추가하면 됨
    // Start is called before the first frame update
    void Start()
    {
        time_current = time_loading;
        time_start = Time.time;
        Set_FillAmount(0);
        StartCoroutine(FadeToFull(1.5f, backscene, logo, mission));
    }

    // Update is called once per frame
    void Update()
    {
        Check_Loading();
    }

    public IEnumerator FadeToFull(float t, Image i, Image j, Image z) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        j.color = new Color(j.color.r, j.color.g, j.color.b, 0);
        z.color = new Color(z.color.r, z.color.g, z.color.b, 0);

        /*while (i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime/t));
            yield return null;
        }*/

        while (j.color.a < 1.0f) {
            j.color = new Color(j.color.r, j.color.g, j.color.b, j.color.a + (Time.deltaTime / t));
            yield return null;
        }
        j.color = new Color(j.color.r, j.color.g, j.color.b, 1);
        while (j.color.a > 0.0f)
        {
            j.color = new Color(j.color.r, j.color.g, j.color.b, j.color.a - (Time.deltaTime / t));
            yield return null;
        }
        j.color = new Color(j.color.r, j.color.g, j.color.b, 0);

        while (z.color.a < 1.0f) {
            z.color = new Color(z.color.r, z.color.g, z.color.b, z.color.a + (Time.deltaTime / t));
            yield return null;
        }
        z.color = new Color(z.color.r, z.color.g, z.color.b, 1);
    }

    private void Check_Loading() {
        time_current = Time.time - time_start;
        if(time_current < time_loading) {
            Set_FillAmount(time_current / time_loading);
        }

        else {
            End_Loading();
        }
    }

    private void End_Loading() {
        Set_FillAmount(1);
        SceneManager.LoadScene("MainMenu");
    }

    private void Set_FillAmount(float _value) {
        silder.value = _value;
        string txt = (_value.Equals(1) ? "Finished.. " : "Loading.. ")
        + (_value.ToString("P"));
        text_percentage.text = txt;
    }
}
