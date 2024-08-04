using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class YieldTest : MonoBehaviour {
	private int i = 0;
	public  int maxCount =20;
	// Use this for initialization
	void Start () {
	
		IEnumerable p = ObtenirListeDePrenoms ();
		foreach (string prenom in p )
		{
			print(prenom );
		}

		IEnumerable test = Compteur ();
		foreach (int i in test) {
						print ("" + i + "  " + test.GetEnumerator ().Current);
			if(i==10) break;		
		}
		
		foreach (int i in test)
			print (""+i+"  "+test.GetEnumerator().Current);
		
		foreach (int i in test)
			print (""+i+"  "+test.GetEnumerator().Current);

	


	}
	
	public  IEnumerable<string> ObtenirListeDePrenoms()
	{
		yield return "Nicolas";
		yield return "Jérémie";
		yield return "Delphine";
	}

	public  IEnumerable<int> Compteur()
	{
		int i = 0;
		print ("Start Count");
		while (i<maxCount) {
		
			yield return i++;

		}
		if (i == maxCount) {
						i++;
						yield break;
				}
		yield return 100;
		yield return 102;
		yield return 104;
	}


}
