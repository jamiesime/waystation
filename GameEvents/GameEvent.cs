using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class GameEvent : ScriptableObject {


	[Header("Appearance Info")]
	public EventOccurrence occurs;

	[DrawIf("occurs", EventOccurrence.SpecificDay)]
	public int specificDayNumber;
	[DrawIf("occurs", EventOccurrence.OnDelay)]
	public int dayDelayAmount;
	[HideInInspector]
	public int dayDelayTarget;
	[DrawIf("occurs", EventOccurrence.Random)]
	public GamePhase appears;
	
	[DrawIf("occurs", EventOccurrence.Random)]
	public int chance = 50;

	[Header("Event Info")]
	public string header;

	[TextArea(6, 10)]
	public string[] pages;
	public EventChoice[] choices;


	 [ExecuteInEditMode]
     void OnValidate(){
			 	 chance = chance > 100 ? 100 : chance;
				 chance = chance < 10 ? 10 : chance;
     }
}

[Serializable]
public struct EventChoice {

	public string displayText;
	public GameEvent addToSceneQueue;
	public GameEvent removeFromSceneQueue;
	public int energyChange;
	public StatReq[] statReqs;
	public ResReq[] resReqs;
	public StatChange[] statChanges;
	public ResChange[] resChanges;

	public GameLocation addToLocations;
	public GameLocation removeFromLocations;
	public Trap addToTraps;
}

[Serializable]
public struct StatChange {
	public StatNames stat;
	public int changeValue;
} 

[Serializable]
public struct ResChange {
	public ResourceNames resource;
	public int changeValue;
}

[Serializable]
public struct StatReq {
	public StatNames stat;
	public int minimum;
}

[Serializable]
public struct ResReq {
	public ResourceNames resource;
	public int minimum;
}

public enum GamePhase{
	Early,
	Mid,
	Late
}