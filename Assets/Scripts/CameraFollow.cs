using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("���� ����")]
    public Transform target;           // ���� ��� (�÷��̾�)
    public float smoothSpeed = 5f;     // �ε巯�� �̵� �ӵ�

    [Header("ī�޶� ��� ����")]
    public float minX = -8f;           // ī�޶� �ּ� X ��ġ
    public float maxX = 8f;            // ī�޶� �ִ� X ��ġ
    public float minY = -4f;           // ī�޶� �ּ� Y ��ġ
    public float maxY = 4f;            // ī�޶� �ִ� Y ��ġ

    private Vector3 offset;            // ī�޶�� �÷��̾� �� �Ÿ�

    void Start()
    {
        // �ʱ� �Ÿ� ���� (z�� ���� ����)
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        // target�� ������ ���� �� ��
        if (target == null)
            return;

        // ��ǥ ��ġ ���
        Vector3 desiredPosition = target.position + offset;

        // Z���� �׻� -10���� ���� (2D ����)
        desiredPosition.z = -10f;

        // ��ġ ���� ���� (�� ������ �� ������)
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // �ε巴�� �̵� (Lerp ���)
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * smoothSpeed
        );
    }
}
