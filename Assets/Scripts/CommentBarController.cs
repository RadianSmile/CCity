using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommentBarController : MonoBehaviour {

	[Header("Variables")] 
	public int  commentNum ;
	public int  commentMax ;
	
	[Header("Setting")] 
	public GameObject prefab ;
	public int maxY ;
	public int minY ; 
	public int num ;
	public float delay ;
	
	public Transform thisTransform ; 
	


	
	float[] yArr ;
	float interval ; 

	int checkI = 0;
	
	void Start(){
		yArr = new float[num] ;
		float interval = ((float)(maxY - minY)) / (num-1) ; 
		for (int i = 0 ; i < num ; i++){
			yArr[i] = minY + i * interval ; 
		}
		
	}
	
	IEnumerator createBar(float y , float delaySeconds , int check ){
		yield return new WaitForSeconds (delaySeconds) ;
		if (check == checkI){ // means that the last operation wasn't interupted.
			GameObject l = Instantiate<GameObject>(prefab) ;
			
			RectTransform r = l.GetComponent<RectTransform>();
			r.SetParent( thisTransform);
			r.localScale = new Vector3(1f,1f,1f);
			r.anchoredPosition = new Vector2 (0f,y);
		}
		
	}
	
	
	
	void OnEnable(){

		if (prefab == null || yArr == null ) return;

		float breakPoint = num * commentNum/commentMax;
		Debug.Log (breakPoint);

		checkI++ ; 
		for (int i = 0 ; i < num ; i++){
			if (i < breakPoint){
				StartCoroutine(createBar(yArr[i] ,  delay*i , checkI ));
			}
		}

				
		
	}
	
	void OnDisable(){
		checkI++ ; 
		foreach(RectTransform c in thisTransform ){
			Destroy(c.gameObject);
		}
		
		
	}
	
}
