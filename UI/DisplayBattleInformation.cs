using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBattleInformation : MonoBehaviour {

	public static DisplayBattleInformation main;

	public Battle currentBattle;
	public GameObject enemyInfoPanel, battleLogPanel, activeTrapsPanel;
	private GameObject enemyName, enemyHealth;
	public GameObject logEntryPrefab, activeTrapPrefab, enemyHealthPrefab;
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
		enemyHealth = enemyInfoPanel.transform.Find("EnemyHealthDisplay").gameObject;
	}

	
	public void updateAllBattleDisplays(){
		updateEnemyInfoDisplay();
		updateActiveTrapsDisplay();
	}

	public void addBattleLog(string logText){
		GameObject newLog;
		if(battleLogPanel.transform.childCount < logMax){
			newLog = Instantiate(logEntryPrefab, this.transform.position, this.transform.rotation, battleLogPanel.transform);
			newLog.transform.SetAsFirstSibling();
		} 
		else
		{
			Debug.Log("should only be 4 logs");
			foreach(Transform child in battleLogPanel.transform){
				if(child.GetSiblingIndex() > logMax) Destroy(child.gameObject);
			}
			newLog = Instantiate(logEntryPrefab, this.transform.position, this.transform.rotation, battleLogPanel.transform);
			newLog.transform.SetAsFirstSibling();
		}
		newLog.GetComponent<Text>().text = logText;
	}

	public void updateEnemyInfoDisplay(){
		enemyName.GetComponent<Text>().text = currentBattle.enemyName;
		foreach(Transform child in enemyHealth.transform) Destroy(child.gameObject);
		for(int i = 0; i < BattleHandler.main.enemyHealth; i++){
			Instantiate(enemyHealthPrefab, this.transform.position, this.transform.rotation, enemyHealth.transform);
		}
	}

	public void updateActiveTrapsDisplay(){
		foreach(Transform child in activeTrapsPanel.transform) Destroy(child.gameObject);
		foreach(Trap trap in GameInformation.main.builtTraps){
			GameObject setTrap = Instantiate(activeTrapPrefab, this.transform.position, this.transform.rotation, activeTrapsPanel.transform);
			TrapDataHolder data = setTrap.GetComponent<TrapDataHolder>();
			data.trapName = trap.trapName;
			data.reuseable = trap.reuseable;
			setTrap.GetComponent<Image>().sprite = trap.icon;
			setTrap.transform.GetChild(0).GetComponent<Text>().text = trap.trapName;
		}
	}

}
