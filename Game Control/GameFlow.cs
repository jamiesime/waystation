using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

	private System.Random randomNumber = new System.Random();

	// Use this for initialization
	void Start () {
		advanceDay(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void advanceDay(int amount){
		Debug.Log("advancing day to " + (GameInformation.main.currentDay + amount));
		GameInformation.main.currentLocation = (GameLocation) Resources.Load("Prefabs/GameLocations/Waystation");
		GameInformation.main.currentDay += amount;
		GameObject changeDayPanel = (GameObject) Resources.Load("Prefabs/Main UI/Panels/AdvanceDayPanel");
		Instantiate(changeDayPanel, DisplayGameInformation.main.transform.position, DisplayGameInformation.main.transform.rotation, DisplayGameInformation.main.transform);
		GameInformation.main.gamePhase = calcGamePhase();
		GameInformation.main.todaysEvents.Clear();
		GameInformation.main.restoreEnergy();
		DisplayGameInformation.main.updateAllDisplays();
	
		if(!isBattleDay()){
			getDayEvents();
			displayEvents();
		} else {
			enterBattlePhase();
		}

	}

	public void getDayEvents(){
			if(GameInformation.main.queuedEvents.Count > 0){
			for (int i = 0; i < GameInformation.main.queuedEvents.Count; i++){
				GameEvent curEvn = GameInformation.main.queuedEvents[i];
				if(GameEventHandler.main.eventIsToday(curEvn)){
					GameInformation.main.todaysEvents.Add(curEvn);
				}
			}
			foreach(GameEvent tEvn in GameInformation.main.todaysEvents){
				if(GameInformation.main.queuedEvents.Contains(tEvn)) GameInformation.main.queuedEvents.Remove(tEvn);
			}

		}

		if(GameInformation.main.randomEvents.Count > 0){
			foreach(GameEvent evn in GameInformation.main.randomEvents){
				if(GameEventHandler.main.isRandomEventValid(evn)){
					int result = randomNumber.Next(0, 100);
					// check chance and also if there are less than 2 events today otherwise, don't add
					if(result <= evn.chance && GameInformation.main.todaysEvents.Count < 2) GameInformation.main.todaysEvents.Add(evn);
				}
			}

			foreach(GameEvent tEvn in GameInformation.main.todaysEvents){
				if(GameInformation.main.todaysEvents.Contains(tEvn)) GameInformation.main.randomEvents.Remove(tEvn);
			}
		}
			
	}

	public bool isBattleDay(){
		foreach(Battle btl in GameInformation.main.battles){
			if(btl.onDay == GameInformation.main.currentDay) return true;
		}
		return false;
	}
	
	public void enterBattlePhase(){
		Battle battle = GameInformation.main.battles.Find(x => x.onDay == GameInformation.main.currentDay);
		BattleHandler.main.startNewBattle(battle);
	}

	public GamePhase calcGamePhase(){
		if(GameInformation.main.currentDay < GameInformation.main.finalDay / 3){
			return GamePhase.Early;
		}
		if(GameInformation.main.currentDay > GameInformation.main.finalDay / 3 && GameInformation.main.currentDay < (GameInformation.main.finalDay / 3) * 2 ) {
			return GamePhase.Mid;
		}
		if(GameInformation.main.currentDay > (GameInformation.main.finalDay / 3) * 2){
			return GamePhase.Late;
		}
		return GamePhase.Mid;
		
	}

	public void displayEvents(){
		if(GameInformation.main.todaysEvents.Count > 0){
			GameEventHandler.main.displayNextEvent();
		}
	}

	public void endDay(){

	}
}
