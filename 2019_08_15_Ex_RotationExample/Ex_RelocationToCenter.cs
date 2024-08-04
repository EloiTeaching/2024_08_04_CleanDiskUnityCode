using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_RelocationToCenter : MonoBehaviour
{
    public Transform m_aOrigine;
    public Transform m_bTarget;
    public Transform m_gizmo;
    public Transform m_gizmoInParent;
    public bool m_isLeft;
    public bool m_isUp;
    public bool m_isBack;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionOfGizmo();

    }

    private void UpdatePositionOfGizmo()
    {
        Vector3 positionRelocated = m_bTarget.position - m_aOrigine.position;
        positionRelocated = RotatePointAroundPivot(positionRelocated, Vector3.zero, Quaternion.Inverse(m_aOrigine.rotation));
        Quaternion rotationRelocated = m_bTarget.rotation * Quaternion.Inverse(m_aOrigine.rotation);

        m_gizmo.position = positionRelocated;
        m_gizmo.rotation = rotationRelocated;
        m_gizmoInParent.localPosition = positionRelocated;
        m_gizmoInParent.localRotation = rotationRelocated;
    }

    void OnValidate()
    {
        UpdatePositionOfGizmo();

    }
    public Vector3 RotatePointAroundPivot( Vector3 point,Vector3 pivot, Quaternion rotation) 
     {
       Vector3 dir = point - pivot; // get point direction relative to pivot
       dir = rotation * dir; // rotate it
        point = dir + pivot; // calculate rotated point
       return point; // return it
        }
    }