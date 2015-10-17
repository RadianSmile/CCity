//using UnityEngine;
//using System.Collections;
//
//public class onclick : MonoBehaviour {
//
//	// Use this for initialization
//
//
//
//	public main Main ; 
//	public GameObject terrain ; 
//	public cameraRotate crs ;
//
//	void Start () {
//
//
//		GameObject buildings = GameObject.Find ("buildings"); 
//		Component[] renderers = buildings.GetComponentsInChildren(typeof(Renderer));
//		foreach(Renderer childRenderer in renderers)
//		{
//			//childRenderer.material.color = Color.green;
//			if (childRenderer.gameObject == terrain ) continue ;
//
//
////			childRenderer.material.SetColor ("_Color", new Color (0.1f, 0f, 0f));
//
//			childRenderer.material.EnableKeyword ("_EMISSION"); 
//			childRenderer.material.SetColor ("_EmissionColor", new Color (0.4f, 0f, 0f));
//
//		}
////
////		GameObject b1 = GameObject.Find ("b1"); 
////		b1.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION"); 
////		b1.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color (0f, .1f, 0f));
//
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//
//
//		if (crs.spoting) {
//
//
//		}else{
//
//
//		}
//
//	}
//}
