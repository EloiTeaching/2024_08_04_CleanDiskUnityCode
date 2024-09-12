using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCheckpointCollisionMono : MonoBehaviour
{
    public GameObject m_whatToAffect;
   
    public LayerMask m_layerMask;

    public void Display()
    {
        Debug.Log("Display", this.gameObject);
        m_whatToAffect.SetActive(true);
    }
    public void Hide()
    {
        Debug.Log("Hide", this.gameObject);
        m_whatToAffect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter", this.gameObject);
        Hide();
    }


}
