using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementTooltip : Tooltip {

	public GameObject descriptText;

	// Use this for initialization
	void Start () {
		descriptText = transform.Find("DescripText").gameObject;
		if(belongsTo.GetComponent<RequirementDataHolder>()){
			string amt = belongsTo.GetComponent<RequirementDataHolder>().reqAmount.ToString();
			string req = belongsTo.GetComponent<RequirementDataHolder>().reqName.ToString();
			descriptText.GetComponent<Text>().text = "Requires " + amt  + " " + req; 
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!belongsTo){
			Destroy(this.gameObject);
		}
	}
}
