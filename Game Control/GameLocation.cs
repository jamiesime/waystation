using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu]
public class GameLocation : ScriptableObject {

	public LocationName place;
	public Sprite backgroundImg;
	public List<GameEvent> eventActions;
	public List<UniqueEvent> uniqueActions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
