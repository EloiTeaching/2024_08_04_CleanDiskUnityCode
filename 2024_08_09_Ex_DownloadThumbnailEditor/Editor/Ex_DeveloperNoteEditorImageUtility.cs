using System.Diagnostics;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;

public class Ex_DeveloperNoteEditorImageUtility
{

    public static void OpenInBrowser(Texture2D texture)
    {
        Texture2D t = null;
        string path = Application.temporaryCachePath + "/temp.png";
        if (texture.isReadable == false)
        {
            CopyTextureNotReadable(texture, out t);
        }
        else
        {
            t = texture;
        }


        File.WriteAllBytes(path, t.EncodeToPNG());
        Application.OpenURL(path);
    }
    public static void OpenInUnity(Texture2D texture)
    {
        Ex_DeveloperNoteMenuEditor_DisplayImage.CreateWindow(texture, () => { OpenInBrowser(texture); });
    }
    public static void CopyTextureNotReadable(Texture2D originalTexture, out Texture2D copy)
    {

        RenderTexture renderTexture = new RenderTexture(originalTexture.width, originalTexture.height, 0);
        Texture2D readableTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        Graphics.Blit(originalTexture, renderTexture);
        readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        readableTexture.Apply();
        copy = readableTexture;
        RenderTexture.active = null;
        renderTexture.Release();

    }


    public static void DrawImage(Texture2D texture, Action toDoOnClick = null)
    {

        //if (!texture.isReadable)
        //{
        //    E_Texture2DUtility.CopyWithRenderer( texture ,out texture);
        //}

        if ((DateTime.Now - m_lastSizeUpdate).Milliseconds > 400)
        {
            GetViewWidth();
            m_lastSizeUpdate = DateTime.Now;
        }
        if (texture == null)
            return;
        GUILayout.BeginHorizontal();
        GUIStyle style = new GUIStyle();
        style.normal.background = texture;
        style.margin = new RectOffset(2, 2, 2, 2);
        style.alignment = TextAnchor.MiddleCenter;
        float ratio = texture.height / (float)texture.width;
        if (GUILayout.Button("", style, GUILayout.Width(m_width), GUILayout.Height(m_width * ratio)))
        {
            if (toDoOnClick != null)
                toDoOnClick.Invoke();
            else OpenInBrowser(texture);
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Web", GUILayout.Width(m_width / 3f)))
        {
            if (toDoOnClick != null)
                toDoOnClick.Invoke();
            else OpenInBrowser(texture);
        }
        if (GUILayout.Button("Unity", GUILayout.Width(m_width / 3f)))
        {
            if (toDoOnClick != null)
                Ex_DeveloperNoteMenuEditor_DisplayImage.CreateWindow(texture, toDoOnClick.Invoke);
            else OpenInUnity(texture);
        }
        if (GUILayout.Button("Source", GUILayout.Width(m_width / 3f)))
        {


            if (texture != null)
            {
                string absolutePath = GetAbsolutePathInEditor(texture);

                Application.OpenURL(absolutePath);
            }



        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (m_isBraveExists)
            if (GUILayout.Button("Brave", GUILayout.Width(m_width / 3f)))
            {
                string absolutePath = GetAbsolutePathInEditor(texture);
                OpenUrlWithProcess(absolutePath, browserPathBrave);
            }
        if (m_isChromeExists)
            if (GUILayout.Button("Chrome", GUILayout.Width(m_width / 3f)))
            {
                string absolutePath = GetAbsolutePathInEditor(texture);
                OpenUrlWithProcess(absolutePath, browserPathChrome);
            }
        if (m_isFirefoxExists)
            if (GUILayout.Button("Firefox", GUILayout.Width(m_width / 3f)))
            {
                string absolutePath = GetAbsolutePathInEditor(texture);
                OpenUrlWithProcess(absolutePath, browserPathFirefox);
            }


        GUILayout.EndHorizontal();

    }

    private static string GetAbsolutePathInEditor(Texture2D texture)
    {
        string relativepath = AssetDatabase.GetAssetPath(texture);
        string absolutePath = Path.GetFullPath(Path.Combine(Application.dataPath, relativepath.Substring("Assets".Length + 1)));
        return absolutePath;
    }

    public static bool ExistsPath(string path)
    {
        return File.Exists(path);
    }
    public static void OpenUrlWithProcess(string url, string browserExe)
    {

        if (File.Exists(browserExe))
        {
            Process.Start(browserExe, url);
        }

    }


    static float m_width;
    static private Rect _rect;
    static DateTime m_lastSizeUpdate;
    static bool m_isChromeExists = false;
    static bool m_isFirefoxExists = false;
    static bool m_isBraveExists = false;
    public static string browserPathChrome = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
    public static string browserPathFirefox = @"C:\Program Files\Mozilla Firefox\firefox.exe";
    public static string browserPathBrave = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";

    private static float GetViewWidth()
    {

        m_isChromeExists = File.Exists(browserPathChrome);
        m_isFirefoxExists = File.Exists(browserPathFirefox);
        m_isBraveExists = File.Exists(browserPathBrave);


        float w = EditorGUIUtility.currentViewWidth;
        m_width = 250;
        return m_width;
        //if (Mathf.Abs(m_width - w) > 50)
        //    m_width = w;
        //return m_width;
    }

    public static void WarningAboutSizeB64()
    {

        GUILayout.Label("Git: Note that image take place in scene size if not in a prefab.");
    }
}