using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldHack : InputField {
	public override void OnSubmit (UnityEngine.EventSystems.BaseEventData eventData)
	{
		base.OnSubmit (eventData);
		Debug.Log ("test2");
		Debug.Log (this.text);
	}

	public void sendOnSubmit(){
		Debug.Log ("test1");
		base.SendOnSubmit ();
	}




}
