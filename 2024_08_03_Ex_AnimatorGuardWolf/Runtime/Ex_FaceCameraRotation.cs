using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_FaceCameraRotation : MonoBehaviour
{
    public Transform m_whatToRotate;
    public Transform m_cameraToFace;
    public float m_adjustmentAngle = 180;



    void Update()
    {
        if(m_cameraToFace == null)
        {
            m_cameraToFace = Camera.main.transform;
        }
        if(m_cameraToFace == null)
        {
            return;
        }

        Vector3 direction= m_cameraToFace.position - m_whatToRotate.position;
        direction.y = 0;
        m_whatToRotate.forward = direction;
        m_whatToRotate.Rotate(Vector3.up, m_adjustmentAngle, Space.Self);
        
    }
}
