using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAndroidApplicationDirectoryMono : DefaultDirectoryInDataPathStorageMono
{



    public override void GetPath(out string path)
    {
#if UNITY_ANDROID &&  !UNITY_EDITOR
        GetDataPath(out IMetaAbsolutePathDirectoryGet folder);
        GetSubFolder(out IMetaRelativePathDirectoryGet subFolder);
        path = Eloi.E_FileAndFolderUtility.Combine( folder, subFolder).GetPath() ;
#else
        base.GetPath(out path);
#endif

    }

    public void GetDataPath(out IMetaAbsolutePathDirectoryGet folder)
    {
        folder = new Eloi.MetaAbsolutePathDirectory(Application.dataPath);
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
}