using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDisplay : MonoBehaviour {

	public static LogDisplay main;

	public GameObject logEntryPrefab;
	public int logMax;

	// Use this for initialization
	void Start () {
		if(main == null){
			main = this;
		} else {
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addLog(string logText)
	{
		GameObject newLog;
		if(this.transform.childCount < logMax){
			newLog = Instantiate(logEntryPrefab, this.transform.position, this.transform.rotation, this.transform);
			newLog.transform.SetAsFirstSibling();
		} else {
			Destroy(this.transform.GetChild(this.transform.childCount - 1).gameObject);
			newLog = Instantiate(logEntryPrefab, this.transform.position, this.transform.rotation, this.transform);
			newLog.transform.SetAsFirstSibling();
		}
		newLog.GetComponent<Text>().text = logText;
	}

	public void parseBuildTrapLog(string trapName){
		addLog(trapName + " built");
	}

	public void parseChoiceLog(EventChoice choice){
		foreach(ResChange change in choice.resChanges){
			if(change.changeValue != 0){
				if(change.changeValue > 0){
				 addLog(change.resource + " increased by " + change.changeValue + " (" + GameEventHandler.main.currentEvent.header + ")");
				 } else {
					 addLog(change.resource + " depleted by " + change.changeValue + " (" + GameEventHandler.main.currentEvent.header + ")");
				 }
			}
		}

		foreach(StatChange change in choice.statChanges){
			if(change.changeValue != 0){
				if(change.changeValue > 0){
				 addLog("Your " + change.stat + " improved by " + change.changeValue + " (" + GameEventHandler.main.currentEvent.header + ")");
				 } else {
					 addLog("Your " + change.stat + " deteriorated by " + change.changeValue + " (" + GameEventHandler.main.currentEvent.header + ")");
				 }
			}
		}


	}
}
