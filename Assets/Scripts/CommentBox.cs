using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


using System;
using System.Text;
public class CommentBox : MonoBehaviour {

	// Use this for initialization
	public Main mainController  ; 
	public GameObject commentInputContainer  ; 
	public InputField commentInput  ; 
	public bool isLike ; 

	bool waitingDB = false  ;

	void Start () {

	}

	public void commentCallback (){
		commentInput.text = "" ;
		mainController.status = "commentDone";
	}

	// Update is called once per frame
	void Update () {
		if (mainController.status == "comment"){

			if( !waitingDB && (Input.GetKey(KeyCode.LeftControl) ||Input.GetKey(KeyCode.RightControl) ) && Input.GetKeyUp(KeyCode.Return) ) {
				string comment = commentInput.text;
				DB.ceateComment(mainController.currentBuilding.name , comment , isLike ) ; 

				string type = isLike ? "l" : "d" ; 
				mainController.currentBuildingData.commentList.Insert(0,( new string[]{type,comment})) ; 
				commentInputContainer.SetActive(false) ;
				mainController.sendingHint.SetActive(true);
				waitingDB = true ; 
			}

			if (waitingDB && DB.commentDone){
				waitingDB = false ; 
				DB.commentDone = false ; 
				commentCallback () ;
			}
		}
	}

	void OnGUI (){

		if (Event.current.type == EventType.KeyUp && Event.current.keyCode != KeyCode.Return) {

			Debug.LogWarning(commentInput.text);
			commentInput.text = checkCommentLength(commentInput.text , 70 ) ;

		}
	}

	private  string checkCommentLength(string str, int byteNum)
	{
		for (int i = 0 ; i < str.Length; i++)
		{
			Debug.LogWarning ("strResult : " + i); 
			if (Encoding.Default.GetByteCount(str.Substring(0,i+1)) >= byteNum){
				#if UNITY_EDITOR
				EditorUtility.DisplayDialog( "共構小叮嚀", "很抱歉！我們的評論不能打太長~", "好的");
				#endif
				return str.Substring(0,i);
			} 
		}
		return str;
	}



}
