using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVeiwSwap : MonoBehaviour
{
    public Camera cam;
    public GameObject player;

    public Transform thirdPerson;
    public Transform topDown;

    public float lerpTime;
    public float m_timeSpentLerping;

    public bool isTopDown = false;

    public Vector3 m_lerpStartPoint;
    private Transform m_lerpEndPoint;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        m_timeSpentLerping = 1.2f;

        if (isTopDown == false)
        {
            cam.transform.position = thirdPerson.position;
            cam.transform.rotation = thirdPerson.rotation;
            cam.orthographic = false;
        }
        else
        {
            cam.transform.position = topDown.position;
            cam.transform.rotation = topDown.rotation;
            cam.orthographic = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timeSpentLerping += Time.deltaTime ;

        if (Input.GetKeyUp(KeyCode.C))
        {
            SwapCameraView();
        }

        if (isTopDown == false)
        {
            m_lerpEndPoint = thirdPerson;
        }
        else
        {
            m_lerpEndPoint = topDown;
            cam.orthographic = true;
        }

        if (m_timeSpentLerping < 1)
        {
            if (isTopDown == false)
            {
                transform.rotation = Quaternion.Lerp(cam.transform.rotation, m_lerpEndPoint.rotation, m_timeSpentLerping * 0.5f);
            }

            transform.position = Vector3.Lerp(m_lerpStartPoint, m_lerpEndPoint.position, m_timeSpentLerping);
        }
        else
        {
            if (isTopDown == false)
            {
                cam.transform.position = thirdPerson.position;
                cam.transform.rotation = thirdPerson.rotation;
            }
            else
            {
                cam.transform.position = topDown.position;
                cam.orthographic = true;
            }
        }
	}

    public void SwapCameraView()
    {
        isTopDown = !isTopDown;

        if (isTopDown == false)
        {
            cam.orthographic = false;
            player.layer = LayerMask.NameToLayer("3D");
        }
        else
        {
            cam.transform.rotation = Quaternion.Euler(89.99f, 0, 0);
            cam.orthographic = true;
            player.layer = LayerMask.NameToLayer("2D");
        }

        m_lerpStartPoint = cam.transform.position;
        m_timeSpentLerping = 0;
    }
}
