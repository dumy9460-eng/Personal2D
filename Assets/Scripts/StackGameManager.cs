using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StackGameManager : MonoBehaviour
{
    [Header("���� ����")]
    public GameObject blockPrefab;          // ��� ������
    public float moveSpeed = 2f;            // ��� �̵� �ӵ�
    public float moveRange = 3f;            // ��� �̵� ����
    public float dropHeight = 4f;           // ��� ���� ����

    [Header("UI")]
    public TextMeshProUGUI scoreText;       // ���� �ؽ�Ʈ
    public GameObject gameOverPanel;        // ���� ���� �г�
    public TextMeshProUGUI finalScoreText;  // ���� ���� �ؽ�Ʈ

    private GameObject currentBlock;        // ���� �����̴� ���
    private GameObject lastBlock;           // ���������� ���� ���
    private int score = 0;                  // ���� ����
    private bool isGameOver = false;        // ���� ���� ����
    private float moveDirection = 1f;       // �̵� ����
    private float returnTimer = 0f;         // ���� Ÿ�̸�
    private bool isReturning = false;       // ���� ������

    void Start()
    {
        // ù ��° ��� ����
        SpawnNewBlock();
        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver)
        {
            // ���� ���� �� 2.5�� �� ���θ����� ����
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

        // ���� ��� �¿� �̵�
        if (currentBlock != null)
        {
            MoveCurrentBlock();

            // �����̽��ٷ� ��� ����߸���
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DropBlock();
            }
        }
    }

    void SpawnNewBlock()
    {
        // �� ��� ����
        float spawnY = dropHeight;
        if (lastBlock != null)
        {
            spawnY = lastBlock.transform.position.y + 1f;
        }

        currentBlock = Instantiate(blockPrefab, new Vector3(0, spawnY, 0), Quaternion.identity);

        // Rigidbody�� Kinematic���� (�������� �ʰ�)
        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void MoveCurrentBlock()
    {
        // ��� �¿� �̵�
        Vector3 pos = currentBlock.transform.position;
        pos.x += moveDirection * moveSpeed * Time.deltaTime;

        // ���� ���� �� ���� ��ȯ
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
        // ����� Dynamic���� ���� (��������)
        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        // ���� ����
        score++;
        UpdateScoreUI();

        // ���� ��� �غ�
        lastBlock = currentBlock;
        Invoke("SpawnNewBlock", 0.5f);  // 0.5�� �� �� ��� ����
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "����: " + score;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        isReturning = true;
        returnTimer = 0f;

        // ���� ����
        PlayerPrefs.SetInt("LastScore", score);

        // �ְ� ���� ����
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        PlayerPrefs.Save();

        // ���� ���� UI ǥ��
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
