

using UnityEngine;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Refresher : MonoBehaviour {



	private static Refresher INSTANCE ;
	public  static Refresher GetInstance() {return INSTANCE;}

	void Awake()
	{
		if(INSTANCE==null) INSTANCE=this;
		else {
			Debug.LogError("Only one refresher is accepted by scene, this one is delete:"+this +" ("+this.gameObject+")");
			Destroy(this);
		}

		GameObject[] objs = GameObject.FindObjectsOfType<GameObject>() as GameObject [];


		Component [] tabr  = null;
//		int i =0;
		foreach(GameObject go  in objs)
		{

			tabr = (Component [] ) go.GetComponents(typeof(I_Refreshable))  ;
			if(tabr!=null){
				foreach( I_Refreshable r in tabr)
					if (r!=null)
						Add(r, r.GetRefreshType());
			}
		}


	}

	private float time;
	private float executionAverageTime;
	private float timeBetweenUpdate;
	public float GetAverage(){return executionAverageTime;}
	public float GetAverageBetweenUpdate(){return timeBetweenUpdate;}
	
	public float sometime =1.5f;
	private float lastSometime ;
	private LinkedList<I_Refreshable> listSometime = new LinkedList<I_Refreshable>();

	public readonly float eachsecond =1f;
	private float lastEachsecond ;
	private LinkedList<I_Refreshable> listEachSecond = new LinkedList<I_Refreshable>();
	
	public float often =0.3f;
	private float lastOften;
	private LinkedList<I_Refreshable> listOften = new LinkedList<I_Refreshable>();

	public float quick =0.1f;
	private float lastQuick;
	private LinkedList<I_Refreshable> listQuick = new LinkedList<I_Refreshable>();

	public float update =0.03f;
	private float lastUpdate;
	private LinkedList<I_Refreshable> listUpdate = new LinkedList<I_Refreshable>();


	void FixedUpdate () {
		time = Time.timeSinceLevelLoad;
		float iT = ((float) DateTime.Now.Millisecond)/1000f;
		float tmpPassed=0f;

		tmpPassed= time-lastEachsecond;
		if( tmpPassed>=eachsecond)
		{
			lastEachsecond=time;
			Warn(listEachSecond,time);
		}

		
		tmpPassed= time-lastSometime;
		if( tmpPassed>sometime)
		{
			lastSometime=time;
		//	print ("Tic, tac   "+ time);
			Warn(listSometime,time);
		}

		
		tmpPassed= time-lastOften;
		if( tmpPassed>often)
		{
			lastOften=time;
			Warn(listOften,time);
		}

		
		tmpPassed= time-lastQuick;
		if( tmpPassed>quick)
		{
			lastQuick=time;
			Warn(listQuick,time);
		}
		
		tmpPassed= time-lastUpdate;
		if( tmpPassed>update)
		{
			lastUpdate=time;
			Warn(listUpdate,time);
		}
		

		//if(UnityEngine.Random.Range(0,9000)==1)	System.Threading.Thread.Sleep(10000);
		float eT = ((float) DateTime.Now.Millisecond)/1000f;
		executionAverageTime= (executionAverageTime+ (eT-iT))/2f;
		timeBetweenUpdate= (timeBetweenUpdate+ Time.deltaTime)/2f;
		//Debug.Log(eT+"-"+iT + " == "+executionAverageTime);
	}


	public void Add(I_Refreshable listener, E_RefreshType type)
	{

		switch(type)
		{
			
		case E_RefreshType.UpdateLike: listUpdate.AddFirst(listener); break;
		case E_RefreshType.Quick: listQuick.AddFirst(listener); break;
		case E_RefreshType.Often: listOften.AddFirst(listener); break;
		case E_RefreshType.EachSecond: listEachSecond.AddFirst(listener); break;
		case E_RefreshType.Sometime: listSometime.AddFirst(listener); break;
		}
	}



	void  Warn(LinkedList<I_Refreshable> toRefresh, float time)
	{
		foreach(I_Refreshable r  in toRefresh)
			r.Refresh(time);
	}

	public int GetLenght(E_RefreshType type)
	
	{
		
		switch(type)
		{
		case E_RefreshType.UpdateLike: return listUpdate.Count; 
		case E_RefreshType.Quick: return listQuick.Count; 
		case E_RefreshType.Often: return  listOften.Count; 
		case E_RefreshType.EachSecond: return  listEachSecond.Count; 
		case E_RefreshType.Sometime: return  listSometime.Count; 
		}
		return 0;

	}


	public enum E_RefreshType{ Sometime, EachSecond, Often, Quick,UpdateLike, NotNow}
	

}




public interface I_Refreshable 
{
	
	void Refresh( float gameTime);
	Refresher.E_RefreshType GetRefreshType();
}
