using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayManager : MonoBehaviour
{
    [Header("UI 참조")]
    public TextMeshProUGUI lastScoreText;   // 최근 점수 텍스트
    public TextMeshProUGUI highScoreText;   // 최고 점수 텍스트

    void Start()
    {
        UpdateScoreDisplay();
    }

    void OnEnable()
    {
        // 씬이 활성화될 때마다 점수 업데이트
        UpdateScoreDisplay();
    }

    // 점수 표시 업데이트
    public void UpdateScoreDisplay()
    {
        // PlayerPrefs에서 점수 불러오기
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // UI에 표시
        if (lastScoreText != null)
        {
            lastScoreText.text = "최근 점수: " + lastScore;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "최고 점수: " + highScore;
        }

        Debug.Log($"점수 표시 업데이트 - 최근: {lastScore}, 최고: {highScore}");
    }

    // 점수 초기화 (테스트용)
    public void ResetScores()
    {
        PlayerPrefs.DeleteKey("LastScore");
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        UpdateScoreDisplay();
        Debug.Log("점수가 초기화되었습니다.");
    }
}
