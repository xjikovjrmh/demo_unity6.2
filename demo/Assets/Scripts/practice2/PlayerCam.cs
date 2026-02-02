using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float sensX;
    public float sensY;
    public Transform orientation; //记录角色朝向

    float xRotation; //俯仰角
    float yRotation; //偏航角

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;//光标锁屏幕中央并且不可见
        // Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X")*Time.deltaTime*sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y")*Time.deltaTime*sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        //处理角度 范围
        xRotation = Mathf.Clamp(xRotation,-90f,90f);
        //正式旋转
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);//摄像机沿双轴旋转
        orientation.rotation = Quaternion.Euler(0,yRotation,0);//角色只沿y轴旋转
        
    }
}
