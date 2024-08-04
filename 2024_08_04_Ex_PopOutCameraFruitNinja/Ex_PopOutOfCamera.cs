using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_PopOutOfCamera : MonoBehaviour
{

    public GameObject m_defaultPrefabToSpawn;
    public float m_topHeightPadding = 0.5f;
    public float m_depthPadding = 2;
    public float m_minSize = 0.5f;
    public float m_maxSize = 1.5f;
    public float m_destroyTime = 5;
    public Transform m_parentToCreateIn;




    [ContextMenu("Spawn Prefab")]
    public void SpawnInstanceOfPrefab()
    { 

        SpawnInstanceOfPrefab  (m_defaultPrefabToSpawn);    
    }

    public void SpawnInstanceOfPrefab(GameObject prefab)
    {
        if(Camera.main == null) {
            return;
        }
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, m_topHeightPadding, m_depthPadding));
        Quaternion rotationOfCamera= Camera.main.transform.rotation;
        if (m_defaultPrefabToSpawn) { 
        
            GameObject g = Instantiate(m_defaultPrefabToSpawn, worldPosition, rotationOfCamera);
            float randomSize = Random.Range(m_minSize, m_maxSize);
            Destroy(g, m_destroyTime);
            g.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            if (m_parentToCreateIn) {
                g.transform.SetParent(m_parentToCreateIn);
            }
        }
    }

}
