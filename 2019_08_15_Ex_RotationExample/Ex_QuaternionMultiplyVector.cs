using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ex_QuaternionMultiplyVector : MonoBehaviour
{

    public Vector3 m_direction;
    public Vector3 m_euleurAngle;
    public Quaternion m_rotation;
    public Vector3 m_newDirection;
    public Transform m_affected;

    void OnValidate()
    {

        //transform.Rotate(Vector3.up, Space.Self);

        m_rotation = Quaternion.Euler(m_euleurAngle);

        //V voulu = Q Rotation voulue * V direction actuel
        m_newDirection = m_rotation * m_direction;


        m_affected.forward = m_newDirection;


    }
    public static bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}


