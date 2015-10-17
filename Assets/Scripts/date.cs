using UnityEngine;
using System.Collections;
using UnityEngine.UI ; 
using System ; 


public class date : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Text> ().text = String.Format("{0:yyyy/MM/dd}", DateTime.Now);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
