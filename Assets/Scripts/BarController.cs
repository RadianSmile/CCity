using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarController : MonoBehaviour {
	[Header("Variables")] 
	public float likePercentage ;
	public bool like ; 

	[Header("Setting")] 
	public GameObject prefab ;
	public int maxY ;
	public int minY ; 
	public int num ;
	public float delay ;
	public Color likeColor   ; 
	public Color dislikeColor ; 
	public Transform thisTransform ; 

	
	float[] yArr ;
	int checkI = 0;

	void Awake (){
		yArr = new float[num] ;
		float interval = ((float)(maxY - minY)) / (num-1) ; 
		Debug.Log (interval);
		
		for (int i = 0 ; i < num ; i++){
			yArr[i] = minY + i * interval ; 
			if (i == num -1 ){
				Debug.Log (yArr[i] + " " + i * interval ) ;
			}
		}
	}

	void Start(){

	}

	IEnumerator createBar(float y , Color color , float delaySeconds , int check){
		yield return new WaitForSeconds (delaySeconds) ;
		if (check == checkI){ // means that the last operation wasn't interupted.
			GameObject l = Instantiate<GameObject>(prefab) ;

			RectTransform r = l.GetComponent<RectTransform>();
			r.SetParent( thisTransform);
			r.localScale = new Vector3(1f,1f,1f);
			r.anchoredPosition = new Vector2 (0f,y);
			l.GetComponent<Image>().color = color;
		}
	}
	public void update ( BuildingData currentBuildingData){
		int l = currentBuildingData.likeNum ; 
		int d = currentBuildingData.dislikeNum ; 

		this.gameObject.SetActive(false);
		if (l == 0 &&  d == 0 ){
			like = true ; 
			likePercentage = 0.5f ;
		}else{
			like = l > d ;
			likePercentage = (like ? l : d ) / (float)(l+d) ; 
		}
		this.gameObject.SetActive(true);
	}

	public void update (  int l , int d ){
		this.gameObject.SetActive(false);
		if (l == 0 &&  d == 0 ){
			like = true ; 
			likePercentage = 0.5f ;
		}else{
			like = l > d ;
			likePercentage = (like ? l : d ) / (float)(l+d) ; 
		}

		this.gameObject.SetActive(true);
	}
	
	void OnEnable(){
		if (prefab == null) return;
		float breakPoint = likePercentage * num ; 
		Debug.Log (breakPoint);
		checkI++ ; 
		for (int i = 0 ; i < num ; i++){

			if (like){
				if (i < breakPoint){
					StartCoroutine(createBar(yArr[i] ,likeColor, delay*i , checkI));
				}else{
					StartCoroutine(createBar(yArr[i] ,dislikeColor, delay*i,checkI));
				}
			}else{
				if (i < num - breakPoint){
					StartCoroutine(createBar(yArr[i] ,likeColor, delay*i,checkI));
				}else{
					StartCoroutine(createBar(yArr[i] ,dislikeColor, delay*i,checkI));
				}
			}
		}
	}

	void OnDisable(){
		checkI++;
		foreach(RectTransform c in thisTransform){
			Destroy(c.gameObject);
		}
	}

}
