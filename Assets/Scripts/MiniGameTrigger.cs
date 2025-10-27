using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameTrigger : MonoBehaviour
{
    public GameObject interactionPopup;  // 상호작용 UI
    private bool playerInZone = false;   // 플레이어가 구역 안에 있는지

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 구역에 진입
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            interactionPopup.SetActive(true);  // UI 표시
            Debug.Log("미니게임 구역 진입");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 구역에서 나감
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            interactionPopup.SetActive(false);  // UI 숨김
            Debug.Log("미니게임 구역 이탈");
        }
    }

    private void Update()
    {
        // 구역 안에서 스페이스바를 누르면
        if (playerInZone && Input.GetKeyDown(KeyCode.Space))
        {
            StartMiniGame();
        }
    }

    private void StartMiniGame()
    {
        Debug.Log("스택 미니게임 시작!");
        SceneManager.LoadScene("StackMiniGame");
    }
}
