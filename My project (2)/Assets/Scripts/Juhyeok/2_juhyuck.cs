using UnityEngine;

public class SimpleMovementNoFlip : MonoBehaviour
{
    public float moveSpeed = 5f; // 초당 이동 속도

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D, ←/→
        float v = Input.GetAxisRaw("Vertical");   // W/S, ↑/↓
        Vector3 dir = new Vector3(h, v, 0f);
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
