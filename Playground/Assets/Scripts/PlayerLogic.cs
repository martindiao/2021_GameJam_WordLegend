using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float MoveSpeed;

    new private Rigidbody rigidbody;

    private Animator animator;

    private float InputX, InputY;
    //The state of character when he stop
    private float StopX, StopY;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //InputX = Input.GetAxisRaw("Horizontal");
        //InputY = Input.GetAxisRaw("Vertical");
        //Vector2 MovementInput = (transform.right * InputX + transform.up * InputY).normalized;
        //rigidbody.velocity = MoveSpeed * MovementInput;

        //if (MovementInput != Vector2.zero)
        //{
        //    animator.SetBool("isMoving", true);
        //    StopX = InputX;
        //    StopY = InputY;
        //}
        //else
        //    animator.SetBool("isMoving", false);
        //animator.SetFloat("InputX", StopX);
        //animator.SetFloat("InputY", StopY);

        PlayerMovement();

    }

    private void PlayerMovement()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
        //Vector3 MovementInput = (transform.right * InputX + transform.up * InputY).normalized;
        Vector3 MovementInput = new Vector3(InputX,0,InputY).normalized;
        //rigidbody.velocity = MoveSpeed * MovementInput;

        if(rigidbody.velocity.y >= 0)
        {
            rigidbody.velocity = new Vector3(MoveSpeed * MovementInput.x, 0, MoveSpeed * MovementInput.z);
        }
        else
        {
            rigidbody.velocity = new Vector3(MoveSpeed * MovementInput.x, -10, MoveSpeed * MovementInput.z);
        }
        



        //如果延轴移动,获取上一帧垂直方向的输入,以保证人物方向不会突变
        if (InputX == 0)
        {
            InputX = StopX;
        }
        if (InputY == 0)
        {
            InputY = StopY;
        }

        //人物是否移动
        if (MovementInput != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            StopX = InputX;
            StopY = InputY;
        }
        else
            animator.SetBool("isMoving", false);


        animator.SetFloat("InputX", StopX);
        animator.SetFloat("InputY", StopY);

        //判断Idle朝向
        //右背
        if (StopX >= 0 && StopY >= 0)
        {
            animator.SetFloat("Direction", 0f);
        }
        //右面
        else if(StopX >= 0 && StopY < 0)
        {
            animator.SetFloat("Direction", 0.33f);
        }
        //左面
        else if (StopX < 0 && StopY < 0)
        {
            animator.SetFloat("Direction", 0.66f);
        }
        //左背
        else if (StopX < 0 && StopY >= 0)
        {
            animator.SetFloat("Direction", 1.0f);
        }
    }
}
