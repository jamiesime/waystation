using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildTrapTooltip : Tooltip {

	public GameObject descriptText, costPanel;

	// Use this for initialization
	void Start () {
		descriptText = transform.Find("DescripText").gameObject;
		if(belongsTo.GetComponent<TrapDataHolder>()){
			descriptText.GetComponent<Text>().text = belongsTo.GetComponent<TrapDataHolder>().trapDescrip;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
