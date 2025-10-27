using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 블록이 게임오버 존에 닿으면
        if (other.CompareTag("Block"))
        {
            Debug.Log("블록이 화면 밖으로 떨어짐 - 게임오버!");

            // 게임 매니저 찾아서 게임오버 호출
            StackGameManager manager = FindObjectOfType<StackGameManager>();
            if (manager != null)
            {
                manager.GameOver();
            }

            // 떨어진 블록 삭제
            Destroy(other.gameObject);
        }
    }
}
