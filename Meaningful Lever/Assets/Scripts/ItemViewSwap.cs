using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewSwap : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ThrownItem")
        {
            Camera.main.GetComponent<CameraVeiwSwap>().SwapCameraView();
            Destroy(collision.gameObject);
        }
    }

}
