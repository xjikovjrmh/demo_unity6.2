using UnityEngine;

public class CarMove : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 2f;
    public float acceleration = 10f;
    public float deceleration = 5f;

    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        // 锁定不需要的轴
        carRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        // 物理相关逻辑应放在FixedUpdate中
        float input = 0f;
        if (Input.GetKey(KeyCode.W)) input = 1f;
        else if (Input.GetKey(KeyCode.S)) input = -1f;

        if (input != 0)
        {
            // 沿世界Z轴施加力，确保方向正确
            carRb.AddForce(Vector3.forward * input * acceleration, ForceMode.Acceleration);
        }
        else
        {
            // 自动减速
            carRb.AddForce(-carRb.linearVelocity.normalized * deceleration, ForceMode.Acceleration);
        }
    }
}