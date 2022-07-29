using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private bool CanGoRight, CanGoLeft, CanGoUP, CanGoDown;

    private int ballCount;
    public GameManagerLogic manager;

    private Renderer playerColor;
    Vector3 startPosition, targetPosition;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("start");
        startPosition = transform.position;
        targetPosition = transform.position;
        playerColor = gameObject.GetComponent<Renderer>();
        CanGoRight = true; CanGoLeft = true; CanGoUP = true; CanGoDown = true;
        ballCount = 0;
    }



    void Update()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (startPosition != targetPosition) // 부드럽게 이동
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }

        //--------------------Movement--------------------//
        if (Input.GetKeyDown(KeyCode.UpArrow) && CanGoUP)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(-1.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && CanGoDown)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(1.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && CanGoLeft)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(0, 0, -1.1f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && CanGoRight)
        {
            startPosition = transform.position;
            targetPosition = transform.position + new Vector3(1.1f, 0, 1.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer ballColor = other.GetComponent<MeshRenderer>();

        if (other.gameObject.CompareTag("Ball"))
        {
            manager.totalBallCount--;
            other.gameObject.SetActive(false);
            ballCount += 1;

            if (ballCount == 1)
            {
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

                //같은 색 만날 때 변하는 코드 없어도 된다.

            }
            if (ballCount >= 3)
            {
                // 3
                playerColor.material.color = Color.black;
            }

        }
    }
    private void OnCollisionStay(Collision other)
    {
        //--------------------ȭ��ǥ ������ ����--------------------//
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
        //플레이어의 움직임을 망가뜨리지 않으면서도 맵 밖으로 이동하지 못하도록 하는 기능
        //CheckPoint = 타일과 같은 큐브 모양(높이만 더 높음)의, 맵 테두리를 두르고 있는 것들
        if (other.gameObject.CompareTag("CheckPoint") && targetPosition.x > manager.maxX) { // no more right
            targetPosition = new Vector3(manager.maxX, targetPosition.y, targetPosition.z);
        }

        if(other.gameObject.CompareTag("CheckPoint") &&targetPosition.x < manager.minX) { // no more left 
            targetPosition = new Vector3(manager.minX, targetPosition.y, targetPosition.z);

        }

        if (other.gameObject.CompareTag("CheckPoint") &&targetPosition.z > manager.maxZ) { // no more up
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, manager.maxZ);
        }

        if (other.gameObject.CompareTag("CheckPoint") &&targetPosition.z < manager.minZ) { // no more down
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, manager.minZ);
        }

        //--------------------Water Enter--------------------//
        if (other.gameObject.CompareTag("Water"))
        {
            Renderer waterColor = other.gameObject.GetComponent<MeshRenderer>();
            if (waterColor.material.color != Color.black)
            {
                playerColor.material.color = Color.white;
            }
        }

        //--------------------Gate Enter--------------------//
        if(other.gameObject.name == "Gate_green") {
            if(playerColor.material.color == new Color32(0, 128, 0, 255) && manager.totalBallCount == 0) { // & 조건 하나 더 추가- 색방울 모두 흡수 여부 (최종 갯수와 일치하는지 여부)
                Debug.Log("Next stage!!");
                //SceneManager.LoadScene("GameScene_" + (manager.stage + 1).ToString());

            }
            else {
                Debug.Log("Retry!");
                //SceneManager.LoadScene("GameScene_" + manager.stage.ToString());

            }

        }
    }
    private void OnCollisionExit(Collision other)
    {

        //--------------------ȭ��ǥ ������ ���� ����--------------------//
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
            if(ballCount > 0) { //색방울을 아직 하나도 먹지 않은 상태에서 물 타일 건들 경우엔 아무 일 x이므로
            // 색방울을 하나라도 먹은 이후여야 물 타일이 작동된다는 조건 추가
            Renderer waterColor = other.gameObject.GetComponent<MeshRenderer>();
            waterColor.material.color = Color.black;
            ballCount = 0;
            }
        }
    }
}
