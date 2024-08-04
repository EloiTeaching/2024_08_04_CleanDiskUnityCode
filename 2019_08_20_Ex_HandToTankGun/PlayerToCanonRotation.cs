using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToCanonRotation : MonoBehaviour
{
    [Header("VR Camera")]
    public Transform m_refPoint;
    public Transform m_playerHand;

    [Header("Offset")]
    public Vector3 m_cameraToHandOffsetPosition;
    public Vector3 m_cameraToHandOffsetEuler;


    [Header("Cannon params")]
    public Transform m_cannonToAffect;
    public float m_angleHorizontal = 90;
    public float m_angleVertical = 45;
    public float m_fireRange = 0.2f;


    [Header("State")]
    public bool m_isFiring;


    void Update()
    {
        m_cameraToHandOffsetPosition = m_refPoint.InverseTransformPoint(m_playerHand.position);

        Debug.DrawLine(Vector3.zero, m_cameraToHandOffsetPosition, Color.cyan);
        Debug.DrawLine(m_refPoint.position, m_playerHand.position, Color.cyan);


        Quaternion localRotation = Quaternion.Euler(- m_angleVertical * Mathf.Clamp( m_cameraToHandOffsetPosition.y,-1,1), m_angleHorizontal * Mathf.Clamp(m_cameraToHandOffsetPosition.x, -1, 1), 0);
        m_cameraToHandOffsetEuler = localRotation.eulerAngles;
        m_isFiring = m_cameraToHandOffsetPosition.z > m_fireRange ;

        m_cannonToAffect.localRotation = localRotation;
    }
}
