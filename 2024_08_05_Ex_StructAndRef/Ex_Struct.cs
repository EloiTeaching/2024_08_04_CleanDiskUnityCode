using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_Struct : MonoBehaviour
{
    public List<EnemyInfo> m_enemiesInScene;

    public EnemyInfoScritable m_enemyChangeable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy/Type", order = 1)]
public class EnemyInfoScritable : ScriptableObject
{
    public EnemyInfo m_info;
    public float m_strenght;
}


[System.Serializable]
public class EnemyInfo
{
    public string m_name;
    public GameObject m_prefab;
    public int m_level;
}