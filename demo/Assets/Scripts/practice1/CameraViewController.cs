//using UnityEngine;
//using UnityEngine.InputSystem;

//public class CameraViewController : MonoBehaviour
//{  
//    private Actions controls;
//    private Camera cam;

//    // === 上帝模式参数 ===
//    [Header("God Mode")]
//    public float godMoveSpeed = 10f;
//    public float godRotateSpeed = 2f;
//    private Vector2 godMoveInput;
//    private bool godUp, godDown;

//    // === 第三人称参数 ===
//    [Header("Third Person")]
//    public Transform target; // 小车或目标
//    public float thirdRotateSpeed = 2f;
//    public float distance = 5f;
//    public float height = 2f;
//    private Vector2 thirdRotation = new Vector2(10f, 0f); // (pitch, yaw)

//    // === 高亮系统 ===
//    [Header("Highlight")]
//    public Material highlightMaterial; // 拖入高亮材质（必须是专用材质）
//    private Renderer highlightedRenderer;
//    private Material originalMaterial;

//    void Awake()
//    {
//        cam = Camera.main;
//        controls = new Actions();

//        // Global
        

//        // GodMode
//        controls.GodMode.Move.performed += ctx => godMoveInput = ctx.ReadValue<Vector2>();
//        controls.GodMode.Move.canceled += _ => godMoveInput = Vector2.zero;
//        controls.GodMode.Up.performed += _ => godUp = true;
//        controls.GodMode.Up.canceled += _ => godUp = false;
//        controls.GodMode.Down.performed += _ => godDown = true;
//        controls.GodMode.Down.canceled += _ => godDown = false;

//        // 关键：RotateCamera 必须绑定 Mouse/delta + Press(Mouse Right)
//        controls.GodMode.RotateCamera.performed += ctx =>
//        {
//            Vector2 delta = ctx.ReadValue<Vector2>();
//            // 水平旋转（绕世界Y轴）
//            transform.Rotate(Vector3.up, delta.x * godRotateSpeed, Space.World);
//            // 垂直旋转（绕自身X轴），限制俯仰
//            float pitch = transform.localEulerAngles.x;
//            if (pitch > 180) pitch -= 360f; // 转为 [-180, 180]
//            pitch -= delta.y * godRotateSpeed;
//            pitch = Mathf.Clamp(pitch, -89f, 89f);
//            transform.localEulerAngles = new Vector3(pitch, transform.localEulerAngles.y, 0);
//        };

//        controls.GodMode.SelectObject.performed += _ => RaycastSelect();

//        // ThirdPerson
//        controls.ThirdPerson.RotateCamera.performed += ctx =>
//        {
//            Vector2 delta = ctx.ReadValue<Vector2>();
//            thirdRotation.y += delta.x * thirdRotateSpeed; // Yaw
//            thirdRotation.x -= delta.y * thirdRotateSpeed; // Pitch
//            thirdRotation.x = Mathf.Clamp(thirdRotation.x, -60f, 60f);
//        };

//        controls.ThirdPerson.Zoom.performed += ctx =>
//        {
//            distance -= ctx.ReadValue<Vector2>().y * 2f;
//            distance = Mathf.Clamp(distance, 1f, 20f);
//        };

       
//    }

   

//    void RaycastSelect()
//    {
//        if (highlightMaterial == null)
//        {
//            Debug.LogWarning("Highlight material not assigned!");
//            return;
//        }

//        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
//        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
//        {
//            Renderer r = hit.collider.GetComponent<Renderer>();
//            if (r != null && r.sharedMaterial != highlightMaterial)
//            {
//                ResetHighlight(); // 清除旧高亮

//                originalMaterial = r.sharedMaterial;
//                highlightedRenderer = r;
//                r.sharedMaterial = highlightMaterial;

//                Invoke(nameof(ResetHighlight), 0.5f);
//            }
//        }
//    }

//    void ResetHighlight()
//    {
//        if (highlightedRenderer != null && originalMaterial != null)
//        {
//            highlightedRenderer.sharedMaterial = originalMaterial;
//            highlightedRenderer = null;
//            originalMaterial = null;
//        }
//        CancelInvoke(nameof(ResetHighlight));
//    }

//    void OnEnable() => controls.Enable();
//    void OnDisable() => controls.Disable();

    

//    void OnDestroy()
//    {
//        ResetHighlight();
//    }
//}