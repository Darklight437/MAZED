using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    Vector3 m_moveDirection;
    Vector3 m_previousmoveDirection;

    CameraVeiwSwap cam;

    public float rotationRate = 0.15f;
    public float Speed;
    public float jumpSpeed;
    public float rotationSpeed;
    Animator animator;
    public float gravity;
    
    CharacterController cc = null;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        m_moveDirection = Vector3.zero;
        cam = Camera.main.GetComponent<CameraVeiwSwap>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Turn");
        
        if (cc.isGrounded)
        {
            animator.SetBool("run", true);

            m_moveDirection = new Vector3(horizontal, transform.position.y, vertical);
            //m_moveDirection = camera.transform.TransformDirection(m_moveDirection);
            m_moveDirection *= Speed;

            if (Input.GetKey(KeyCode.Space))
            {
                m_moveDirection.y = jumpSpeed;
                animator.SetBool("jump", true);
            }
        }
        else
        {
            m_moveDirection = new Vector3(horizontal, m_moveDirection.y, vertical);
            //m_moveDirection = camera.transform.TransformDirection(m_moveDirection);
            m_moveDirection.x *= Speed;
            m_moveDirection.z *= Speed;
            //Debug.Log("I AM JUMPING!!! :D");
        }

       // transform.Rotate(0, turn * rotationSpeed, 0);

        m_moveDirection.y -= gravity * Time.deltaTime;

        if ((m_moveDirection.x != 0) || (m_moveDirection.z != 0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,  Quaternion.LookRotation( new Vector3(m_moveDirection.x, 0, m_moveDirection.z)), 0.15f);
        }

        cc.Move(m_moveDirection * Time.deltaTime);

        if (cc.isGrounded)
        {
            animator.SetBool("jump", false);
        }

        if ((m_moveDirection.x == 0) && (m_moveDirection.z == 0))
        {
            animator.SetBool("run", false);
        }
        
    }
}
    

