using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    Vector3 m_moveDirection;
    

    public float Speed;
    public float jumpSpeed;
    public float rotationSpeed;

    public float gravity;
    
    CharacterController cc = null;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        m_moveDirection = Vector3.zero;
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
            m_moveDirection = new Vector3(horizontal, 0, vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection *= Speed;

            if (Input.GetKey(KeyCode.Space))
            {
                m_moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            m_moveDirection = new Vector3(horizontal, m_moveDirection.y, vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection.x *= Speed;
            m_moveDirection.z *= Speed;
        }

        transform.Rotate(0, turn * rotationSpeed, 0);

        m_moveDirection.y -= gravity * Time.deltaTime;

        cc.Move(m_moveDirection * Time.deltaTime);
        
    }
    
}
