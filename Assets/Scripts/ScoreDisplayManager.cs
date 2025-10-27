using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayManager : MonoBehaviour
{
    [Header("UI ����")]
    public TextMeshProUGUI lastScoreText;   // �ֱ� ���� �ؽ�Ʈ
    public TextMeshProUGUI highScoreText;   // �ְ� ���� �ؽ�Ʈ

    void Start()
    {
        UpdateScoreDisplay();
    }

    void OnEnable()
    {
        // ���� Ȱ��ȭ�� ������ ���� ������Ʈ
        UpdateScoreDisplay();
    }

    // ���� ǥ�� ������Ʈ
    public void UpdateScoreDisplay()
    {
        // PlayerPrefs���� ���� �ҷ�����
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // UI�� ǥ��
        if (lastScoreText != null)
        {
            lastScoreText.text = "�ֱ� ����: " + lastScore;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "�ְ� ����: " + highScore;
        }

        Debug.Log($"���� ǥ�� ������Ʈ - �ֱ�: {lastScore}, �ְ�: {highScore}");
    }

    // ���� �ʱ�ȭ (�׽�Ʈ��)
    public void ResetScores()
    {
        PlayerPrefs.DeleteKey("LastScore");
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        UpdateScoreDisplay();
        Debug.Log("������ �ʱ�ȭ�Ǿ����ϴ�.");
    }
}
