using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_Euleur : MonoBehaviour
{
    public Vector3 m_euler;
    public Transform m_gizmoObject;

    private void OnValidate()
    {
        m_gizmoObject.localRotation = Quaternion.Euler( m_euler);
    }
}
