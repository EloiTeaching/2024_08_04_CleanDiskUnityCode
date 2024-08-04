using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_MapCreatedByTexture : MonoBehaviour
{
    public Texture2D  m_texture;
    public GameObject m_prefab;
    public Transform m_whereToCreate;

    [ContextMenu("Create Map")]
    public void CreateMap() {

        DeleteInContainer();
        for (int x = 0; x < m_texture.width; x++) {
            for (int y = 0; y < m_texture.height; y++) {
                Color color = m_texture.GetPixel(x, y);
                if (color.a == 0 || (color.r <= 0 && color.g <= 0 && color.b <= 0) ) { 

                    GameObject go = Instantiate(m_prefab, m_whereToCreate);
                    go.transform.localPosition = new Vector3(x, 0, y);
                    go.transform.localScale = Vector3.one;
                    go.transform.localRotation = Quaternion.identity;

                }
            }
        }
    }

    [ContextMenu("Destroy Childrens")]
    private void DeleteInContainer()
    {
        for (int i = m_whereToCreate.childCount - 1; i >= 0; i--)
        {
            if(Application.isEditor && !Application.isPlaying)
                DestroyImmediate(m_whereToCreate.GetChild(i).gameObject);
            else
            Destroy(m_whereToCreate.GetChild(i).gameObject);
        }
    }
}
