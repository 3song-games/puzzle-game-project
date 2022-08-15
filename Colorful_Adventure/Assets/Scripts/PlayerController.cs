using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private bool CanGoRight, CanGoLeft, CanGoUP, CanGoDown;
    private bool[,] movable = new bool[100, 100]; // 움직일 수 있는 좌표평면(Map)

    public GameManagerLogic manager;
    public LevelScript IFCLEAR_popup;
    public GameObject GetMap;
    private Transform[] MapArr;

    private Renderer playerColor;
    private Vector3 premove, targetPosition;
    public int ballCount; // 현재 먹은 색방울의 개수 (물방울타일 영향 받)
    private int finalGetBall; // UI 표시용. 플레이어가 get한 색방울 갯수 누적ver.
    public int GetBall()
    {
        return finalGetBall;
    }

    public GameObject IfClear; // 관문 통과 성공 시 성공 팝업창 활성화

    public GameObject IfFailed; // 실패 팝업창 활성화

    public AudioClip audMove; //움직일 때 효과음
    public AudioClip audBlock; //막힐 때 효과음
    public AudioClip audWater; //물타일 enter 효과음
    public AudioClip audCloud; //구름 exit 효과음
    public AudioClip audIce; //얼음 enter, exit 효과음
    public AudioClip audClear; //스테이지 클리어 시 효과음
    public AudioClip audFail; //스테이지 실패 시 효과음
    public AudioClip audColorBall; //색방울 흡수 시 효과음

    AudioSource aud;

    void Start()
    {
        Debug.Log("start");
        premove = Vector3.zero;
        targetPosition = transform.position;
        playerColor = gameObject.GetComponent<Renderer>();
        CanGoRight = true; CanGoLeft = true; CanGoUP = true; CanGoDown = true;
        ballCount = 0;
        this.aud = GetComponent<AudioSource>();
        // Map의 자식 오브젝트 배열
        MapArr = GetMap.GetComponentsInChildren<Transform>();

        // Map에 있는 좌표는 움직일 수 있음
        for (int i = 0; i < MapArr.Length; i++)
        {
            int x = (int)(MapArr[i].transform.position.x / 1.1f + 0.5f);
            int z = (int)(MapArr[i].transform.position.z / 1.1f + 0.5f);
            movable[x, z] = true;
        }

    }

    void MeetSpecialCube()
    { //특정기능타일 pass 여부 판단 함수
        for (int i = 0; i < MapArr.Length; i++)
        {
            if (MapArr[i].transform.CompareTag("ColorCube"))
            {
                int x = (int)(MapArr[i].transform.position.x / 1.1f + 0.5f);
                int z = (int)(MapArr[i].transform.position.z / 1.1f + 0.5f);
                movable[x, z] = false; // 기본적으로는 컬러큐브 pass 불가
                if (playerColor.material.color == MapArr[i].gameObject.GetComponent<Renderer>().material.color)
                // only if (플레이어 색 == 컬러큐브 색)
                {
                    movable[x, z] = true; // can pass
                }
            }

            if (MapArr[i].transform.CompareTag("Gate"))
            {
                int x = (int)(MapArr[i].transform.position.x / 1.1f + 0.5f);
                int z = (int)(MapArr[i].transform.position.z / 1.1f + 0.5f);
                movable[x, z] = true;
                if (playerColor.material.color != MapArr[i].transform.Find("default").gameObject.GetComponent<MeshRenderer>().material.color) 
                {
                    movable[x, z] = false;
                }

            }
        }
    }




    void Update()
    {
        MeetSpecialCube();
        // 움직일 수 없는 경우, do not move
        if (!movable[(int)(targetPosition.x / 1.1f + 0.5f), (int)(targetPosition.z / 1.1f + 0.5f)])
        {
            this.aud.PlayOneShot(this.audBlock);

            targetPosition = transform.position;
        }
        // reset point: 절댓값 차이가 작은 경우 강제로 position 지정
        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.01f && Mathf.Abs(transform.position.z - targetPosition.z) < 0.01f)
        {
            transform.position = targetPosition;
        }

        if (transform.position != targetPosition) // 부드럽게 이동
        {
            float speed;
            //얼음 너무 빨리 움직일 때
            if (Mathf.Abs(transform.position.x - targetPosition.x) > 1.5f || Mathf.Abs(transform.position.z - targetPosition.z) > 1.5f)
            {
                speed = 0.08f;
                Debug.Log("slow");
            }
            else speed = 0.4f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
        }
        //--------------------Movement--------------------//
        else if (Input.GetKeyDown(KeyCode.UpArrow) && CanGoUP)
        {
            premove = new Vector3(-1.1f, 0, 0);
            targetPosition = transform.position + new Vector3(-1.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && CanGoDown)
        {
            premove = new Vector3(1.1f, 0, 0);
            targetPosition = transform.position + new Vector3(1.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && CanGoLeft)
        {
            premove = new Vector3(0, 0, -1.1f);
            targetPosition = transform.position + new Vector3(0, 0, -1.1f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && CanGoRight)
        {
            premove = new Vector3(0, 0, 1.1f);
            targetPosition = transform.position + new Vector3(0, 0, 1.1f);
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
            finalGetBall += 1;
            manager.GetBall(finalGetBall);

            this.aud.PlayOneShot(this.audColorBall);//흡수 효과음
            this.aud.volume = 1.0f;

            if (ballCount == 1)
            {
                playerColor.material.color = ballColor.material.color;
            }

            if (ballCount == 2)
            {
                // red + blue = purple
                if ((playerColor.material.color == new Color32(231, 29, 34, 255) && ballColor.material.color == new Color32(0, 162, 231, 255)) || (playerColor.material.color == new Color32(0, 162, 231, 255) && ballColor.material.color == new Color32(231, 29, 34, 255)))
                {
                    playerColor.material.color = new Color32(141, 71, 151, 255); //purple
                }

                // red + yellow = orange
                if ((playerColor.material.color == new Color32(231, 29, 34, 255) && ballColor.material.color == new Color32(242, 232, 77, 255)) || (playerColor.material.color == new Color32(242, 232, 77, 255) && ballColor.material.color == new Color32(231, 29, 34, 255)))
                {
                    playerColor.material.color = new Color32(236, 101, 21, 255); //orange
                }

                //red + white = pink
                if ((playerColor.material.color == new Color32(231, 29, 34, 255) && ballColor.material.color == Color.white) || (playerColor.material.color == Color.white && ballColor.material.color == new Color32(231, 29, 34, 255)))
                {
                    playerColor.material.color = new Color32(244, 177, 184, 255); //pink
                }

                //blue + yellow = green
                if ((playerColor.material.color == new Color32(0, 162, 231, 255) && ballColor.material.color == new Color32(242, 232, 77, 255)) || (playerColor.material.color == new Color32(242, 232, 77, 255) && ballColor.material.color == new Color32(0, 162, 231, 255)))
                {
                    playerColor.material.color = new Color32(60, 175, 53, 255); //green
                }

                //blue + white = skyblue
                if ((playerColor.material.color == new Color32(0, 162, 231, 255) && ballColor.material.color == Color.white) || (playerColor.material.color == Color.white && ballColor.material.color == new Color32(0, 162, 231, 255)))
                {
                    playerColor.material.color = new Color32(185, 225, 235, 255); //skyblue
                }

                //yellow + white = bright yellow
                if ((playerColor.material.color == new Color32(242, 232, 77, 255) && ballColor.material.color == Color.white) || (playerColor.material.color == Color.white && ballColor.material.color == new Color32(242, 232, 77, 255)))
                {
                    playerColor.material.color = new Color32(245, 245, 174, 255); //bright yellow
                }

                //같은 색 만날 때 변하는 코드 없어도 된다.

            }
            if (ballCount >= 3)
            {
                // 3개 이상은 black
                playerColor.material.color = new Color32(5, 13, 38, 255);
            }

        }
    }
    private void OnCollisionStay(Collision other)
    {
        //--------------------화살표 이동 제한--------------------//
        if (other.gameObject.CompareTag("Arrow_U"))
        {
            Debug.Log("Arrow_Up_Collision");
            CanGoDown = false;
            CanGoLeft = false;
            CanGoRight = false;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                this.aud.PlayOneShot(this.audBlock);
            }
        }
        if (other.gameObject.CompareTag("Arrow_D"))
        {
            Debug.Log("Arrow_Down_Collision");
            CanGoUP = false;
            CanGoLeft = false;
            CanGoRight = false;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                this.aud.PlayOneShot(this.audBlock);
            }
        }
        if (other.gameObject.CompareTag("Arrow_L"))
        {
            Debug.Log("Arrow_Left_Collision");
            CanGoUP = false;
            CanGoDown = false;
            CanGoRight = false;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                this.aud.PlayOneShot(this.audBlock);
            }
        }
        if (other.gameObject.CompareTag("Arrow_R"))
        {
            Debug.Log("Arrow_Right_Collision");
            CanGoUP = false;
            CanGoDown = false;
            CanGoLeft = false;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                this.aud.PlayOneShot(this.audBlock);
            }
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        //-----------MoveSound출력----------------//
        if (other.gameObject.CompareTag("Cube") || other.gameObject.CompareTag("ColorCube") || other.gameObject.CompareTag("Cloud"))
        {

            this.aud.PlayOneShot(this.audMove);
            this.aud.volume = 0.3f;
        }
        
        if (other.gameObject.CompareTag("Arrow_R") || other.gameObject.CompareTag("Arrow_D") || other.gameObject.CompareTag("Arrow_U") || other.gameObject.CompareTag("Arrow_L"))
        {
            this.aud.PlayOneShot(this.audMove);
            this.aud.volume = 0.3f;
        }
        

        //--------------------Water Enter--------------------/
        if (other.gameObject.CompareTag("Water"))
        {
            Renderer waterColor = other.gameObject.GetComponent<MeshRenderer>();
            Renderer waterdropColor = other.transform.Find("default").gameObject.GetComponent<MeshRenderer>();
            if (waterColor.material.color != new Color32(24, 133, 122, 255) && ballCount > 0)
            {
                ballCount = 0;

                this.aud.PlayOneShot(this.audWater); //물 타일 오염 시 효과음 재생

                waterdropColor.material.color = new Color32(5, 13, 38, 255);
                playerColor.material.color = Color.white;
                Debug.Log("RuinedWater");
            }
        }
        //--------------------Ice Enter--------------------//
        if (other.gameObject.CompareTag("Ice"))
        {
            this.aud.PlayOneShot(this.audIce);

            Debug.Log("Ice");
            int x = (int)(targetPosition.x / 1.1f + 0.5f);
            int z = (int)(targetPosition.z / 1.1f + 0.5f);
            while (true)
            {
                x += (int)premove.x;
                z += (int)premove.z;
                if (movable[x, z]) targetPosition += premove;
                else break;
            }
        }

        //--------------------Gate Enter--------------------//
        if (other.gameObject.CompareTag("Gate"))
        {
            if (playerColor.material.color == other.transform.Find("default").gameObject.GetComponent<MeshRenderer>().material.color && manager.totalBallCount == 0)
            {

                this.aud.PlayOneShot(this.audClear); //클리어효과음

                Debug.Log("Next stage!!");
                //SceneManager.LoadScene((manager.stage)+1);
                IfClear.SetActive(true);
                IFCLEAR_popup.Pass();
            }
            else if (playerColor.material.color == other.transform.Find("default").gameObject.GetComponent<MeshRenderer>().material.color && manager.totalBallCount != 0)
            {

                this.aud.PlayOneShot(this.audFail); //실패효과음

                IfFailed.SetActive(true);
            }

            else if (playerColor.material.color != other.transform.Find("default").gameObject.GetComponent<MeshRenderer>().material.color)
            {
                movable[(int)(other.transform.position.x / 1.1f + 0.5f), (int)(other.transform.position.y / 1.1f + 0.5f)] = false;
            }

        }
    }
    private void OnCollisionExit(Collision other)
    {

        //--------------------화살표 이동 제한 해제--------------------//
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
            this.aud.PlayOneShot(this.audCloud);

            Debug.Log("CloudOut");
            other.gameObject.SetActive(false);
            int x = (int)(other.transform.position.x / 1.1f + 0.5f);
            int z = (int)(other.transform.position.z / 1.1f + 0.5f);
            movable[x, z] = false; // 구름타일로 이제 움직일 수 없음
        }
        //--------------------Water Exit--------------------//
        if (other.gameObject.CompareTag("Water"))
        {
            Renderer waterColor = other.gameObject.GetComponent<MeshRenderer>();
            Renderer waterdropColor = other.transform.Find("default").gameObject.GetComponent<MeshRenderer>();
            if (waterdropColor.material.color == new Color32(5, 13, 38, 255)) {
                waterColor.material.color = new Color32(24, 133, 122, 255);
                int x = (int)(other.transform.position.x / 1.1f + 0.5f);
                int z = (int)(other.transform.position.z / 1.1f + 0.5f);
                movable[x, z] = false; // 물타일로 이제 움직일 수 없음
            }
        }
        //--------------------Ice Exit--------------------//
        if (other.gameObject.CompareTag("Ice"))
        {
            this.aud.PlayOneShot(this.audIce);

            Debug.Log("Ice");
            int x = (int)(targetPosition.x / 1.1f + 0.5f);
            int z = (int)(targetPosition.z / 1.1f + 0.5f);
            while (true)
            {
                x += (int)premove.x;
                z += (int)premove.z;
                if (movable[x, z]) targetPosition += premove;
                else break;
            }
        }
        if (other.gameObject.CompareTag("ColorBall Cube"))
        {
            Debug.Log("ColorBall on Cube");
            other.gameObject.tag = "Cube";
        }
        //색방울 타일에서 색방울이 없어진 뒤 기존 큐브로 변경
    }
}