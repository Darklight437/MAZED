using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVeiwSwap : MonoBehaviour
{
    [System.Serializable]
    public class StartPointInfo
    {
        public StartPointInfo(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }

        public Vector3 position;
        public Quaternion rotation;
    }

    public Camera cam;

    public Transform thirdPerson;
    public Transform topDown;

    public float lerpTime;
    public float m_timeSpentLerping;

    public bool isTopDown = false;

    public StartPointInfo m_lerpStartPoint;
    private Transform m_lerpEndPoint;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        m_timeSpentLerping = 1.2f;

        m_lerpStartPoint = new StartPointInfo(Vector3.zero, Quaternion.identity);

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
        }

        if (m_timeSpentLerping < 1)
        {
            if (isTopDown == false)
            {
                transform.rotation = Quaternion.Lerp(cam.transform.rotation, m_lerpEndPoint.rotation, m_timeSpentLerping * 0.5f);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(90, 0, 0), m_timeSpentLerping * 0.5f);
            }

            transform.position = Vector3.Lerp(m_lerpStartPoint.position, m_lerpEndPoint.position, m_timeSpentLerping);
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

        m_lerpStartPoint.position = cam.transform.position;
        m_lerpStartPoint.rotation = cam.transform.rotation;
        cam.orthographic = false;
        m_timeSpentLerping = 0;
    }
}
