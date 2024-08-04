using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_SetTileOffsetOfTexture : MonoBehaviour
{
    public Material m_materialToAffect;
    public int m_tileCount = 8;
    public int m_tileXLeftRight;
    public int m_tileYDownTop;

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
        m_materialToAffect.mainTextureScale = new Vector2(1f / m_tileCount, 1f / m_tileCount);
        m_materialToAffect.mainTextureOffset = new Vector2(1f / m_tileCount * ( m_tileXLeftRight), 1f / m_tileCount * ( m_tileYDownTop));
    }

    public void SetWith(int leftRightX, int downTopY)
    {
        m_tileXLeftRight = leftRightX;
        m_tileYDownTop = downTopY;
        OnValidate();
    }

}
