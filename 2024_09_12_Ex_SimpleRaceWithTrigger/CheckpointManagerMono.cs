using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManagerMono : MonoBehaviour
{

    public GameObject[] m_checkpoints;
    
    public bool m_atLeastOneActive = false;
    public bool m_allCheckpointsDeactivated;
    private bool m_previousValue;
    public UnityEvent m_endRace;
    public UnityEvent m_startRace;


    void Update()
    {

        int i = 0;
        int stop_arrayLenght = m_checkpoints.Length;

        bool isOneActive = false;

        while (i < stop_arrayLenght) {
            if (m_checkpoints[i].activeSelf)
            {
                isOneActive = true;
                break;
            }
            i = i + 1;
        }
        m_atLeastOneActive = isOneActive;

        m_previousValue = m_allCheckpointsDeactivated;
        m_allCheckpointsDeactivated = !isOneActive;

        if (m_previousValue != m_allCheckpointsDeactivated)
        {
            if (m_allCheckpointsDeactivated) { 
                m_endRace.Invoke();
            }
            else
            {
                m_startRace.Invoke();
            }
        }


    }

    [ContextMenu("Display All")]
    public void DisplayAll()
    {
        for (int i = 0; i < m_checkpoints.Length; i++)
        {
            m_checkpoints[i].SetActive(true);
        }
    }
    [ContextMenu("Hide All")]
    public void HideAll()
    {
        for (int i = 0; i < m_checkpoints.Length; i++)
        {
            m_checkpoints[i].SetActive(false);
        }
    }
    [ContextMenu("Random All")]
    public void RandomAll()
    {
        for (int i = 0; i < m_checkpoints.Length; i++)
        {
            bool random = Random.Range(0, 2) == 0;
            m_checkpoints[i].SetActive(random);
        }
    }
}
