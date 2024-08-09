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

using UnityEngine;
using System.Collections;
using System;
public class LifeTime : MonoBehaviour {

	public bool activate=false;
	public float lifeTime= 3f;
	public bool withEvent;
	public bool withDestroyGameObject=true;

	public EventHandler<DeathArgs> onDeath ;

	protected virtual void Update () {
		if (!activate)
						return;
		lifeTime-= Time.deltaTime;
		if(lifeTime<0){
			Kill (Time.timeSinceLevelLoad);
		}
	}

	public void Kill(float time)
	{
		
		if(withEvent&& onDeath!=null)
		{
			DeathArgs da= new DeathArgs(lifeTime, time, this.gameObject);
			onDeath(this, da);
		}
		
		if (withDestroyGameObject)
			Destroy(this.gameObject);
		else 
			Destroy(this);


	}

	public void SetLifeTimeAndPlay (float lifeTime)
	{
		this.lifeTime = lifeTime;
		activate = true;
	}

	public class DeathArgs : EventArgs
	{
		/**How many time was it suppose to live*/
		private float lifeTime;
		/**The time corresponding at when the object is declared death;*/
		private float whenDeath;

		private GameObject what;

		public DeathArgs (float lifeTime, float whenDeath, GameObject what)
		{
			this.lifeTime = lifeTime;
			this.whenDeath = whenDeath;
			this.what = what;
		}

		public float LifeTime {
			get {
				return this.lifeTime;
			}
			set {
				lifeTime = value;
			}
		}

		public float WhenDeath {
			get {
				return this.whenDeath;
			}
			set {
				whenDeath = value;
			}
		}

		public GameObject What {
			get {
				return this.what;
			}
			set {
				what = value;
			}
		}
	}
}
