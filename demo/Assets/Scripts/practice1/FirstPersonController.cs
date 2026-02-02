// FirstPersonController.cs
using UnityEngine;
using UnityEngine.InputSystem; // 必须引入

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Look")]
    public float lookSensitivity = 2f;
    public Transform playerCamera; // 拖入相机（通常是子物体）

    private CharacterController controller;
    private Actions inputActions; // 自动生成的输入类
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isRunning;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (playerCamera == null) playerCamera = Camera.main.transform;

        // 创建输入动作实例
        inputActions = new Actions();

        // 绑定 Move 和 Look 回调（自动持续触发）
        inputActions.GodMode.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.GodMode.Move.canceled += _ => moveInput = Vector2.zero;

        inputActions.GodMode.RotateCamera.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.GodMode.RotateCamera.canceled += _ => lookInput = Vector2.zero;

        //// 绑定 Run（按住 LeftShift）
        //inputActions.Player.Run.started += _ => isRunning = true;
        //inputActions.Player.Run.canceled += _ => isRunning = false;
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleLook()
    {
        if (lookInput == Vector2.zero) return;

        // 水平旋转角色
        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity * Time.deltaTime);

        // 垂直旋转相机（限制俯仰）
        float pitch = playerCamera.localEulerAngles.x;
        if (pitch > 180) pitch -= 360f; // 转为 [-180, 180]
        pitch -= lookInput.y * lookSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        playerCamera.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    void HandleMovement()
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // 构建移动方向（忽略 Y 轴）
        Vector3 moveDir = forward * moveInput.y + right * moveInput.x;
        moveDir.y = 0f;

        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }
}