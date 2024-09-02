using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Ex_SetQuadInDirectionOfCamera : MonoBehaviour
{


    public int m_maxQuad = 16383;
    public MeshFilter m_meshFilter;
    public SkinnedMeshRenderer m_skinMeshRenderer;

    [Header("Debug")]
    public Mesh m_currentMesh;
    public ProcessBulletsForParallels m_job;
    public NativeArray<bool> m_quadToDisplay;
    public NativeArray<Vector3> m_quadPosition;
    public NativeArray<Vector3> m_verticePosition;
    public NativeArray<float> m_quadSizeRadius;
    


    private void Reset()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_skinMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if(m_meshFilter == null)
        {
            m_meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        if(m_skinMeshRenderer == null)
        {
            m_skinMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
        }
    }
    private void Awake()
    {
        m_quadToDisplay = new NativeArray<bool>(m_maxQuad, Allocator.Persistent);
        m_quadPosition = new NativeArray<Vector3>(m_maxQuad, Allocator.Persistent);
        m_quadSizeRadius = new NativeArray<float>(m_maxQuad, Allocator.Persistent);
        m_verticePosition = new NativeArray<Vector3>(m_maxQuad * 4, Allocator.Persistent);

    }

    private void OnDestroy()
    {
        if (m_quadToDisplay.IsCreated)
            m_quadToDisplay.Dispose();
        if (m_quadPosition.IsCreated)
            m_quadPosition.Dispose();
        if (m_quadSizeRadius.IsCreated)
            m_quadSizeRadius.Dispose();
        if (m_verticePosition.IsCreated)
            m_verticePosition.Dispose();
    }


    public bool m_randomizeAtStart = true;
    public bool m_randomizeAtEachFrame = true;

    private void Start()
    {
        if (m_randomizeAtStart)
            Randomize32();
    }



    private void Update()
    {
        if (m_randomizeAtEachFrame)
            Randomize32();
        ApplyComputeRendering();
    }
    private void Randomize32()
    {
        for (int i = 0; i < m_maxQuad; i++)
        {
            m_quadToDisplay[i] = UnityEngine.Random.value > 0.5f;
            m_quadPosition[i] = new Vector3(UnityEngine.Random.Range(-32f, 32f), UnityEngine.Random.Range(-32f, 32f), UnityEngine.Random.Range(-32f, 32f));
            m_quadSizeRadius[i] = UnityEngine.Random.Range(0.5f, 2f);
        }
    }

    private bool m_isInitiatized = false;
    public void ApplyComputeRendering()
    {
        if (!m_isInitiatized)
        {
            m_isInitiatized = true;
            InitWithCount();
        }

        m_job.m_cameraPosition = transform.InverseTransformPoint(Camera.main.transform.position);
        m_job.m_maxQuad = m_maxQuad;
        m_job.m_quadPosition = m_quadPosition;
        m_job.m_squareSize = m_quadSizeRadius;
        m_job.m_isActiveObject = m_quadToDisplay;
        m_job.m_resultVerticepositions = m_verticePosition;
        JobHandle jh = m_job.Schedule(m_maxQuad, 64);
        jh.Complete();
        m_currentMesh.SetVertices(m_job.m_resultVerticepositions);
        m_skinMeshRenderer.bounds = new Bounds(Vector3.zero, Vector3.one * m_maxDistanceBounds);


        if (m_useDebugDraw)
        {
            for (int i = 0; i < 100; i++)
            {
                int iv = i * 4;
                Debug.DrawLine(m_job.m_resultVerticepositions[iv + 0], m_job.m_resultVerticepositions[iv + 1], Color.red);
                Debug.DrawLine(m_job.m_resultVerticepositions[iv + 0], m_job.m_resultVerticepositions[iv + 2], Color.red);
                Debug.DrawLine(m_job.m_resultVerticepositions[iv + 2], m_job.m_resultVerticepositions[iv + 3], Color.red);
                Debug.DrawLine(m_job.m_resultVerticepositions[iv + 3], m_job.m_resultVerticepositions[iv + 1], Color.red);

            }
        }
    }
    public bool m_useDebugDraw = false;
    public float m_maxDistanceBounds = 1000f;
    public void InitWithCount()
    {
        m_currentMesh = new Mesh();
        m_currentMesh.name = "Bullet as square at far distance";
        Vector3[] vertices = new Vector3[m_maxQuad * 4];
        Vector2[] uvs = new Vector2[m_maxQuad * 4];
        int[] triangles = new int[m_maxQuad * 6];

        m_currentMesh.bounds = new Bounds(Vector3.zero, Vector3.one * m_maxDistanceBounds);

        for (int i = 0; i < m_maxQuad; i++)
        {
            int iuv = i * 4;
            int itv = i * 6;
            uvs[iuv + 0] = new Vector2(0, 0);
            uvs[iuv + 1] = new Vector2(1, 0);
            uvs[iuv + 2] = new Vector2(0, 1);
            uvs[iuv + 3] = new Vector2(1, 1);
            triangles[itv + 0] = iuv + 0;
            triangles[itv + 1] = iuv + 2;
            triangles[itv + 2] = iuv + 1;
            triangles[itv + 3] = iuv + 2;
            triangles[itv + 4] = iuv + 3;
            triangles[itv + 5] = iuv + 1;
        }
        m_currentMesh.SetVertices(vertices);
        m_currentMesh.SetUVs(0, uvs);
        m_currentMesh.SetTriangles(triangles, 0);
        m_meshFilter.sharedMesh = m_currentMesh;
        m_meshFilter.mesh = m_currentMesh;
        if (m_skinMeshRenderer != null)
        {

            m_skinMeshRenderer.sharedMesh = m_currentMesh;
        }


        m_job = new ProcessBulletsForParallels();
        int[] lp = new int[m_maxQuad];
        Vector3[] v3 = new Vector3[m_currentMesh.vertices.Length];
        m_job.m_quadPosition = m_quadPosition;
        m_job.m_squareSize = m_quadSizeRadius;
        m_job.m_resultVerticepositions = m_verticePosition;
        m_job.m_isActiveObject = m_quadToDisplay;
        m_job.m_maxQuad = m_maxQuad;
        ;
    }




    [BurstCompile(CompileSynchronously = true)]
    public struct ProcessBulletsForParallels : IJobParallelFor
    {
        public int m_maxQuad;


        [ReadOnly] public NativeArray<bool> m_isActiveObject;
        [ReadOnly] public NativeArray<Vector3> m_quadPosition;
        [ReadOnly] public NativeArray<float> m_squareSize;
        [NativeDisableParallelForRestriction]
        [WriteOnly] public NativeArray<Vector3> m_resultVerticepositions;
        public Vector3 m_hidePosition;
        public Vector3 m_cameraPosition;
        Quaternion m_pbl;
        Quaternion m_pbr;
        Quaternion m_ptl;
        Quaternion m_ptr;

        //public void SetRotations()
        //{
        //    //p1 = Quaternion.Euler(0, 90, 0) * Quaternion.Euler(0, 0, 45);
        //    //p2 = Quaternion.Euler(0, 90, 0) * Quaternion.Euler(0, 0, -45);
        //    //p3 = Quaternion.Euler(0, -90, 0) * Quaternion.Euler(0, 0, 45);
        //    //p4 = Quaternion.Euler(0, -90, 0) * Quaternion.Euler(0, 0, -45);
        //    m_pbl = Quaternion.Euler(-135, 90, 0);
        //    m_pbr = Quaternion.Euler(-45, 90, 0);
        //    m_ptl = Quaternion.Euler(135, 90, 0);
        //    m_ptr = Quaternion.Euler(45, 90, 0);
        //    m_squareSize = 1f;
        //}

        public void Execute(int index)
        {
            if (index >= m_maxQuad)
                return;
            bool isBulletUsed = m_isActiveObject[index];
            int vertexIndexPosition = index * 4;
            if (isBulletUsed)
            {

                m_resultVerticepositions[vertexIndexPosition + 0] = Vector3.zero;
                m_resultVerticepositions[vertexIndexPosition + 1] = Vector3.zero;
                m_resultVerticepositions[vertexIndexPosition + 2] = Vector3.zero;
                m_resultVerticepositions[vertexIndexPosition + 3] = Vector3.zero;
                return;
            }
            else
            {
                float radius = m_squareSize[index];

                float squareSize = (float)Math.Sqrt(radius * radius + radius * radius);
                Vector3 dir = -(m_quadPosition[index] - m_cameraPosition);
                Quaternion cQ = Quaternion.LookRotation(dir, Vector3.up);
                Vector3 position = m_quadPosition[index];

                SetBorderLocal();
                m_resultVerticepositions[vertexIndexPosition + 0] = position + ((cQ * m_pbl) * Vector3.forward * radius);
                m_resultVerticepositions[vertexIndexPosition + 1] = position + ((cQ * m_pbr) * Vector3.forward * radius);
                m_resultVerticepositions[vertexIndexPosition + 2] = position + ((cQ * m_ptl) * Vector3.forward * radius);
                m_resultVerticepositions[vertexIndexPosition + 3] = position + ((cQ * m_ptr) * Vector3.forward * radius);



            }
        }


        public void SetBorderLocal()
        {
            this.m_pbl = Quaternion.LookRotation(new Vector3(1, -1, 0), Vector3.up);
            this.m_pbr = Quaternion.LookRotation(new Vector3(-1, -1, 0), Vector3.up);
            this.m_ptl = Quaternion.LookRotation(new Vector3(1, 1, 0), Vector3.up);
            this.m_ptr = Quaternion.LookRotation(new Vector3(-1, 1, 0), Vector3.up);
        }
        public static Vector3 Rotated(Vector3 vector, Quaternion rotation, Vector3 pivot = default(Vector3))
        {
            return rotation * (vector - pivot) + pivot;
        }

        public static Vector3 Rotated(Vector3 vector, Vector3 rotation, Vector3 pivot = default(Vector3))
        {
            return Rotated(vector, Quaternion.Euler(rotation), pivot);
        }

        public static Vector3 Rotated(Vector3 vector, float x, float y, float z, Vector3 pivot = default(Vector3))
        {
            return Rotated(vector, Quaternion.Euler(x, y, z), pivot);
        }


    }
}
