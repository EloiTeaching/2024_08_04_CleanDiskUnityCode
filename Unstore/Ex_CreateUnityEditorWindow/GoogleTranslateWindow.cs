#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class GoogleTranslateWindow : EditorWindow {
	
	public enum GoogleLanguage : int {FR=0,EN=1}

	private GoogleLanguage from = GoogleLanguage.FR;
	private GoogleLanguage[] fromList ;
	private int fromCursor = (int) GoogleLanguage. FR;

	private GoogleLanguage to = GoogleLanguage.EN;
	private GoogleLanguage [] toList = new GoogleLanguage[] {GoogleLanguage.FR,GoogleLanguage.EN};
	private int toCursor = (int) GoogleLanguage. EN;

	public string word="";
	public string translation="";
	
	private bool shortcutShow;
	public bool horizontal;
	private static GUIStyle labelStyle ;
	
	[MenuItem ("Window/Google Translate")]
	static void Init () {
		EditorWindow.GetWindow (typeof (GoogleTranslateWindow));
	
	}

	void OnGUI () {
		if (fromList == null || (fromList != null && fromList.Length == 0)) {
			fromList = new GoogleLanguage[] {GoogleLanguage.FR,GoogleLanguage.EN};
		}
		
		if (toList == null || (toList != null && toList.Length == 0)) {
			toList = new GoogleLanguage[] {GoogleLanguage.FR,GoogleLanguage.EN};
		}
		

		if (labelStyle == null) {
			this.minSize=new Vector2(100,20);
		
			this.name= "Google Translate";
			this.title="Google Trans.";
			labelStyle = new GUIStyle();	
			labelStyle.stretchWidth=true;
			labelStyle.fixedWidth = 30;
			labelStyle.normal.textColor = Color.grey;

		}

					EditorGUILayout.BeginHorizontal ();
						
						bool okToTranslate = false;
						if (word.Contains ("   ")) {
							word = word.Replace ("   ", "");
							okToTranslate = true;
		}
		if (okToTranslate || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown ("enter")) {
			okToTranslate = false;
			System.Diagnostics.Process.Start ("http://translate.google.be/?hl=" + from.ToString () + "#" + from.ToString () + "/" + to.ToString () + "/" + word);	
			
		}
		if (GUILayout.Button ("Translate")) {
			System.Diagnostics.Process.Start ("http://translate.google.be/?hl=" + from.ToString () + "#" + from.ToString () + "/" + to.ToString () + "/" + word);	
			
		}

						word = EditorGUILayout.TextField ( word);
						if (GUILayout.Button (from.ToString ())) {
							fromCursor = ((fromCursor + 1) % fromList.Length);
							from = fromList [fromCursor];
						}
						if (GUILayout.Button ("<->")) {
							GoogleLanguage tmp = from;
							from = to;
							to = tmp;
						}
						if (GUILayout.Button (to.ToString ())) {
							toCursor = ((toCursor + 1) % toList.Length);
							to = fromList [toCursor];
						}

						

					EditorGUILayout.EndHorizontal();

	}


}
#endif