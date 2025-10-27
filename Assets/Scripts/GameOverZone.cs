using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ����� ���ӿ��� ���� ������
        if (other.CompareTag("Block"))
        {
            Debug.Log("����� ȭ�� ������ ������ - ���ӿ���!");

            // ���� �Ŵ��� ã�Ƽ� ���ӿ��� ȣ��
            StackGameManager manager = FindObjectOfType<StackGameManager>();
            if (manager != null)
            {
                manager.GameOver();
            }

            // ������ ��� ����
            Destroy(other.gameObject);
        }
    }
}
