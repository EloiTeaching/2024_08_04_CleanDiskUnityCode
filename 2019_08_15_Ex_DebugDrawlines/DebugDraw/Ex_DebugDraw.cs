using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_DebugDraw : MonoBehaviour
{
    public Transform m_rayStart;
    public bool m_hit;
    public float m_distanceOfRay=0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(m_rayStart.position, m_rayStart.position + m_rayStart.forward* m_distanceOfRay, m_hit?Color.green: Color.red, Time.deltaTime);
       // Debug.DrawRay(m_rayStart.position, )
    }
}
