using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{

    public static GameInformation main;

    public GameLocation currentLocation;
    public List<GameLocation> availableLocations;
	public List<GameEvent> queuedEvents;
    public List<GameEvent> todaysEvents;
    public List<GameEvent> randomEvents;

    public int currentDay;
    public int finalDay;
    public GamePhase gamePhase;
    public int currentEnergy;
    public int maxEnergy;
    public List<PlayerStats> playerStats;
    public List<PlayerResources> playerResources;
    public List<Trap> builtTraps;
    public List<Trap> availableTraps;
 
    void Awake()
    {
        if(main == null){
            main = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this.gameObject);
        }

        GameEvent[] tmpRnd = Resources.LoadAll<GameEvent>("Prefabs/Events");
        foreach(GameEvent thing in tmpRnd){
            if(thing.occurs == EventOccurrence.Random){
                randomEvents.Add(thing);
            }
        }
        if(currentLocation == null){
            currentLocation = (GameLocation) Resources.Load("Prefabs/GameLocations/Waystation");
        }
        if(availableLocations.Count < 2){
            availableLocations.Add((GameLocation) Resources.Load("Prefabs/GameLocations/Waystation"));
            availableLocations.Add((GameLocation) Resources.Load("Prefabs/GameLocations/Lake"));
        }
    }

    public void restoreEnergy(){
        currentEnergy = maxEnergy;
    }

    public void allStatsToDefault(){
        for (int index= 0; index < playerStats.Count; index++){
            PlayerStats stat = playerStats[index];
            stat.currentValue = playerStats[index].defaultValue;
            playerStats[index] = stat;
        }
    }

    public void allResourcesToDefault(){
         for (int index= 0; index < playerStats.Count; index++){
            PlayerResources res = playerResources[index];
            res.currentValue = playerResources[index].defaultValue;
            playerResources[index] = res;
        }
    }


}

    [Serializable]
    public class PlayerStats {
        public StatNames name;
        public int currentValue;
        public int defaultValue;
    }

    [Serializable]
    public class PlayerResources {
        public ResourceNames name;
        public int currentValue;
        public int defaultValue;
    }