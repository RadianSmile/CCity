using UnityEngine;
using UnityEngine.UI;
using Parse;
using System ; 
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class DB  {


	static public string tableSelectBid ;
	static public string tableStatus ;
	static public DateTime tableLastStatusTime ; 


	static public bool commentDone = false ; 
	static public ParseObject ProjectStatus = ParseObject.CreateWithoutData("Status","4uTIsQ0p0B") ;
	static public void init (){
//		ParseQuery<ParseObject> query = ParseObject.GetQuery("Status");
//		query.GetAsync("4uTIsQ0p0B").ContinueWith(t => {
//			ProjectStatus = t.Result;
//		});
	}
	

	static public void ceateComment (string bid , string comment , bool isLike ){
		int bidNum = int.Parse (bid); 
		Debug.Log ("submit comment ...") ;
		ParseObject _comment = new ParseObject("Comment");
		_comment["comment"] = comment ;
		_comment["bid"] = bidNum;
		_comment["type"] = isLike ? "l" : "d" ; 
		_comment.SaveAsync().ContinueWith(t => {
			Debug.Log ("createComment Success :" + comment );
			commentDone = true ;
		});
	}

	static public void updateStatus ( string status , int bid = 0 ){
		if (bid != 0 ){
			ProjectStatus ["bid"] = bid; 
		}
		ProjectStatus ["behavior"] = status; 
		ProjectStatus.SaveAsync().ContinueWith(t => {
			Debug.Log ("Status upload successfully : " + status);
		});
	}

	public static IEnumerator thumbBuilding ( string bid , bool like ,Action callback){

		int bidNum = int.Parse (bid); 
		ParseQuery<ParseObject> query = 
			ParseObject.GetQuery ("Building")
				.WhereEqualTo ("bid", bidNum); 

		Task<Parse.ParseObject> queryBuildingTask = query.FirstAsync ();
		while (!queryBuildingTask.IsCompleted) yield return null; 
		ParseObject b = queryBuildingTask.Result; 

		string behavior = (like) ? "like" : "dislike"; 
		b.Increment (behavior);	
		updateStatus(behavior, bidNum);

		Task saveTask = b.SaveAsync (); 
		while (!saveTask.IsCompleted) yield return null; 
		callback();
		Debug.Log ("Building thumb saved.");
	}
	public static IEnumerator updateComments (){
		ParseQuery<ParseObject> commentQuery = 
			ParseObject.GetQuery ("Comment");
//				.OrderByDescending("createdAt")
//				.Limit(1000);

		Task<IEnumerable<Parse.ParseObject>> commentQueryTask = commentQuery.FindAsync();
		while (!commentQueryTask.IsCompleted) yield return null;   

		foreach (ParseObject comment in commentQueryTask.Result){
			string bid = comment.Get<int>("bid").ToString() ; 

			// yet
		}

	}



	static public IEnumerator syncTableStatus(){
		ParseQuery<ParseObject> query = ParseObject.GetQuery("Status");
		query = query.WhereEqualTo("device","T");
		Task<Parse.ParseObject>tableStatusTask = query.FirstAsync ();
		while (!tableStatusTask.IsCompleted) {yield return null;}
//		yield return new WaitForSeconds (.5f) ;

		if (tableStatusTask.IsFaulted){
			foreach( ParseException e in tableStatusTask.Exception.InnerExceptions) {
				Debug.LogWarning("syncTableStatus : " + e.Message);
			}
			return false ; 
		}



		if (tableStatusTask.Result.UpdatedAt.HasValue && tableStatusTask.Result.UpdatedAt.Value.CompareTo(tableLastStatusTime) < 0 ){
			Debug.Log ("Table status : this is privious status. Nothing done." + tableStatusTask.Result.UpdatedAt.Value.ToString());

		}else {
			ParseObject T = tableStatusTask.Result ;
			T.TryGetValue<string> ("behavior", out tableStatus);
			int temp; 
			T.TryGetValue<int> ("bid",out temp); 
			tableSelectBid = temp.ToString ();
			Debug.Log ("Table status : " + tableStatus + " " + tableSelectBid);
		}
	}


	public static IEnumerator getAllBuildingInfo(GameObject obj , Action<IEnumerable<ParseObject>,IEnumerable<ParseObject>> callback  ){
		ParseQuery<ParseObject> query = ParseObject.GetQuery("Building");
		query = query.Limit (100);
		Task<System.Collections.Generic.IEnumerable<Parse.ParseObject>>getAllBuildingTask = query.FindAsync ();
		while (!getAllBuildingTask.IsCompleted) yield return null;


		ParseQuery<ParseObject> commentQuery = ParseObject.GetQuery("Comment");
		commentQuery = commentQuery
			.Limit (1000)
			.OrderBy("bid")
			.ThenByDescending("createdAt");

		Task<System.Collections.Generic.IEnumerable<Parse.ParseObject>>getAllCommentTask = commentQuery.FindAsync ();
		while (!getAllCommentTask.IsCompleted) yield return null;


		if (getAllBuildingTask.IsFaulted){
			foreach( ParseException e in getAllBuildingTask.Exception.InnerExceptions) {
				Debug.LogWarning("getAllBuildingInfo Error : " + e.Message);
			}
		}else if (getAllCommentTask.IsFaulted){
			foreach( ParseException e in getAllCommentTask.Exception.InnerExceptions) {
				Debug.LogWarning("getAllCommentTask Error : " + e.Message);
			}
		}else{
			callback (getAllBuildingTask.Result , getAllCommentTask.Result ); 
			Debug.Log ("getAllBuildingInfo and comment done");
		}
	}


//	static public void ceateParseObject (){
//		ParseObject gameScore = new ParseObject("GameScore");
//		gameScore["score"] = 1337;
//		gameScore["playerName"] = "Sean Plott";
//		gameScore.SaveAsync().ContinueWith(t => {
//			Debug.Log (t);
//		});
//	}
	static public void queryParseObject(){
		ParseQuery<ParseObject> query = ParseObject.GetQuery("GameScore");
		query.GetAsync("29mbIDFPoC").ContinueWith(t => {
			ParseObject gameScore = t.Result;
			int score = gameScore.Get<int>("score");
			Debug.Log(score);
		});
	}
}
