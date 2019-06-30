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
			TrapDataHolder trap = belongsTo.GetComponent<TrapDataHolder>();
			descriptText.GetComponent<Text>().text = trap.trapDescrip;
			foreach(ResourceCost cost in trap.costs){
				DisplayRequirement.main.displayResourceRequirement(transform.Find("RequirementPanel").transform, cost.resource, cost.amount);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
