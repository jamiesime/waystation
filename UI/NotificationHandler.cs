using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHandler : MonoBehaviour {

	public static NotificationHandler main;

	public GameObject header, msgText, okBtn;

	void Awake(){
		if(main == null){
			main = this;
		} else {
			Destroy(this.gameObject);
		}
	}

	void Start(){
		header = transform.Find("HeaderText").gameObject;
		msgText = transform.Find("MessageText").gameObject;
		okBtn = transform.Find("OKBtn").gameObject;
		foreach(Transform child in this.gameObject.transform){
			child.gameObject.SetActive(false);
		}
		this.transform.SetAsLastSibling();
	}
	
	public void closeNotificationWindow(){
		foreach(Transform child in this.gameObject.transform){
			child.gameObject.SetActive(false);
		}
	}

	public void newNotification(string newMsg, string headerText = "INFO"){
		this.transform.SetAsLastSibling();
		msgText.GetComponent<Text>().text = newMsg;
		header.GetComponent<Text>().text = headerText;
    foreach(Transform child in this.gameObject.transform){
			child.gameObject.SetActive(true);
		}
	}
}
