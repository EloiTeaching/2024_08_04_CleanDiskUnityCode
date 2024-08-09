using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_SkinnedMeshColliderToConvexColliderMono : MonoBehaviour
{
    public SkinnedMeshRenderer m_meshRendererSource;
    [Tooltip("In Case you need the collider to have some specific script on it. Give Empty prefab with what you need on it.")]
    public GameObject m_createColliderFromPrefab;

    [Header("Debug")]
    public GameObject m_created;
    public MeshFilter m_meshFilterTarget;
    public MeshCollider m_meshColliderTarget;
    public float m_scaleFactor = 0.01f;

    public bool m_updateOnStart = true;

    void Start()
    {
        if(m_updateOnStart)
            UpdateMesh();
    }

    [ContextMenu("Update Mesh")]
    void UpdateMesh()
    {

        if(m_created != null)
        {
            if(Application.isPlaying)
               {
                Destroy(m_created);
            }
            else
            {
                DestroyImmediate(m_created);
            }
        }

            m_created =m_createColliderFromPrefab? Instantiate(m_createColliderFromPrefab): new GameObject("Collision Created");
            m_created.transform.parent = m_meshRendererSource.transform;
            m_created.transform.localPosition = Vector3.zero;
            m_created.transform.localRotation = Quaternion.identity;
            m_created.transform.localScale = Vector3.one*m_scaleFactor;
            m_meshFilterTarget= m_created.GetComponent<MeshFilter>();
            if(m_meshFilterTarget == null)
            {
                m_meshFilterTarget = m_created.AddComponent<MeshFilter>();
            }
            m_meshColliderTarget = m_created.GetComponent<MeshCollider>();
            if(m_meshColliderTarget == null)
            {
                m_meshColliderTarget = m_created.AddComponent<MeshCollider>();
            }
            Mesh mesh = new Mesh();
            m_meshRendererSource.BakeMesh(mesh);
            m_meshFilterTarget.mesh = mesh;
            m_meshColliderTarget.sharedMesh = mesh;
            m_meshColliderTarget.convex = true;

    }
}
