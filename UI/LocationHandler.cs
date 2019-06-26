using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocationHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeLocation(){
		LocationDataHolder info = EventSystem.current.currentSelectedGameObject.GetComponent<LocationDataHolder>();
		if(GameInformation.main.currentLocation != info.location){
			if(GameInformation.main.currentEnergy > 0){
				GameInformation.main.currentLocation = info.location;
				GameInformation.main.currentEnergy -= 1;
				DisplayGameInformation.main.updateAllDisplays();
				LogDisplay.main.addLog("Moved to " + info.placeName);
			} else {
				LogDisplay.main.addLog("Too tired to move any more today...");
			}
		} 
		else 
		{
		LogDisplay.main.addLog("Already in " + info.placeName);
		}
	}

	public void performLocationAction(){
		LocationActionDataHolder action = EventSystem.current.currentSelectedGameObject.GetComponent<LocationActionDataHolder>();
		if(action.uniqueEvent != null){
			// unique events not implemented yet
		} else {
			if(action.gameEvent){
				GameInformation.main.todaysEvents.Insert(0, action.gameEvent);
				GameEventHandler.main.displayNextEvent();
			}
		}
	}

}
