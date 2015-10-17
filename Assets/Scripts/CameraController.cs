using UnityEngine;
using System.Collections;
using DG.Tweening ; 
public class CameraController : MonoBehaviour {
//	public float a = 1.0f;
//	float PlanetRotateSpeed  = -25.0f;



	public bool spoting = false ; 
	bool _spoting = false ; 


	[Header("Setting Objects")]
	public GameObject cam ; 
	public Transform target ;
	public Transform terrain ; 
	public Transform cam_target ; //= GameObject.Find ("bb").transform ;
	public Transform cam_lookat ; 



	[Header("Camera Settings")]
	public float OrbitSpeed  = 10.0f;
	public float chagneDuration  ; 
//	public Vector3 offsetVector = new Vector3(0,0,0) ;
	

	[Header("Local Mode")]

	public int spotFieldOfView = 20 ; 
	public Vector3 spotLookatOffset = new Vector3(2.64f,0.72f,0f)  ;
	public Vector3 spotCameraHeight  ; 

	[Header("Global Mode")] 
	
	public int globalFieldOfView = 50 ; 
	public Vector3 globalLookatOffset = new Vector3(0.0f,0.32f,0f) ;
	public Vector3 globalCameraHeight ; 
	 
	[Header("Debug Infomation")] 

	public int currentFieldOfView ; 
	public Vector3 currentLookatOffset ;
	public Vector3 currentCamHeight ; 
	// Use this for initialization




	public bool targetChanged = false ; 

	void Start () {

		currentFieldOfView = globalFieldOfView ; 
		currentLookatOffset = globalLookatOffset ; 
		_spoting = spoting; 
	
	}





	// Update is called once per frame
	void Update () {

		if ( (spoting != _spoting) || targetChanged ){
			_spoting = spoting ; 
			targetChanged = false ; 
			if (spoting){
				currentFieldOfView = spotFieldOfView;
				currentLookatOffset = spotLookatOffset ; 
				currentCamHeight = spotCameraHeight ; 
				Debug.Log ("fuck" + spotLookatOffset) ; 
			}else {
				currentFieldOfView = globalFieldOfView;
				currentLookatOffset = globalLookatOffset ; 
				currentCamHeight = globalCameraHeight ; 
			}
			cam.GetComponent <Camera> ().DOFieldOfView (currentFieldOfView, chagneDuration);
			cam_lookat.DOLocalMove (cam_lookat.localRotation * currentLookatOffset , chagneDuration);

			cam.transform.DOLocalMove (currentCamHeight , chagneDuration );
//			cam_lookat.transform.localPosition = (cam_lookat.rotation * currentLookatOffset);
			if (spoting){
				cam_target.DOMove (target.position, chagneDuration);
			}else {
				cam_target.DOMove (terrain.position,chagneDuration);
			}
		}
//		Debug.Log (cam_lookat.localPosition) ;

		cam.transform.LookAt (cam_lookat.position ); 
		cam_lookat.RotateAround (cam_target.position, Vector3.up, OrbitSpeed * Time.deltaTime);

	}

}
