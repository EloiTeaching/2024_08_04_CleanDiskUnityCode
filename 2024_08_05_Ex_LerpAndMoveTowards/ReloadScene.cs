using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public KeyCode m_input;

    void Update()
    {
        if (Input.GetKeyDown(m_input)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
