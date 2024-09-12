using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCollisionMono : MonoBehaviour
{
    public UnityEvent m_onTriggerEnter;
    public UnityEvent m_onTriggerExit;
  
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        m_onTriggerEnter.Invoke();
    }
    public void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit");
        m_onTriggerExit.Invoke();
    }
}
