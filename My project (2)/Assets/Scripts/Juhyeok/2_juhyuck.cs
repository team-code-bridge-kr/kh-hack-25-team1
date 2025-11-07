using UnityEngine;

public class SimpleMovementNoFlip : MonoBehaviour
{
    [Header("이동")]
    public float moveSpeed = 5f;          // 유닛/초
    public bool normalizeDiagonal = true; // 대각선 과속 방지
    public bool fourDirectionsOnly = false; // true면 상하좌우만

    [Header("픽셀 퍼펙트(선택)")]
    public bool pixelSnap = true;   // 픽셀 격자에 스냅
    public int pixelsPerUnit = 32;  // PNG 임포트 PPU와 동일

    Vector2 input;

    void Update()
    {
        // WASD/화살표 모두 동작 (구 Input Manager 기준)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector2(h, v);

        // 4방향만 허용하면 축 하나만 살려주기
        if (fourDirectionsOnly && input != Vector2.zero)
        {
            if (Mathf.Abs(input.x) >= Mathf.Abs(input.y)) input.y = 0f;
            else input.x = 0f;
        }

        // 대각선 속도 보정
        if (normalizeDiagonal && input.sqrMagnitude > 1f)
            input.Normalize();

        // transform 기반 이동 (물리X)
        transform.position += (Vector3)(input * moveSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        // 픽셀 스냅(픽셀 깨짐 방지) - 원하면 끄기
        if (!pixelSnap) return;
        float upp = 1f / Mathf.Max(1, pixelsPerUnit);
        Vector3 p = transform.position;
        p.x = Mathf.Round(p.x / upp) * upp;
        p.y = Mathf.Round(p.y / upp) * upp;
        transform.position = p;
    }
}
