using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float MoveSpeed;

    new private Rigidbody2D rigidbody;

    private Animator animator;

    private float InputX, InputY;
    //The state of character when he stop
    private float StopX, StopY;

    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
        Vector2 MovementInput = (transform.right * InputX + transform.up * InputY).normalized;
        rigidbody.velocity = MoveSpeed * MovementInput;

        if (MovementInput != Vector2.zero && !isAttacking)
        {
            animator.SetBool("isMoving", true);
            StopX = InputX;
            StopY = InputY;
        }
        else
            animator.SetBool("isMoving", false);
        animator.SetFloat("InputX", StopX);
        animator.SetFloat("InputY", StopY);

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            if (StopX == 1)
                animator.SetInteger("isAttacking", 4);
            if (StopX == -1)
                animator.SetInteger("isAttacking", 3);
            if (StopY == 1)
                animator.SetInteger("isAttacking", 2);
            if (StopY == -1)
                animator.SetInteger("isAttacking", 1);
            isAttacking = true;
        }

        if(isAttacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            isAttacking = false;
            animator.SetInteger("isAttacking", 0);
        }
    }

    private void PlayerMovement()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
        Vector2 MovementInput = (transform.right * InputX + transform.up * InputY).normalized;
        rigidbody.velocity = MoveSpeed * MovementInput;

        if (MovementInput != Vector2.zero && !isAttacking)
        {
            animator.SetBool("isMoving", true);
            StopX = InputX;
            StopY = InputY;
        }
        else
            animator.SetBool("isMoving", false);
        animator.SetFloat("InputX", StopX);
        animator.SetFloat("InputY", StopY);
    }
}
