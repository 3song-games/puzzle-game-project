using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public PlayerController Player;
    public GameObject IfClear;
    public Text text1;
    public Text text2;
    int ballcnt;
    bool check = true; // �ݺ������� ������� ����
    void Start()
    {
        text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);
        StartCoroutine(FadeTextToFullAlpha(text1, 1));
    }
    private void Update()
    {
        ballcnt = Player.GetBall();
        if (ballcnt >= 1 && check)
            StartCoroutine(FadeTextToZeroAlpha(text1));
        if (IfClear.activeSelf == true)
        {
            text1.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);
            text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, 0);
        }
    }

    public IEnumerator FadeTextToFullAlpha(Text text, int idx) // ���İ� 0���� 1�� ��ȯ
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            ballcnt = Player.GetBall();
            if (ballcnt >= 1 && idx == 1)
                break;
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        //StartCoroutine(FadeTextToZeroAlpha());
    }

    public IEnumerator FadeTextToZeroAlpha(Text text)  // ���İ� 1���� 0���� ��ȯ
    {
        check = false;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 0.5f));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha(text2, 2));
    }
}
