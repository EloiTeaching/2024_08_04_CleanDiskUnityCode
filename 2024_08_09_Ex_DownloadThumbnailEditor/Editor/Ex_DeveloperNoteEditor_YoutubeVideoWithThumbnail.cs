//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//namespace Eloi
//{
//    [CustomEditor(typeof(Ex_YoutubeThumbnailDownload))]
//    public class Ex_DeveloperNoteEditor_YoutubeVideoWithThumbnail : Editor
//    {

//        //https://www.youtube.com/watch?v=s6TMa33niJo&t=133s
//        public override void OnInspectorGUI()
//        {
//            Ex_YoutubeThumbnailDownload myScript = (Ex_YoutubeThumbnailDownload)target;
//            if (myScript.m_thumbnail != null)
//                Ex_DeveloperNoteEditorImageUtility.DrawImage(myScript.m_thumbnail, Open);
//            if (GUILayout.Button("Open On youtube"))
//            {
//                myScript.OpenYoutube();
//            }
//            base.DrawDefaultInspector();
//        }

//        private void Open()
//        {
//            DeveloperNote_YoutubeVideoWithThumbnail myScript = (DeveloperNote_YoutubeVideoWithThumbnail)target;
//            myScript.OpenYoutube();

//        }

//    }
//}
