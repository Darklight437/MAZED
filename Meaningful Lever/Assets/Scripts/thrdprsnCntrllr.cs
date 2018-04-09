using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class thrdprsnCntrllr : MonoBehaviour {


    Rigidbody m_Rigidbody;
    Animator m_Animator;
    CapsuleCollider m_Capsule;
    bool m_IsGrounded;
    float m_CapsuleHeight;
    Vector3 m_CapsuleCenter;


    // Use this for initialization
    void Start ()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
        m_CapsuleHeight = m_Capsule.height;
        m_CapsuleCenter = m_Capsule.center;

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(Vector3 move, bool jump)
    {

        if (move.magnitude > 1f) move.Normalize();
    }

}
