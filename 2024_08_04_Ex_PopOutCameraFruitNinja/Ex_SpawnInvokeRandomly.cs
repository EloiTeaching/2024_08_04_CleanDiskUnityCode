using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ex_SpawnInvokeRandomly : MonoBehaviour
{

    public float m_minTime = 1.0f;
    public float m_maxTime = 2.0f;
    public UnityEvent m_onTick;
    public bool m_invokeAtCoroutineStart = true;


    public void OnEnable()
    {
        StartCoroutine(SpawnRandomly());
    }

    private IEnumerator SpawnRandomly()
    {
        if (m_invokeAtCoroutineStart)
        {
            m_onTick.Invoke();
        }
        while (true) { 

            
            yield return new WaitForSeconds(UnityEngine.Random.Range(m_minTime, m_maxTime));
            m_onTick.Invoke();
            yield return new WaitForEndOfFrame();
        }
    }
}
