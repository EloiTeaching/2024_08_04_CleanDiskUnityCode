using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_MovingSky : MonoBehaviour
{

    public Material m_skyMaterial;
    public float m_offset;
    public float m_speed=2f;
    
    void Update()
    {
        Vector2 offset = m_skyMaterial.GetTextureOffset("_MainTex");
        offset.x += Time.deltaTime* m_speed;
        if (offset.x < 0)
            offset.x = 1f;
        if (offset.x > 1)
            offset.x = 0f;

        m_skyMaterial.SetTextureOffset("_MainTex", offset);
       
    }
}
