using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StackGameManager : MonoBehaviour
{
    [Header("���� ����")]
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
        Debug.Log("=== ���� ���� ===");
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

        // ������ ��� üũ
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            if (block != null && block != currentBlock && block.transform.position.y < -7f)
            {
                Debug.Log("����� ������ - ���ӿ���!");
                Destroy(block);
                GameOver();
                return;
            }
        }

        // ���� ��� ������
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
                Debug.Log("�����̽��� - ��� ����߸�");
                DropBlock();
            }
        }
    }

    void SpawnNewBlock()
    {
        if (blockPrefab == null)
        {
            Debug.LogError("Block Prefab ����!");
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

        Debug.Log("��� ����: " + currentBlock.name);
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
            scoreText.text = "����: " + score;
        }
    }

    public void AddScore()
    {
        score++;
        UpdateScoreUI();
        Debug.Log("����: " + score);
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
            finalScoreText.text = "���� ����: " + score;
        }

        Debug.Log("���� ����! ����: " + score);
    }

    void ReturnToMainMap()
    {
        SceneManager.LoadScene("MainMap");
    }
}
