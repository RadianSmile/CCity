using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Spot : MonoBehaviour {

	public float tweenDuration ;

	[Range (0f,8f)] 
	public float activeIndensity ;
	[Range (0f,8f)] 
	public float inactiveIndensity ;

	Light thisLight ;
	// Use this for initialization
	void Start () {
		thisLight = this.gameObject.GetComponent<Light>() ;  
	}

	public void fadeOut () {
		thisLight.DOIntensity (inactiveIndensity, tweenDuration);
	}

	public void fadeIn () {
		thisLight.DOIntensity (activeIndensity, tweenDuration);
	}
}
