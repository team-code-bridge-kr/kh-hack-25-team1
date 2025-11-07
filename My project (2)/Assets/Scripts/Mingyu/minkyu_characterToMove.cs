using System;
using UnityEngine;

public class minkyu_characterToMove : MonoBehaviour
{
    public float minkyu_speed = 5f;         // 이동 속도
    public float minkyu_swingSpeed = 5f;    // 흔들림 속도
    public float minkyu_swingAmount = 15f;  // 흔들림 각도
    public float minkyu_minX = -100f;       // 왼쪽 벽
    public float minkyu_maxX = 100f;        // 오른쪽 벽
    public float minkyu_minY = -305f;       // 아래 벽
    public float minkyu_maxY = 305f;        // 위 벽

    private Vector3 minkyu_moveDirection;   // 이동 방향
    private float minkyu_swingTime = 0f;

    void Start()
    {
        // 시작할 때 랜덤 방향 설정
        minkyu_SetRandomDirection();
    }

    void Update()
    {
        // 자동 이동
        transform.position += minkyu_moveDirection * minkyu_speed * Time.deltaTime;

        // 허수아비처럼 좌우로 흔들림
        minkyu_swingTime += Time.deltaTime * minkyu_swingSpeed;
        float minkyu_swingAngle = Mathf.Sin(minkyu_swingTime) * minkyu_swingAmount;
        transform.rotation = Quaternion.Euler(15, 0, minkyu_swingAngle);

        // 벽 충돌 체크
        Vector3 minkyu_pos = transform.position;

        // 좌우 벽 충돌
        if (minkyu_pos.x <= minkyu_minX || minkyu_pos.x >= minkyu_maxX)
        {
            minkyu_moveDirection.x = -minkyu_moveDirection.x; // X 방향 반전
            minkyu_pos.x = Mathf.Clamp(minkyu_pos.x, minkyu_minX, minkyu_maxX); // 벽 안쪽으로 위치 조정
        }

        // 상하 벽 충돌
        if (minkyu_pos.y <= minkyu_minY || minkyu_pos.y >= minkyu_maxY)
        {
            minkyu_moveDirection.y = -minkyu_moveDirection.y; // Y 방향 반전
            minkyu_pos.y = Mathf.Clamp(minkyu_pos.y, minkyu_minY, minkyu_maxY);
        }

        transform.position = minkyu_pos;
    }

    void minkyu_SetRandomDirection()
    {
        // 랜덤한 방향 생성 (정규화하여 일정한 속도 유지)
        float minkyu_randomX = UnityEngine.Random.Range(-1f, 1f);
        float minkyu_randomY = UnityEngine.Random.Range(-1f, 1f);
        minkyu_moveDirection = new Vector3(minkyu_randomX, minkyu_randomY, 0).normalized;
    }

    // Inspector에서 벽 범위를 시각적으로 확인
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // 벽 그리기
        Vector3 minkyu_bottomLeft = new Vector3(minkyu_minX, minkyu_minY, 0);
        Vector3 minkyu_bottomRight = new Vector3(minkyu_maxX, minkyu_minY, 0);
        Vector3 minkyu_topLeft = new Vector3(minkyu_minX, minkyu_maxY, 0);
        Vector3 minkyu_topRight = new Vector3(minkyu_maxX, minkyu_maxY, 0);

        Gizmos.DrawLine(minkyu_bottomLeft, minkyu_bottomRight);
        Gizmos.DrawLine(minkyu_bottomRight, minkyu_topRight);
        Gizmos.DrawLine(minkyu_topRight, minkyu_topLeft);
        Gizmos.DrawLine(minkyu_topLeft, minkyu_bottomLeft);
    }
}