using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_OnValidate : MonoBehaviour
{

    public string m_nameWithoutSpace;

    private void OnValidate()
    {
        RemoveSpace();
    }

    private void RemoveSpace()
    {
        m_nameWithoutSpace =m_nameWithoutSpace.Replace(" ", "");
    }
}
