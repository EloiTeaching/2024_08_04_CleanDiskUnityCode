using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ex_Event : MonoBehaviour
{
    public KeyCode m_input = KeyCode.Space;
    public UnityEvent m_onInputDown;

    public Color m_colorValue = Color.white;
    public OnColorChangedEvent m_onColorChange;
    private Color m_previousValue;

    void Start()
    {
        m_onColorChange.AddListener(DebugColorChanged);


    }
    public void Update()
    {
        if (Input.GetKeyDown(m_input))
            m_onInputDown.Invoke();
    }



    public void DebugColorChanged(Color color) {
        Debug.Log("Color change: " + color.ToString());
    }


    public void OnValidate()
    {
        if (m_previousValue != m_colorValue) {
            m_previousValue = m_colorValue;
            m_onColorChange.Invoke(m_colorValue);
        }    
    }
}

[System.Serializable]
public class OnColorChangedEvent : UnityEvent <Color>{

}