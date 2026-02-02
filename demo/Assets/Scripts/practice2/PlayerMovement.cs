
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("MOvement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;//跳跃冷却时间
    public float airMultiplier; //空中倍增系数
    bool readyToJump;//准备跳跃
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;


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
        readyToJump = true;//初始化为可以跳跃
    }

    // Update is called once per frame
    void Update()
    {
                            // 从角色 位置 ，射线方向， 射线长度，检测层
        grounded = Physics.Raycast(transform.position,Vector3.down,playerHeight*0.5f+0.2f,WhatIsGround);
        MyInput();
        //控制最大速度
        SpeedControl();
        if(grounded)
        {
            rb.linearDamping = groundDrag;
        }else
        {
            rb.linearDamping = 0;
        }
        
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;//在空中不能二段跳
            Jump();
            Invoke(nameof(ResetJump),jumpCooldown);//延时调用
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
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
        if(grounded)
        rb.AddForce(moveDirection.normalized*moveSpeed*10f,ForceMode.Force);//归一化，避免对角线移动更快
        else if(!grounded)  //在空中                        跳跃系数
        rb.AddForce(moveDirection.normalized*moveSpeed*10f*airMultiplier,ForceMode.Force);
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
    private void Jump()
    {
        //重置y轴速度 为0 保证每次跳跃相同距离
        rb.linearVelocity = new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z);
        rb.AddForce(transform.up*jumpForce,ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

}
