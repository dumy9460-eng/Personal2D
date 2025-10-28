using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("추적 설정")]
    public Transform target;           // 따라갈 대상 (플레이어)
    public float smoothSpeed = 5f;     // 부드러운 이동 속도

    [Header("카메라 경계 설정")]
    public float minX = -8f;           // 카메라 최소 X 위치
    public float maxX = 8f;            // 카메라 최대 X 위치
    public float minY = -4f;           // 카메라 최소 Y 위치
    public float maxY = 4f;            // 카메라 최대 Y 위치

    private Vector3 offset;            // 카메라와 플레이어 간 거리

    void Start()
    {
        // 초기 거리 설정 (z축 차이 유지)
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        // target이 없으면 실행 안 함
        if (target == null)
            return;

        // 목표 위치 계산
        Vector3 desiredPosition = target.position + offset;

        // Z축은 항상 -10으로 고정 (2D 게임)
        desiredPosition.z = -10f;

        // 위치 제한 적용 (맵 밖으로 안 나가게)
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // 부드럽게 이동 (Lerp 사용)
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * smoothSpeed
        );
    }
}
