using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_Lerp : MonoBehaviour
{

    [Range(0,1)]
    public float m_pourcent;
    public Transform m_atobObj;
    public Transform m_a;
    public Transform m_b;

    public Transform m_followObj;
    public Transform m_followTarget;
    public float m_power=2f;


    public void Update()
    {
        m_followObj.position = Vector3.Lerp(m_followObj.position, m_followTarget.position, Time.deltaTime * m_power);
        m_followObj.rotation = Quaternion.Lerp(m_followObj.rotation, m_followTarget.rotation, Time.deltaTime * m_power);
    }


    public void OnValidate()
    {
       m_atobObj.position=  Vector3.Lerp(m_a.position, m_b.position, m_pourcent);
    }
}
