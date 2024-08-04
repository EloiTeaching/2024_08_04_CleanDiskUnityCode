using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_SetTileOffsetOfTextureXY : MonoBehaviour
{
    public Material m_materialToAffect;
    public int m_tileCountX = 4;
    public int m_tileCountY = 3;
    public int m_tileXLeftRight;
    public int m_tileYDownTop;

    public bool m_createInstanceAtStart = true;

    private void Awake()
    {
        if (m_createInstanceAtStart)
        {
            m_materialToAffect = new Material(m_materialToAffect);
            GetComponent<Renderer>().material = m_materialToAffect;
        }
    }

    private void Reset()
    {
        m_materialToAffect = GetComponent<Renderer>().material;
    }

    private void OnValidate()
    {
        RefreshTileAndoffset();
    }

    private void RefreshTileAndoffset()
    {
        m_materialToAffect.mainTextureScale = new Vector2(1f / m_tileCountX, 1f / m_tileCountY);
        m_materialToAffect.mainTextureOffset = new Vector2((1f / m_tileCountX) * ( m_tileXLeftRight), (1f / m_tileCountY) * ( m_tileYDownTop));
    }

    public void SetWith(int leftRightX, int downTopY)
    {
        m_tileXLeftRight = leftRightX;
        m_tileYDownTop = downTopY;
        OnValidate();
    }

}
