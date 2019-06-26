using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameEventHandler : MonoBehaviour {

	public static GameEventHandler main;

	public GameObject eventPanel;
	public GameObject subPanelPrefab;
	public GameObject subPanel;
	public GameObject choicePrefab;

	public GameEvent currentEvent;
	public int currentPage;

	public List<GameEvent> randomEvents;
	public List<GameEvent> storyEvents;

	// Use this for initialization
	void Awake () {
		Object[] rndEvn = Resources.LoadAll("Prefabs/Events/Random/", typeof(GameEvent)); 
		foreach(GameEvent evn in rndEvn){
			randomEvents.Add(evn);
		}
		Object[] storyEvn = Resources.LoadAll("Prefabs/Events/Story/", typeof(GameEvent)); 
		foreach(GameEvent evn in storyEvn){
			storyEvents.Add(evn);
		}
		if(main == null){
			main = this;
		}
		eventPanel.SetActive(false);
	}

	void Start(){
	}
	
	public bool eventIsToday(GameEvent gEvent){
		if(gEvent.occurs == EventOccurrence.SpecificDay){
			return GameInformation.main.currentDay == gEvent.specificDayNumber ? true : false;
		}
		if(gEvent.occurs == EventOccurrence.OnDelay){
			if(gEvent.dayDelayTarget > 0){
				return GameInformation.main.currentDay == gEvent.dayDelayTarget ? true : false;
			} else {
				gEvent.dayDelayTarget =- 0 + gEvent.dayDelayAmount;
				return GameInformation.main.currentDay == gEvent.dayDelayTarget ? true : false;
			}
		}
		return true;
	}

	public bool isRandomEventValid(GameEvent rEvent){
		if(rEvent.occurs == EventOccurrence.Random){
			return GameInformation.main.gamePhase == rEvent.appears ? true : false;
		} else {
			return false;
		}
	}

	public void displayNextEvent(){
		currentEvent = GameInformation.main.todaysEvents[0];
		subPanel = Instantiate(subPanelPrefab, this.transform.position, this.transform.rotation, eventPanel.transform);
		subPanel.transform.Find("EventHeaderText").GetComponent<Text>().text = currentEvent.header;
		subPanel.transform.Find("EventDetailText").GetComponent<Text>().text = currentEvent.pages[0];
		currentPage = 0;
		if(currentEvent.pages.Length < 2){
			subPanel.transform.Find("PagePanel").gameObject.SetActive(false);
			subPanel.transform.Find("EventChoicesPanel").gameObject.SetActive(true);
			displayEventChoices();
		} else {
			GameEventHandler.main.subPanel.transform.Find("PagePanel").Find("PagesText").GetComponent<Text>().text = (GameEventHandler.main.currentPage + 1) + " / " + GameEventHandler.main.currentEvent.pages.Length;
			subPanel.transform.Find("PagePanel").gameObject.SetActive(true);
			subPanel.transform.Find("EventChoicesPanel").gameObject.SetActive(false);
		}
		eventPanel.SetActive(true);
	}

	public void changePage(int dir){

		if(dir == 1 && GameEventHandler.main.currentPage < (GameEventHandler.main.currentEvent.pages.Length - 1)){
			GameEventHandler.main.currentPage++;
			GameEventHandler.main.subPanel.transform.Find("EventDetailText").GetComponent<Text>().text = GameEventHandler.main.currentEvent.pages[GameEventHandler.main.currentPage];
			if(GameEventHandler.main.currentPage == GameEventHandler.main.currentEvent.pages.Length -1){
				GameEventHandler.main.displayEventChoices();
			}
		} 

		if(dir == -1 && GameEventHandler.main.currentPage > 0){
			GameEventHandler.main.currentPage--;
			GameEventHandler.main.subPanel.transform.Find("EventDetailText").GetComponent<Text>().text = GameEventHandler.main.currentEvent.pages[GameEventHandler.main.currentPage];
			GameEventHandler.main.removeEventChoices();
		}
		
		GameEventHandler.main.subPanel.transform.Find("PagePanel").Find("PagesText").GetComponent<Text>().text = (GameEventHandler.main.currentPage + 1) + " / " + GameEventHandler.main.currentEvent.pages.Length;
	}

	public void displayEventChoices(){
		GameObject choicePanel = subPanel.transform.Find("EventChoicesPanel").gameObject;
		choicePanel.SetActive(true);
		foreach(EventChoice choice in currentEvent.choices){
			string prependMsg = "";
			GameObject choiceBtn = Instantiate(choicePrefab, this.transform.position, this.transform.rotation, choicePanel.transform);
			choiceBtn.GetComponent<ChoiceDataHolder>().choiceData = choice;
			if(!requirementsMet(choice)) { choiceBtn.GetComponent<Button>().interactable = false; prependMsg = "[REQUIREMENT NOT MET] "; }
			choiceBtn.transform.GetChild(0).GetComponent<Text>().text = prependMsg + choice.displayText;

		}
	}

	public void removeEventChoices(){
		GameObject choicePanel = subPanel.transform.Find("EventChoicesPanel").gameObject;
		foreach(Transform child in choicePanel.transform){
			Destroy(child.gameObject);
		}
		choicePanel.SetActive(false);
	}

	public bool requirementsMet(EventChoice choice){
		foreach(ResReq req in choice.resReqs){
			PlayerResources resource = GameInformation.main.playerResources.Find(delegate(PlayerResources res) { return res.name == req.resource;});
			if(resource.currentValue < req.minimum) return false;
		}
		foreach(StatReq req in choice.statReqs){
			PlayerStats stat = GameInformation.main.playerStats.Find(delegate(PlayerStats st) { return st.name == req.stat;});
			if(stat.currentValue < req.minimum) return false;
		}
		if(GameInformation.main.currentEnergy + choice.energyChange < 0) return false;
		return true;
	}



	public void submitChoice(){
		EventChoice selectedChoice = EventSystem.current.currentSelectedGameObject.GetComponent<ChoiceDataHolder>().choiceData;
		GameInformation.main.todaysEvents.RemoveAt(0);
		GameEventHandler.main.applyChoiceEffects(selectedChoice);
		Destroy(GameEventHandler.main.subPanel);
		if(GameInformation.main.todaysEvents.Count == 0){
			GameEventHandler.main.eventPanel.SetActive(false);
		} else {
			GameEventHandler.main.displayNextEvent();
		}
	}

	public void applyChoiceEffects(EventChoice choice){
		foreach(ResChange change in choice.resChanges){
			GameInformation.main.playerResources.Find(delegate(PlayerResources res) { return res.name == change.resource;}).currentValue += change.changeValue;
		}
		foreach(StatChange change in choice.statChanges){
			GameInformation.main.playerStats.Find(delegate(PlayerStats stat) { return stat.name == change.stat;}).currentValue += change.changeValue;
		}
		GameInformation.main.currentEnergy += choice.energyChange;
		if(choice.addToSceneQueue != null){
			if(choice.addToSceneQueue.occurs == EventOccurrence.Immediately){
				GameInformation.main.todaysEvents.Insert(0, choice.addToSceneQueue);
			} else {
				GameInformation.main.queuedEvents.Add(choice.addToSceneQueue);
				if(choice.addToSceneQueue.occurs == EventOccurrence.OnDelay) choice.addToSceneQueue.dayDelayTarget = GameInformation.main.currentDay += choice.addToSceneQueue.dayDelayAmount;
			}
		}
		if(choice.removeFromSceneQueue != null){
			int matchIndex = GameInformation.main.queuedEvents.FindIndex(delegate(GameEvent evn) {return evn.name == choice.removeFromSceneQueue.name;});
			GameInformation.main.queuedEvents.RemoveAt(matchIndex);
		}
		if(choice.addToLocations != null){
			if(!GameInformation.main.availableLocations.Contains(choice.addToLocations)){
				GameInformation.main.availableLocations.Add(choice.addToLocations);
			}
		}
		if(choice.removeFromLocations != null){
			GameInformation.main.availableLocations.Remove(choice.removeFromLocations);
		}


		DisplayGameInformation.main.updateAllDisplays();
		LogDisplay.main.parseChoiceLog(choice);
	}


}
