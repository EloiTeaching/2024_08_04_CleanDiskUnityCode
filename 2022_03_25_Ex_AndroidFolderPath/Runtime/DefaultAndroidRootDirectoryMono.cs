using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAndroidRootDirectoryMono : DefaultDirectoryInDataPathStorageMono
{


    public override void GetPath(out string path)
    {
#if UNITY_ANDROID &&  !UNITY_EDITOR
        GetAndroidExternalStoragePath(out IMetaAbsolutePathDirectoryGet folder);
        GetSubFolder(out IMetaRelativePathDirectoryGet subFolder);
        path = Eloi.E_FileAndFolderUtility.Combine( folder, subFolder).GetPath() ;
#else
        base.GetPath(out path);
#endif

    }

    public void GetAndroidExternalStoragePath(out IMetaAbsolutePathDirectoryGet folder)
    {
        folder = new Eloi.MetaAbsolutePathDirectory(GetAndroidExternalStoragePath());
    }

    public void GetSubFolder(out IMetaRelativePathDirectoryGet subFolder)
    {
        subFolder = (m_subfolders);
    }

    public override string GetPath()
    {
        this.GetPath(out string p);
        return p;
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
        return "/";
    }

}
