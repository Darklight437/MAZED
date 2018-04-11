using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    public GameObject throwItem;
    public float force;
	void Update ()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction.normalized;

            GameObject go = Instantiate(throwItem, new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, transform.position.z + dir.z), Quaternion.identity);
            go.tag = "ThrownItem";
            go.GetComponent<Rigidbody>().velocity = dir * force;
            Debug.Log(new Vector3(transform.position.x - dir.normalized.x, transform.position.y + dir.normalized.y, transform.position.z - dir.normalized.z));
        }

	}
}
