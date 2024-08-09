using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class TestStorageAndroidSleepyScript : MonoBehaviour
{

    public Text m_directoryPath;
    public string p_directoryPath;
    public void Open_Directory() => Application.OpenURL(p_directoryPath);

    public Text m_dataPath;
    public string p_dataPath;
    public void Open_DataPath() => Application.OpenURL(p_dataPath);

    public Text m_permaPath;
    public string p_permaPath;
    public void Open_PermaPath() => Application.OpenURL(p_permaPath);

    public Text m_streamPath;
    public string p_streamPath;
    public void Open_StreamPath() => Application.OpenURL(p_streamPath);

    public Text m_temporyPath;
    public string p_temporyPath;
    public void Open_TempPath() => Application.OpenURL(p_temporyPath);

    public Text m_rootPathWeb0;
    public string p_rootPathWeb0;
    public void Open_RootPathWeb() => Application.OpenURL(p_rootPathWeb0);

    public Text m_guessRootPath;
    public string p_guessRootPath;
    public void Open_GuessRoot() => Application.OpenURL(p_guessRootPath);

    public Text m_sdPath0;
    public string p_sdPath0;
    public void Open_SDPath0() => Application.OpenURL(p_sdPath0);

    public Text m_javaExternal;
    public string p_javaExternal;
    public void OpenJavaExternal() => Application.OpenURL(p_javaExternal);

    public string m_pathDoc = "https://gist.github.com/lopspower/76421751b21594c69eb2";
    public string[] m_possibleRootPath = new string[]{
        "/storage/sdcard0/Android/data/",
        "/storage/sdcard0",
        "/",
        ""
        };

    public void Start()
    {
        //#if PLATFORM_ANDROID
        //        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        //        {
        //            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        //        }
        //        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        //        {
        //            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        //        }
        //#endif
        Refresh();
    }
    public bool E(string path)
    {
        return Directory.Exists(path);
    }
    public string m_helloToday = "Hello";
    public void Refresh()
    {

        m_helloToday += " " + DateTime.Now;

        p_directoryPath = Directory.GetCurrentDirectory();
        m_directoryPath.text = "Directory|" + E(p_directoryPath) + "|" + p_directoryPath;
        try { File.WriteAllText(p_directoryPath + "/E_CurrentDirectory.txt", m_helloToday); } catch (Exception) { };

        p_dataPath = Application.dataPath;
        m_dataPath.text = "Data|" + E(p_dataPath) + "|" + p_dataPath;
        try
        {
            File.WriteAllText(p_dataPath + "/E_DataPath.txt", m_helloToday);
        }
        catch (Exception) { };

        p_permaPath = Application.persistentDataPath;
        m_permaPath.text = "Perma|" + E(p_permaPath) + "|" + p_permaPath;
        try { File.WriteAllText(p_permaPath + "/E_PermaPath.txt", m_helloToday); } catch (Exception) { };

        p_streamPath = Application.streamingAssetsPath;
        m_streamPath.text = "Stream|" + E(p_streamPath) + "|" + p_streamPath;
        try { File.WriteAllText(p_streamPath + "/E_StreamPath.txt", m_helloToday); } catch (Exception) { };

        p_temporyPath = Application.temporaryCachePath;
        m_temporyPath.text = "Temp|" + E(p_temporyPath) + "|" + p_temporyPath;
        try { File.WriteAllText(p_temporyPath + "/E_TemporyPath.txt", m_helloToday); } catch (Exception) { };
        bool isOnAndroid = false;
#if UNITY_ANDROID && !UNITY_EDITOR
        isOnAndroid = true;
#endif
        if (isOnAndroid) { 
            p_rootPathWeb0 = Application.persistentDataPath.Substring(0,
                        Application.persistentDataPath.IndexOf("Android", StringComparison.Ordinal));
            m_rootPathWeb0.text = "Root Perma|" + E(p_rootPathWeb0) + "|" + p_rootPathWeb0;
            try { File.WriteAllText(p_rootPathWeb0 + "/E_RootPathWeb0.txt", m_helloToday); } catch (Exception) { };

            p_sdPath0 = "/storage/sdcard0";
            m_sdPath0.text = "Storage /storage/sdcard0 exist ?|" + Directory.Exists(p_sdPath0);
            try { File.WriteAllText(p_sdPath0 + "/Storesdcar0.txt", m_helloToday); } catch (Exception) { };

            TryToGuessRootPath(out bool found, out string path);
            p_guessRootPath = path;
            if (found)
                m_guessRootPath.text = "Root|" + E(p_guessRootPath) + "|" + p_guessRootPath;
            else m_guessRootPath.text = "Root|" + "Not found custom Path";
            try { File.WriteAllText(p_sdPath0 + "/E_Guess.txt", m_helloToday); } catch (Exception) { };

            p_javaExternal = GetAndroidExternalStoragePath();
            m_javaExternal.text = "Java External|" + Directory.Exists(p_javaExternal) + "|" + p_javaExternal; ;
            try { File.WriteAllText(p_javaExternal + "/JavaExternal.txt", m_helloToday); } catch (Exception) { };

        }

    }

    private void TryToGuessRootPath(out bool found, out string path)
    {
        for (int i = 0; i < m_possibleRootPath.Length; i++)
        {
            if (Directory.Exists(m_possibleRootPath[i]))
            {
                found = true;
                path = m_possibleRootPath[i];
                return;
            }
        }
        path = "";
        found = false;
    }


    public string GetAndroidExternalStoragePath()
    {
        string path = "";
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
            path = jc.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");
            return path;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return "";
    }
    public void  GetAndroidExternalStoragePath2()
    {
        AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
        string path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", jc.GetStatic<string>("DIRECTORY_DCIM")).Call<string>("getAbsolutePath");
        Debug.Log("Attempting to recover DCIM External Storage Directory: " + path);
    }
}
