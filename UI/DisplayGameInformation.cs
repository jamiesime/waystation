using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameInformation : MonoBehaviour {

	public static DisplayGameInformation main;

	// statDisplays
	public GameObject statPrefab;
	public GameObject resPrefab;
	public GameObject energySegPrefab;
	public GameObject locationBtnPrefab, locActionBtnPrefab;
	private GameObject statPanel, resPanel, dayPanel, energyDisplayPanel, locButtonPanel, locPanel, locActionPanel, bgPanel, trapPanel, trapMenu;

	void Awake(){
		if(main == null){
			main = this;
		}
	}

	// Use this for initialization
	void Start () {
		statPanel = GameObject.Find("StatPanel");
		resPanel = GameObject.Find("ResourcePanel");
		dayPanel = GameObject.Find("DayPanel");
		energyDisplayPanel = GameObject.Find("EnergyDisplay");
		bgPanel = GameObject.Find("BackgroundPanel");
		locPanel = GameObject.Find("LocationPanel");
		locButtonPanel = GameObject.Find("LocationButtonPanel");
		locActionPanel = GameObject.Find("LocationActionPanel");
		trapPanel = GameObject.Find("ActiveTrapsPanel");
		trapMenu = GameObject.Find("TrapPanel");
		updateResAndStatDisplay();
		updateEnergyDisplay();
		updateDayDisplay();
	}

	public void updateLocationDisplay(){
		GameObject.Find("CurrentLocationPanel").transform.Find("Text").GetComponent<Text>().text = GameInformation.main.currentLocation.place.ToString();
		bgPanel.GetComponent<Image>().sprite = GameInformation.main.currentLocation.backgroundImg;

		foreach(Transform child in locActionPanel.transform){
			Destroy(child.gameObject);
		}

		foreach(GameEvent gEvent in GameInformation.main.currentLocation.eventActions){
			GameObject eventBtn = Instantiate(locActionBtnPrefab, this.transform.position, this.transform.rotation, locActionPanel.transform);
			eventBtn.GetComponent<LocationActionDataHolder>().gameEvent = gEvent;
			eventBtn.transform.Find("Text").GetComponent<Text>().text = gEvent.name;
		}

		foreach(UniqueEvent uEvent in GameInformation.main.currentLocation.uniqueActions){
			GameObject eventBtn = Instantiate(locActionBtnPrefab, this.transform.position, this.transform.rotation, locActionPanel.transform);
			eventBtn.GetComponent<LocationActionDataHolder>().uniqueEvent = uEvent;
			eventBtn.transform.Find("Text").GetComponent<Text>().text = uEvent.name;
		}	

	}

	public void updateAvailableLocations(){
		foreach(Transform child in locButtonPanel.transform){
			Destroy(child.gameObject);
		}

		foreach(GameLocation loc in GameInformation.main.availableLocations){
			GameObject newLoc = Instantiate(locationBtnPrefab, this.transform.position, this.transform.rotation, locButtonPanel.transform);
			newLoc.GetComponent<LocationDataHolder>().location = loc;
			newLoc.GetComponent<LocationDataHolder>().placeName = loc.place.ToString();
			newLoc.GetComponent<LocationDataHolder>().bgImage = loc.backgroundImg;
		}
	}

	public void updateAllDisplays(){
		updateDayDisplay();
		updateEnergyDisplay();
		updateResAndStatDisplay();
		updateLocationDisplay();
		updateAvailableLocations();
		updateTrapDisplay();
		setTrapPanelActive();
	}

	public void updateDayDisplay(){
		dayPanel.transform.Find("DayText").GetComponent<Text>().text = "DAY " + GameInformation.main.currentDay;
	}

	public void updateEnergyDisplay(){
		foreach(Transform child in energyDisplayPanel.transform){
			Destroy(child.gameObject);
		}
		for(int i = 0; i < GameInformation.main.currentEnergy; i++){
			Instantiate(energySegPrefab, this.transform.position, this.transform.rotation, energyDisplayPanel.transform);
		}
	}

	public void updateTrapDisplay(){
		int i = 0;
		foreach(Transform child in trapPanel.transform){
					try {
						Trap trap = GameInformation.main.builtTraps[i];
						GameObject slot = child.gameObject;
						if(trap.icon) slot.GetComponent<Image>().sprite = trap.icon;
						slot.transform.GetChild(0).GetComponent<Text>().text = trap.trapName;
						i++;
					} catch (System.ArgumentOutOfRangeException) {
						GameObject slot = child.gameObject;
						slot.GetComponent<Image>().sprite = null;
						slot.transform.GetChild(0).GetComponent<Text>().text = "EMPTY";
						i++;
						continue;
					}
		}
	}

	public void setTrapPanelActive(){
		GameLocation ws = (GameLocation) Resources.Load("Prefabs/GameLocations/Waystation");
		if(GameInformation.main.currentLocation == ws){
			trapMenu.SetActive(true);
		} else {
			trapMenu.SetActive(false);
		}
	}


	public void updateResAndStatDisplay(){
		// these loops get all stats + resources entered in inspector and populate UI panels with children displaying their names and current values

		foreach(Transform child in statPanel.transform) Destroy(child.gameObject);
		foreach(Transform child in resPanel.transform) Destroy(child.gameObject);

		foreach(PlayerStats stat in GameInformation.main.playerStats){
			GameObject statObj = Instantiate(statPrefab, this.transform.position, this.transform.rotation, statPanel.transform);
			statObj.transform.Find("StatNameText").GetComponent<Text>().text = stat.name.ToString();
			statObj.transform.Find("StatValueText").GetComponent<Text>().text = stat.currentValue.ToString();
		}

		foreach(PlayerResources res in GameInformation.main.playerResources){
			GameObject resObj = Instantiate(resPrefab, this.transform.position, this.transform.rotation, resPanel.transform);
			resObj.transform.Find("ResNameText").GetComponent<Text>().text = res.name.ToString();
			resObj.transform.Find("ResValueText").GetComponent<Text>().text = res.currentValue.ToString();
		}
	}

}
