/*
 * --------------------------BEER-WARE LICENSE--------------------------------
 * PrIMD42@gmail.com wrote this file. As long as you retain this notice you
 * can do whatever you want with this code. If you think
 * this stuff is worth it, you can buy me a beer in return, 
 *  S. E.
 * Donate a beer: http://www.primd.be/donate/ 
 * Contact: http://www.primd.be/
 * ----------------------------------------------------------------------------
 */
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

using System.Linq;
using System.Reflection;
using System.Collections;
using Object = UnityEngine.Object;


public class Shortcuts 
{

	[MenuItem("Tools/Shortcut/Create Empty Point %#e")]
	static void GoCreateObjectInParent() {
		CreateEmptyPointInParent ("Empty Point");
	}


	static GameObject CreateEmptyPointInParent(string name){
		GameObject go = new GameObject(name);
		if(Selection.activeTransform != null)
			go.transform.parent = Selection.activeTransform;
		return go;

	}

	[MenuItem("Tools/Shortcut/Group Object %#g")]
	static void GroupMeThat() {

		//Create the group
		GameObject group  = CreateEmptyPointInParent ("Group");
		//Get the destination
		Transform destination = null; 
		Transform lastSelected = Selection.activeTransform;
		if (lastSelected != null) {

			destination= lastSelected.parent;

			group.transform.parent=destination;

			foreach (GameObject g in Selection.objects)
								g.transform.parent = group.transform;
				}

	}


	private static EditorWindow _mouseOverWindow;
	[MenuItem("Tools/Shortcut/Lock Inspect. %#t")]
	
	static void ToggleInspectorLock()
		
	{
		
		if (_mouseOverWindow == null)
			
		{
			
			if (!EditorPrefs.HasKey("LockableInspectorIndex"))
				
				EditorPrefs.SetInt("LockableInspectorIndex", 0);
			
			int i = EditorPrefs.GetInt("LockableInspectorIndex");
			
			
			
			Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
			
			Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
			
			_mouseOverWindow = (EditorWindow)findObjectsOfTypeAll[i];
			
		}
		
		
		
		if (_mouseOverWindow != null && _mouseOverWindow.GetType().Name == "InspectorWindow")
			
		{
			
			Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
			
			PropertyInfo propertyInfo = type.GetProperty("isLocked");
			
			bool value = (bool)propertyInfo.GetValue(_mouseOverWindow, null);
			
			propertyInfo.SetValue(_mouseOverWindow, !value, null);
			
			_mouseOverWindow.Repaint();
			
		}
		
	}
}
#endif