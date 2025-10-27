using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public GameObject interactionPopup;  // ��ȣ�ۿ� UI
    private bool playerInZone = false;   // �÷��̾ ���� �ȿ� �ִ���

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ ������ ����
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            interactionPopup.SetActive(true);  // UI ǥ��
            Debug.Log("�̴ϰ��� ���� ����");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ �������� ����
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            interactionPopup.SetActive(false);  // UI ����
            Debug.Log("�̴ϰ��� ���� ��Ż");
        }
    }

    private void Update()
    {
        // ���� �ȿ��� �����̽��ٸ� ������
        if (playerInZone && Input.GetKeyDown(KeyCode.Space))
        {
            StartMiniGame();
        }
    }

    private void StartMiniGame()
    {
        Debug.Log("�̴ϰ��� ����! (5�ܰ迡�� �� ��ȯ ���� ����)");
        // 5�ܰ迡�� �� ��ȯ �ڵ� �߰� ����
    }
}
