using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BestScoreOfLobbyMono : MonoBehaviour
{
    public float m_secondSinceStart=0;
    public float m_lastScore = 0;
    public float m_bestScoreOfLobby = 0;
    public float m_bestScoreOfEver = 0;
    public bool m_isTimerRunning = false;
    public float m_defautlBestScoreOnReset = 600;

    public Text m_uiCurrentScore;
    public Text m_uiBestScore;

    public bool m_launchAtStart = true;


    public void Start()
    {

        if(m_launchAtStart)
        {
            StartTimer();
        }


        m_bestScoreOfLobby = m_defautlBestScoreOnReset;
        m_bestScoreOfEver = m_defautlBestScoreOnReset;
        string pathFile = Application.persistentDataPath + "/BestScoreOfEver.txt";

        if (File.Exists(pathFile))
        {
            string readFileText = File.ReadAllText(pathFile);
            float monMeilleurScore = float.Parse(readFileText);
            m_bestScoreOfEver = monMeilleurScore;
        }
        else
        {
            m_bestScoreOfEver = m_defautlBestScoreOnReset;
        }
    }

    private void OnApplicationQuit()
    {
        SaveTheBestScoreOnDisk();
    }

    private void OnDestroy()
    {
        SaveTheBestScoreOnDisk();
    }


    [ContextMenu("Reset All Score at Default")]
    public void ResetAllScoreAtDefault() {

        StopTimer();
        m_isTimerRunning = false;
        m_lastScore = 0;
        m_secondSinceStart = 0;
        m_bestScoreOfLobby = m_defautlBestScoreOnReset;
        m_bestScoreOfEver = m_defautlBestScoreOnReset;
        SaveTheBestScoreOnDisk();

        m_uiBestScore.text = string.Format("{0:0.00}", m_bestScoreOfLobby);
        m_uiCurrentScore.text = "0";

    }


    [ContextMenu("Start Timer")]
    public void StartTimer() { 
        m_isTimerRunning = true;
        m_secondSinceStart = 0;
    }

    [ContextMenu("Stop Timer")]
    public void StopTimer() {
        m_isTimerRunning = false;
        m_lastScore = m_secondSinceStart;

        if (m_secondSinceStart < m_bestScoreOfLobby) { 
            m_bestScoreOfLobby = m_secondSinceStart;
        }

        if (m_secondSinceStart < m_bestScoreOfEver) { 
            m_bestScoreOfEver = m_secondSinceStart;
            //PlayerPrefs.SetFloat("BestScoreOfEver", m_bestScoreOfEver);
            //PlayerPrefs.SetString("BestScoreOfEverDate", DateTime.Now.ToString());
            SaveTheBestScoreOnDisk();
        }
            
        m_uiBestScore.text =string.Format("{0:0.00}", m_bestScoreOfLobby);
    }

    public void SaveTheBestScoreOnDisk() {
        string pathFile = Application.persistentDataPath + "/BestScoreOfEver.txt";
        //Debug.Log("File Save:" + pathFile);
        File.WriteAllText(pathFile, m_bestScoreOfEver.ToString());
    }


    void Update()
    {
        if(m_isTimerRunning)
            m_secondSinceStart += Time.deltaTime;

        int second = (int)(m_secondSinceStart*100);
        float trimSeconds= (float)second / 100f;
        m_uiCurrentScore.text = trimSeconds.ToString();
    }




    // Start is called before the first frame update
    //void Start()
    //{

    //    m_whenStart = DateTime.Now;
    //    m_whenStop = DateTime.Now;
    //    float secondsPast = (float)(m_whenStop - m_whenStart).TotalSeconds;
    //}
    //public DateTime m_whenStart;
    //public DateTime m_whenStop;

}
