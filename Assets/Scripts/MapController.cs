using UnityEngine;
using System ; 
using Parse ; 
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;



public class MapController : MonoBehaviour {
	public GameObject obj ;
	public int totalLikeNum  ; 
	public int totalDislikeNum  ; 
//	public int totalLikePercentage ; 
	public int totalIsLike ; 
	public Main main ; 
	public bool isPrepared ; 
	// Use this for initialization
	public IEnumerable<BuildingData> buildings ; 
	void Start () {

		StartCoroutine( DB.getAllBuildingInfo (obj,buildingDataCallback));
		StartCoroutine("thermodynamic");

		buildings = this.gameObject.GetComponentsInChildren<BuildingData>() ;
	}

	// Update is called once per frame

	public void buildingDataCallback ( IEnumerable<ParseObject> allData , IEnumerable<ParseObject> allComment ){
		foreach (ParseObject b in allData) {

			string bid =  b.Get<int>("bid").ToString() ;
			GameObject building = GameObject.Find(bid) ; 
			if (building != null){
				BuildingData bb = building.GetComponent<BuildingData>();

				b.TryGetValue<string>("name" , out bb.bname) ;
				b.TryGetValue<string>("feature",out bb.feature);
				b.TryGetValue<string>("feature1",out bb.feature1);
				b.TryGetValue<string>("description",out bb.description);

			}else {
				Debug.LogWarning ("no such bid : " + bid) ; 
			}
		}
		main.commentBar.gameObject.SetActive(false) ; 
		foreach (ParseObject c in allComment) {
			string bid =  c.Get<int>("bid").ToString() ;
			GameObject building = GameObject.Find(bid) ; 
			if (building != null){
				main.commentBar.commentNum +=  1 ; 
				Debug.LogWarning(main.commentBar.commentNum);

				BuildingData bb = building.GetComponent<BuildingData>();


				string[] commentArr = new string[2];
				c.TryGetValue<string>("type" , out commentArr[0] ); 
				c.TryGetValue<string>("comment" , out commentArr[1] ) ;

				bb.commentList.Add(commentArr) ; 

			}else {
				Debug.LogWarning ("comment couldnt find building : " + bid ) ; 
			}
		}
		main.commentBar.gameObject.SetActive(true ) ; 
		isPrepared = true ;

	}
	public void spoting ( GameObject currentBuilding ){
		foreach( BuildingData b in buildings){
			if ( b.gameObject == currentBuilding ){
				b.active = true ;
			}else {
				b.active = false ;
			}
		}
	}

	public void spoting ( bool isSpoting = false ){
		if (!isSpoting){
			foreach( BuildingData b in buildings)
				b.active = true ;
		}
	}



	
	public IEnumerator thermodynamic()
	{

		totalLikeNum = 0 ; 
		totalDislikeNum = 0 ; 
//		totalLikePercentage = 0 ; 

		WWW www = new WWW("http://coconstructionv2.parseapp.com/thermodynamic");
		yield return www;


		if (www.error != null) {	
			Debug.Log ("WWW Error: " + www.error);
		} else {

			JSONArray jsonArray = JSONArray.Parse (www.text);
			foreach (JSONValue aa in jsonArray) {
				JSONObject statusJson;
				statusJson = JSONObject.Parse (aa.ToString ()); 
				string bid = statusJson.GetNumber ("bid").ToString ();

//				if (bid == "0" ){ // yet  here means the whole map's data
//
//
//				}


				GameObject building = GameObject.Find (bid);
//				Debug.Log (building);
				if (building != null) {

					BuildingData b = building.GetComponent<BuildingData> ();


				
					b.likeStatus = statusJson.GetString ("type");
					b.likeNum = (int)statusJson.GetNumber ("like");


					b.dislikeNum = (int)statusJson.GetNumber ("dislike");
					b.percentage = (float)statusJson.GetNumber ("percent") ;

					totalLikeNum += b.likeNum ; 
					totalDislikeNum  += b.dislikeNum ; 


					b.colorChange = true ;
				} else {
					Debug.LogWarning ("bid " + bid + " not found"); 
				}
			}
			main.changed = true ;
//			totalLikePercentage = (float)totalLikeNum / (totalLikeNum + totalDislikeNum) ;
//			totalIsLike = totalLikeNum >= totalDislikeNum ; 
			// loop end 
		}
	}
}
