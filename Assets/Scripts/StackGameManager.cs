using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StackGameManager : MonoBehaviour
{
    [Header("게임 설정")]
    public GameObject blockPrefab;          // 블록 프리팹
    public float moveSpeed = 2f;            // 블록 이동 속도
    public float moveRange = 3f;            // 블록 이동 범위
    public float dropHeight = 4f;           // 블록 시작 높이

    [Header("UI")]
    public TextMeshProUGUI scoreText;       // 점수 텍스트
    public GameObject gameOverPanel;        // 게임 오버 패널
    public TextMeshProUGUI finalScoreText;  // 최종 점수 텍스트

    private GameObject currentBlock;        // 현재 움직이는 블록
    private GameObject lastBlock;           // 마지막으로 쌓인 블록
    private int score = 0;                  // 현재 점수
    private bool isGameOver = false;        // 게임 오버 상태
    private float moveDirection = 1f;       // 이동 방향
    private float returnTimer = 0f;         // 복귀 타이머
    private bool isReturning = false;       // 복귀 중인지

    void Start()
    {
        // 첫 번째 블록 생성
        SpawnNewBlock();
        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver)
        {
            // 게임 오버 후 2.5초 뒤 메인맵으로 복귀
            if (isReturning)
            {
                returnTimer += Time.deltaTime;
                if (returnTimer >= 2.5f)
                {
                    ReturnToMainMap();
                }
            }
            return;
        }

        // 현재 블록 좌우 이동
        if (currentBlock != null)
        {
            MoveCurrentBlock();

            // 스페이스바로 블록 떨어뜨리기
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DropBlock();
            }
        }
    }

    void SpawnNewBlock()
    {
        // 새 블록 생성
        float spawnY = dropHeight;
        if (lastBlock != null)
        {
            spawnY = lastBlock.transform.position.y + 1f;
        }

        currentBlock = Instantiate(blockPrefab, new Vector3(0, spawnY, 0), Quaternion.identity);

        // Rigidbody를 Kinematic으로 (떨어지지 않게)
        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void MoveCurrentBlock()
    {
        // 블록 좌우 이동
        Vector3 pos = currentBlock.transform.position;
        pos.x += moveDirection * moveSpeed * Time.deltaTime;

        // 범위 제한 및 방향 전환
        if (pos.x >= moveRange)
        {
            pos.x = moveRange;
            moveDirection = -1f;
        }
        else if (pos.x <= -moveRange)
        {
            pos.x = -moveRange;
            moveDirection = 1f;
        }

        currentBlock.transform.position = pos;
    }

    void DropBlock()
    {
        // 블록을 Dynamic으로 변경 (떨어지게)
        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        // 점수 증가
        score++;
        UpdateScoreUI();

        // 다음 블록 준비
        lastBlock = currentBlock;
        Invoke("SpawnNewBlock", 0.5f);  // 0.5초 후 새 블록 생성
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "점수: " + score;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        isReturning = true;
        returnTimer = 0f;

        // 점수 저장
        PlayerPrefs.SetInt("LastScore", score);

        // 최고 점수 갱신
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        PlayerPrefs.Save();

        // 게임 오버 UI 표시
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (finalScoreText != null)
        {
            finalScoreText.text = "최종 점수: " + score;
        }

        Debug.Log("게임 오버! 점수: " + score);
    }

    void ReturnToMainMap()
    {
        SceneManager.LoadScene("MainMap");
    }
}
