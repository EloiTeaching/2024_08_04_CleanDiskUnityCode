using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_CreateAndImportPermaFolder : MonoBehaviour
{
    public DefaultDirectoryInDataPathStorageMono m_whereToStore;
    public Text m_debugText;


    public void Start()
    {
        Eloi.E_FileAndFolderUtility.CreateOrOverrideFile(m_whereToStore, "Hey Yo",  DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")+ "_Test", "txt");
        Eloi.E_FileAndFolderUtility.GetAllfilesInAndInChildren(m_whereToStore, out string[] files);
        m_debugText.text = string.Join("\n", files);
    }

}
