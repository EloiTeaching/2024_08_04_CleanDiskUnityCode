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


using System;
using UnityEditor;
using UnityEngine;
 		[CustomEditor(typeof(Refresher))]
public class RefresherEditor : Editor
{
				
	public override void OnInspectorGUI()
	{

		base.OnInspectorGUI();
		Refresher r = (Refresher) target;
		EditorGUILayout.LabelField  ("Sometime: "+r.GetLenght(Refresher.E_RefreshType.Sometime ));
		EditorGUILayout.LabelField("Each Second: "+r.GetLenght(Refresher.E_RefreshType.EachSecond ));
		EditorGUILayout.LabelField("Often: "+r.GetLenght(Refresher.E_RefreshType.Often ));
		EditorGUILayout.LabelField("Quick: "+r.GetLenght(Refresher.E_RefreshType.Quick ));
		EditorGUILayout.LabelField("Update like: "+r.GetLenght(Refresher.E_RefreshType.UpdateLike ));
		EditorGUILayout.LabelField("Exe. average: "+r.GetAverage());
		EditorGUILayout.LabelField("Exe. average Update: "+r.GetAverageBetweenUpdate());
	}



}


