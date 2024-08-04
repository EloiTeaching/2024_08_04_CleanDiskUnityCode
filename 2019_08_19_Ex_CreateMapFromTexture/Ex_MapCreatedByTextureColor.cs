using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_MapCreatedByTextureColor : MonoBehaviour
{
    public Texture2D m_textureToUse;
    public Transform m_whereToCreate;

    public List<Color32ToPrefab> m_color32ToPrefabs;


    [System.Serializable]
    public class Color32ToPrefab
    {
        public Color32 color;
        public GameObject prefab;

        public bool IsSameColor(Color32 color)
        {
            return this.color.r == color.r && this.color.g == color.g && this.color.b == color.b;
        }
    }

    private void Reset()
    {
        m_color32ToPrefabs= new List<Color32ToPrefab>();
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(0, 0, 0, 255), prefab = null });
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(255, 0, 0, 255), prefab = null });
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(0, 255, 0, 255), prefab = null });
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(0, 0, 255, 255), prefab = null });
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(0, 255, 255, 255), prefab = null });
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(255, 0, 255, 255), prefab = null });
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(255, 255, 0, 255), prefab = null });
        m_color32ToPrefabs.Add(new Color32ToPrefab() { color = new Color32(255, 255, 255, 255), prefab = null });
    }

    [ContextMenu("Create Map")]
    public void CreateMap()
    {

        DeleteInContainer();
        Color32[] colors = m_textureToUse.GetPixels32(); 
        for (int x = 0; x < m_textureToUse.width; x++)
        {
            for (int y = 0; y < m_textureToUse.height; y++)
            {
                int index = y * m_textureToUse.width + x;
                Color32 color = colors[index];
                Color32ToPrefab color32ToPrefab = m_color32ToPrefabs.Find(c => c.IsSameColor(color));
                if (color32ToPrefab != null)
                {
                    GameObject prefab = color32ToPrefab.prefab;
                    if (prefab != null) { 
                        GameObject go = Instantiate(prefab, m_whereToCreate);
                        go.transform.localPosition = new Vector3(x, 0, y);
                        go.transform.localScale = Vector3.one;
                        go.transform.localRotation = Quaternion.identity;
                    }
                }
                
            }
        }
    }

    [ContextMenu("Delete Childs")]
    private void DeleteInContainer()
    {
        for (int i = m_whereToCreate.childCount - 1; i >= 0; i--)
        {
            if (Application.isEditor && !Application.isPlaying)
                DestroyImmediate(m_whereToCreate.GetChild(i).gameObject);
            else
                Destroy(m_whereToCreate.GetChild(i).gameObject);
        }
    }
}
