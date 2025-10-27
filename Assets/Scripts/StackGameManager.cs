using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StackGameManager : MonoBehaviour
{
    [Header("게임 설정")]
    public GameObject blockPrefab;
    public float moveSpeed = 2f;
    public float moveRange = 3f;
    public float dropHeight = 3f;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    private GameObject currentBlock;
    private GameObject lastBlock;
    private int score = 0;
    private bool isGameOver = false;
    private float moveDirection = 1f;
    private float returnTimer = 0f;
    private bool isReturning = false;

    void Start()
    {
        Debug.Log("=== 게임 시작 ===");
        SpawnNewBlock();
        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver)
        {
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

        // 떨어진 블록 체크
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            if (block != null && block != currentBlock && block.transform.position.y < -7f)
            {
                Debug.Log("블록이 떨어짐 - 게임오버!");
                Destroy(block);
                GameOver();
                return;
            }
        }

        // 현재 블록 움직임
        if (currentBlock != null)
        {
            Vector3 pos = currentBlock.transform.position;
            pos.x += moveDirection * moveSpeed * Time.deltaTime;

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("스페이스바 - 블록 떨어뜨림");
                DropBlock();
            }
        }
    }

    void SpawnNewBlock()
    {
        if (blockPrefab == null)
        {
            Debug.LogError("Block Prefab 없음!");
            return;
        }

        float spawnY = dropHeight;
        if (lastBlock != null)
        {
            spawnY = lastBlock.transform.position.y + 1f;
        }

        currentBlock = Instantiate(blockPrefab, new Vector3(0, spawnY, 0), Quaternion.identity);

        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Debug.Log("블록 생성: " + currentBlock.name);
    }

    void DropBlock()
    {
        if (currentBlock == null) return;

        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        lastBlock = currentBlock;
        currentBlock = null;
        Invoke("SpawnNewBlock", 1f);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "점수: " + score;
        }
    }

    public void AddScore()
    {
        score++;
        UpdateScoreUI();
        Debug.Log("점수: " + score);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        isReturning = true;
        returnTimer = 0f;

        PlayerPrefs.SetInt("LastScore", score);

        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        PlayerPrefs.Save();

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
