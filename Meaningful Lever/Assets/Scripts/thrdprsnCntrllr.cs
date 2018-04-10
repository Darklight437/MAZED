using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharControl
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    //Heavily ripped from unity standard asset 3rd person character controller (edited to be input driven over animatior driven)
    public class ThrdprsnCntrllr : MonoBehaviour
    {

        [SerializeField] float m_GroundCheckDistance = 0.1f;
        [SerializeField] float m_JumpPower = 12f;
        [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;


        Rigidbody m_Rigidbody;
        Animator m_Animator;
        CapsuleCollider m_Capsule;
        //float m_TurnAmount;
        //float m_ForwardAmount;
        bool m_IsGrounded;
        float m_OrigGroundCheckDistance;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        Vector3 m_GroundNormal;

        // Use this for initialization
        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Move(Vector3 move, bool jump)
        {

            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            //m_TurnAmount = Mathf.Atan2(move.x, move.z);
            //m_ForwardAmount = move.z;

            //movement will be identical in air or on ground but will have different animation handling
            if (m_IsGrounded)
            {
                HandleGroundedMovement(jump);
            }
            else
            {
                HandleAirborneMovement();
            }

            //will need to update movement not using animator
            updatePosition(move);
        }

        void updatePosition(Vector3 move)
        {
            //set the animator states in here






            //now move character by physics




        }




        void HandleAirborneMovement()
        {
            // apply extra gravity from multiplier:
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravityForce);

            m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
        }


        void HandleGroundedMovement(bool jump)
        {
            if (jump && m_IsGrounded)
            {
                m_Animator.SetBool("jump", true);
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
                m_IsGrounded = false;
                m_GroundCheckDistance = 0.1f;

            }
        }

        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                //m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                //m_Animator.applyRootMotion = false;
            }
        }


    }
}
