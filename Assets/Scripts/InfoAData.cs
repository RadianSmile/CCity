using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI ; 
using DG.Tweening;

public class InfoAData : MonoBehaviour {

	static Color dislikeColor = Lib.RColor(237,28,36,255) ; 
	static Color likeColor = Lib.RColor(0,113,188,255) ;

	public GameObject box2 ; 
	public GameObject noCommentHint ; 
	public BuildingData currentBuildingData ; 

	public float revewChangeTime ;
	public string bname ;
	public string description ;
	public string feature1 ;
	public string feature2 ;


	public Text NameField ; 
	public Text descriptionField ; 
	public Text feature1Field ; 
	public Text feature2Field ; 



	private int commentIndex = 0  ; 
	private int commentCount ; 
	private int commentRowNum = 0 ;



	public List<ReviewData> reviews ;
//	public Dictionary<Tuple<int,int>, GameObject> thumbs ; 
	

//	public Text review0 ; 
//	public Text review1 ; 
//	public Text review2 ; 
	void SetUp(){
		commentRowNum = reviews.Count ;
	}

	// Use this for initialization
	void OnEnable () {

		NameField.text = currentBuildingData.bname ; 
		descriptionField.text = currentBuildingData.description ;
		feature1Field.text = currentBuildingData.feature ;
		feature2Field.text = currentBuildingData.feature1 ;

		commentIndex = 0 ;
		StartCoroutine(showReviewRecursively());
	}

	void showToReviewRow (){
//		int iterator = 0 ; 
		foreach (ReviewData review in reviews ){

			string[] commentArr ; 
			if (commentIndex < currentBuildingData.commentList.Count){
				commentArr = currentBuildingData.commentList[commentIndex++] ;
			}else{
				commentArr = null ;
			}


			if (commentArr != null){
				review.txt.color = getColor(commentArr) ;
				if (commentArr[0] == "l") {
					review.likeImg.enabled = true  ;
					review.dislikeImg.enabled = false  ;
				}else{
					review.likeImg.enabled = false  ;
					review.dislikeImg.enabled = true  ;
				}
				review.txt.text = commentArr[1] ;

			}else{
				Debug.Log("commentArr is null") ;
				
				review.likeImg.enabled = false  ;
				review.dislikeImg.enabled = false  ;
				review.txt.text = "" ;
			}
		}
	}

	IEnumerator showReviewRecursively (){
		if (currentBuildingData.commentList.Count < commentRowNum){
			showToReviewRow(); 
			return false ; 
		}else{
			while (true){
				showToReviewRow() ;
				if (commentIndex >= currentBuildingData.commentList.Count ) commentIndex = 0 ;
				yield return (new WaitForSeconds(revewChangeTime));
			}
		}
	} 

	Color getColor (string[] commentArr){
		if( commentArr[0] == "l" ){
			return likeColor ; 
		}
			return dislikeColor ; 
	}
}
