using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_InverseTransform : MonoBehaviour
{
    public Transform m_a;
    public Transform m_b;
    public Transform m_gizmo;
    public bool m_isAtRight;
    public bool m_isAtUp;
    public bool m_isAtForward;
    public float m_range = 5;
    void Update()
    {
        m_gizmo.localPosition = (m_a.InverseTransformPoint(m_b.position)) * m_range;
      //  transform.InverseTransformDirection

        m_isAtRight = m_gizmo.localPosition.x > 0f;
        m_isAtUp = m_gizmo.localPosition.y > 0f;
        m_isAtForward = m_gizmo.localPosition.z > 0f;
    }
}
