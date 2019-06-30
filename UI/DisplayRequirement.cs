using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRequirement : MonoBehaviour {
	
	public static DisplayRequirement main;

	public GameObject requirementPrefab;

	public Sprite scrapIcon, woodIcon, strIcon, healthIcon;

	// Use this for initialization
	void Awake () {
		if(main == null){
			main = this;
		}
	}

	public void displayResourceRequirement(Transform parentPanel, ResourceNames type, int amount){
		GameObject resInstance = Instantiate(requirementPrefab, parentPanel.transform.position, parentPanel.transform.rotation, parentPanel);
		resInstance.transform.GetChild(0).GetComponent<Text>().text = amount.ToString();
		resInstance.transform.GetChild(1).GetComponent<Image>().sprite = getResourceIcon(type);
		resInstance.GetComponent<RequirementDataHolder>().reqAmount = amount;
		resInstance.GetComponent<RequirementDataHolder>().reqName = type.ToString();
	}

	public void displayStatRequirement(Transform parentPanel, StatNames type, int amount){
		GameObject resInstance = Instantiate(requirementPrefab, parentPanel.transform.position, parentPanel.transform.rotation, parentPanel);
		resInstance.transform.GetChild(0).GetComponent<Text>().text = amount.ToString();
		resInstance.transform.GetChild(1).GetComponent<Image>().sprite = getStatIcon(type);
		resInstance.GetComponent<RequirementDataHolder>().reqAmount = amount;
		resInstance.GetComponent<RequirementDataHolder>().reqName = type.ToString();
	}

	public void displayEnergyRequirement(int amount){

	}

	private Sprite getResourceIcon(ResourceNames type){
		switch(type){
			case ResourceNames.Scrap:
				return scrapIcon;
			break;
			case ResourceNames.Wood:
				return woodIcon;
			break;
			default:
				return scrapIcon;
			break;
		}
	}

	private Sprite getStatIcon(StatNames type){
		switch(type){
			case StatNames.Health:
				return healthIcon;
			break;
			case StatNames.Strength:
				return strIcon;
			break;
			default:
				return strIcon;
			break;
		}
	}
}
