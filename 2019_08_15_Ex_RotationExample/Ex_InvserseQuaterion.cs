using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_InvserseQuaterion : MonoBehaviour
{
    public Transform m_a;

    public Transform m_b;

    
    void Update()
    {
        m_b.localRotation = Quaternion.Inverse(m_a.localRotation);
        
    }
}
