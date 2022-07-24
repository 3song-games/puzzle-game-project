using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool CanGoRight, CanGoLeft, CanGoUP, CanGoDown;
    private int ballCount;

    private Renderer playerColor;
    Vector3 startPosition, targetPosition;

    void Start()
    {
        Debug.Log("start");
        startPosition = transform.position;
        targetPosition = transform.position;
        playerColor = gameObject.GetComponent<Renderer>();
        CanGoRight = true; CanGoLeft = true; CanGoUP = true; CanGoDown = true;
        ballCount = 0;
    }


    void Update()
    {
        if (startPosition != targetPosition) //부드럽게 움직이게 하는 방법
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }

        //--------------------Movement--------------------//
        if (Input.GetKeyDown(KeyCode.UpArrow) && CanGoUP)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(0, 0, 1.1f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && CanGoDown)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(0, 0, -1.1f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && CanGoLeft)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(-1.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && CanGoRight)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(1.1f, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer ballColor = other.GetComponent<MeshRenderer>();

        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.SetActive(false);
            ballCount += 1;

            if (ballCount == 1)
            {
                //플레이어의 material 컬러 == 부딪힌 ball의 material color의 색상
                playerColor.material.color = ballColor.material.color;
            }

            if (ballCount == 2)
            {
                // red + blue = purple
                if ((playerColor.material.color == Color.red && ballColor.material.color == Color.blue) || (playerColor.material.color == Color.blue && ballColor.material.color == Color.red))
                {
                    playerColor.material.color = new Color32(106, 13, 173, 255); //purple
                }

                // red + yellow = orange
                if ((playerColor.material.color == Color.red && ballColor.material.color == new Color32(255, 255, 0, 255)) || (playerColor.material.color == new Color32(255, 255, 0, 255) && ballColor.material.color == Color.red))
                {
                    playerColor.material.color = new Color32(255, 165, 0, 255); //orange
                }

                //red + white = pink
                if ((playerColor.material.color == Color.red && ballColor.material.color == Color.white) || (playerColor.material.color == Color.white && ballColor.material.color == Color.red))
                {
                    playerColor.material.color = new Color32(255, 192, 203, 255); //pink
                }

                //blue + yellow = green
                if ((playerColor.material.color == Color.blue && ballColor.material.color == new Color32(255, 255, 0, 255)) || (playerColor.material.color == new Color32(255, 255, 0, 255) && ballColor.material.color == Color.blue))
                {
                    playerColor.material.color = new Color32(0, 128, 0, 255); //green
                }

                //blue + white = skyblue
                if ((playerColor.material.color == Color.blue && ballColor.material.color == Color.white) || (playerColor.material.color == Color.white && ballColor.material.color == Color.blue))
                {
                    playerColor.material.color = new Color32(135, 206, 235, 255); //skyblue
                }

                //yellow + white = bright yellow
                if ((playerColor.material.color == new Color32(255, 255, 0, 255) && ballColor.material.color == Color.white) || (playerColor.material.color == Color.white && ballColor.material.color == new Color32(255, 255, 0, 255)))
                {
                    playerColor.material.color = new Color32(255, 255, 153, 255); //bright yellow
                }

                //같은 색깔이면 그대로는 코드 짤 필요 없음

            }
            if (ballCount >= 3)
            {
                // 같은 색방울이라도, 3개 먹으면 플레이어 컬러는 검정색 됨.
                playerColor.material.color = Color.black;
            }

        }
    }
    private void OnCollisionStay(Collision other)
    {
        //--------------------화살표 움직임 제한--------------------//
        if (other.gameObject.CompareTag("Arrow_U"))
        {
            Debug.Log("Arrow_Up_Collision");
            CanGoDown = false;
            CanGoLeft = false;
            CanGoRight = false;
        }
        if (other.gameObject.CompareTag("Arrow_D"))
        {
            Debug.Log("Arrow_Down_Collision");
            CanGoUP = false;
            CanGoLeft = false;
            CanGoRight = false;
        }
        if (other.gameObject.CompareTag("Arrow_L"))
        {
            Debug.Log("Arrow_Left_Collision");
            CanGoUP = false;
            CanGoDown = false;
            CanGoRight = false;
        }
        if (other.gameObject.CompareTag("Arrow_R"))
        {
            Debug.Log("Arrow_Right_Collision");
            CanGoUP = false;
            CanGoDown = false;
            CanGoLeft = false;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //--------------------Water Enter--------------------//
        if (other.gameObject.CompareTag("Water"))
        {
            Renderer waterColor = other.gameObject.GetComponent<MeshRenderer>();
            if (waterColor.material.color != Color.black)
            {
                playerColor.material.color = Color.white;
                ballCount = 0;
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        //--------------------화살표 움직임 제한 해제--------------------//
        if (other.gameObject.CompareTag("Arrow_U") || other.gameObject.CompareTag("Arrow_D") || other.gameObject.CompareTag("Arrow_L") || other.gameObject.CompareTag("Arrow_R"))
        {
            Debug.Log("YouCanGoAnywhere");
            CanGoRight = true;
            CanGoLeft = true;
            CanGoUP = true;
            CanGoDown = true;
        }
        //--------------------Cloud--------------------//
        if (other.gameObject.CompareTag("Cloud"))
        {
            Debug.Log("CloudOut");
            other.gameObject.SetActive(false);
        }
        //--------------------Water--------------------//
        if (other.gameObject.CompareTag("Water"))
        {
            Renderer waterColor = other.gameObject.GetComponent<MeshRenderer>();
            waterColor.material.color = Color.black;
        }
    }
}
