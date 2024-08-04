using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_DebugDrawCross : MonoBehaviour
{
    public float m_distance=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawCross(transform, m_distance);

    }

    public static  void DrawCross(Transform obj, float dist= 0.5f)
    {
        Debug.DrawLine(obj.position,       obj.position + obj.forward * dist , Color.blue, Time.deltaTime);
        Debug.DrawLine(obj.position,       obj.position + obj.right * dist  , Color.red, Time.deltaTime);
        Debug.DrawLine(obj.position,       obj.position + obj.up * dist    , Color.green, Time.deltaTime);
    }
}
