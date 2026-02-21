using UnityEngine;

public class ViewSwitchManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("视角配置")]
    public Camera mainCamera;
    
    public KeyCode switchKey = KeyCode.V;

    public bool isGodView = true; // 当前是否是上帝视角
    public Transform car;
    public Transform God;
    private GodMovement godMove;
    private CarMove carMoveScript;// 小车控制脚本
    public MoveCamera moveCamera;// 摄像机控制脚本
    
    void Start()
    {
        
        godMove = God.GetComponent<GodMovement>();
        carMoveScript = car.GetComponent<CarMove>();
        SwitchToGodView();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            isGodView = !isGodView;
            if (isGodView)
            {
                SwitchToGodView();
            }
            else
            {
                SwitchToCarView();
            }
        }
    }

    void SwitchToGodView()
    {
        if (godMove == null)
        {
            Debug.LogError("请为GodMove赋值！");
            return;
        }
        if(moveCamera==null)
        {
            Debug.LogError("请为MoveCamera赋值！");
            return;
        }
        godMove.enabled = true;
        godMove.isGodView = true;
        moveCamera.SwitchAnchor(true);
        if (carMoveScript != null) carMoveScript.enabled = false;
        
        
    
    } 

    void SwitchToCarView()
    {
        if (car == null)
        {
            Debug.LogError("请为Car赋值！");
            return;
        }
        if (godMove == null)
        {
            Debug.LogError("godMove为空！");
            return;
        }
        if (moveCamera == null)
        {
           Debug.LogError("moveCamera为空！");
           return;
        } 
        godMove.enabled = false;
        isGodView = false;
        godMove.isGodView = false;
        moveCamera.SwitchAnchor(false);
        // 启用小车控制脚本
        if (carMoveScript != null) carMoveScript.enabled = true;
        
        
    }

    public void SetCar(Transform carTransform)
    {
        car = carTransform;
        carMoveScript = car.GetComponent<CarMove>();
    }

}
