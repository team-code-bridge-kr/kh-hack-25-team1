using System;
using UnityEngine;

public class minkyu_characterMove : MonoBehaviour
{
    public float minkyu_speed = 10f;        // 이동 속도
    public float minkyu_swingSpeed = 20f;    // 흔들림 속도
    public float minkyu_swingAmount = 15f;  // 흔들림 각도

    private Vector3 targetPosition;
    private bool minkyu_isMoving = false;
    private float minkyu_swingTime = 0f;    // 흔들림 애니메이션 시간

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 감지, 의도는 터치
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // 카메라로부터의 거리
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
            targetPosition.z = 0; // Z좌표 고정 (2D)
            minkyu_isMoving = true;
        }

        if (minkyu_isMoving)
        {
            // 캐릭터 이동
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                minkyu_speed * Time.deltaTime
            );

            // 허수아비처럼 좌우로 흔들림
            minkyu_swingTime += Time.deltaTime * minkyu_swingSpeed;
            float swingAngle = Mathf.Sin(minkyu_swingTime) * minkyu_swingAmount;
            transform.rotation = Quaternion.Euler(15, 0, swingAngle);

            // 도착 체크
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                minkyu_isMoving = false;
                transform.rotation = Quaternion.Euler(15, 0, 0); // 원래 각도로 복귀
            }
        }
    }
}