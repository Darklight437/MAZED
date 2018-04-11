using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    public GameObject throwItem;
    public float force;
    public float throwheignt;
    private float throwOffset = 0.35f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("throw", false);
        if (Input.GetMouseButtonDown(0))
        {
            
            animator.SetBool("aim", true);
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("throw", true);
            animator.SetBool("aim", false);
        }
    }

    public void Throwball()
    {
        GameObject go = Instantiate(throwItem, new Vector3(transform.position.x + throwOffset, transform.position.y + throwheignt, transform.position.z), Quaternion.identity);
        go.tag = "ThrownItem";
        go.GetComponent<Rigidbody>().velocity = transform.forward * force;
    }
}
