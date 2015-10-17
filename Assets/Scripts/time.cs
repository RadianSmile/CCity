using UnityEngine;
using System.Collections;
using UnityEngine.UI ; 
using System ; 

public class time : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Text> ().text = String.Format("{0:HH:mm:ss}", DateTime.Now);; 
	}
}
