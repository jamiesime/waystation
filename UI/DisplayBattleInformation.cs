using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBattleInformation : MonoBehaviour {

	public static DisplayBattleInformation main;

	public Battle currentBattle;
	public GameObject enemyInfoPanel, battleLogPanel, activeTrapsPanel;
	private GameObject enemyName, enemyHealth;
	public GameObject logEntryPrefab;
	public int logMax;

	// Use this for initialization
	void Awake () {
		if(main == null){
			main = this;
		} else {
			Destroy(this);
		}
	}

	void Start(){
		enemyName = enemyInfoPanel.transform.Find("EnemyName").gameObject;
	}

	
	public void updateAllBattleDisplays(){
		updateEnemyInfoDisplay();
		updateActiveTrapsDisplay();
	}

	public void addBattleLog(string logText){
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

	public void updateEnemyInfoDisplay(){
		enemyName.GetComponent<Text>().text = currentBattle.enemyName;
	}

	public void updateActiveTrapsDisplay(){

	}

}
