using UnityEngine;

public class GodMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //状态
    public bool isGodView = true;
    
    [Header("MOvement")]
    public float moveSpeed=10f;

    [Header("阻力")]
    public float groundDrag;
    public float airDrag;

    public float jumpForce;
    
    public float airMultiplier; //空中倍增系数

    public float downForce;

    // bool readyToJump;//准备跳跃
    [Header("Keybinds")]
    //绑定按键
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode downKey = KeyCode.LeftControl;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask WhatIsGround;
    bool grounded;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();//分配刚体
        rb.freezeRotation = true;//冻结刚体旋转
        Cursor.lockState = CursorLockMode.Locked;//光标锁屏幕中央并且不可见
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
                            // 从角色 位置 ，射线方向， 射线长度，检测层
        grounded = Physics.Raycast(transform.position,Vector3.down,playerHeight*0.5f+0.2f,WhatIsGround);
        MyInput();
        //控制最大速度
        SpeedControl();
        if(grounded)   //增加阻力
        {
            rb.linearDamping = groundDrag;
        }else
        {
            rb.linearDamping = airDrag;
        }
        
        if(Input.GetKey(jumpKey) )
        {
            Up();
        }
        if(Input.GetKey(downKey))
        {
            Down();
        }
    }
    void FixedUpdate()
    {
        if(isGodView)MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        //保证移动方向和视线相同
        moveDirection = orientation.forward*verticalInput +orientation.right*horizontalInput;

    

        //前后左右移动 
        if(moveDirection.magnitude>0.01f)
        {
            if(grounded)
                rb.AddForce(moveDirection.normalized*moveSpeed*14f,ForceMode.Force);//归一化，避免对角线移动更快
            else if(!grounded)  //在空中                        跳跃系数
                rb.AddForce(moveDirection.normalized*moveSpeed*15f*airMultiplier,ForceMode.Force);
        }
        
    } 
    
    //速度控制器
    private void SpeedControl()
    {
        //获取刚体的平面速度仅包含x和z分量 
        Vector3 flatVel = new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z);
        // flatVel.magnitude是平面速度大小
        //   maginude是向量的模 长度 x^2+y^2+z^2开根号
        if(flatVel.magnitude>moveSpeed)  //moveSpeed为最大速度
        {
            //flatVel 归一化（变成单位向量，方向不变，长度为 1）
            Vector3 limitedVel = flatVel.normalized*moveSpeed;//应用最大速度，保留原方向
            rb.linearVelocity = new Vector3(limitedVel.x,rb.linearVelocity.y,limitedVel.z);
        }
    }

    //向上
    private void Up()
    {
        //重置y轴速度 为0 保证每次跳跃相同距离
        // rb.linearVelocity = new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z);
        rb.AddForce(transform.up*jumpForce*moveSpeed,ForceMode.Force);
    }

    private void Down()
    {
        rb.AddForce(-transform.up*downForce*moveSpeed,ForceMode.Force);
    }
    // private void ResetJump()
    // {
    //     readyToJump = true;
    // }
}
