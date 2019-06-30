using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TrapHandler : MonoBehaviour {

	public static TrapHandler main;

	public GameObject buildTrapPanel, destroyTrapPanel;
	public GameObject trapListPanel, destroyTrapListPanel;
	public GameObject trapBtnPrefab, destroyTrapBtnPrefab;

	// Use this for initialization
	void Start () {
		if(main == null){
			main = this;
		} else {
			Destroy(this);
		}
		trapListPanel = GameObject.Find("AvailableTrapPanel");
		buildTrapPanel = GameObject.Find("BuildTrapPanel");
		destroyTrapPanel = GameObject.Find("DestroyTrapPanel");
		destroyTrapListPanel = GameObject.Find("DestroyTrapList");
		buildTrapPanel.SetActive(false);
		destroyTrapPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void buildNewTrap(){
		TrapDataHolder trap = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.GetComponent<TrapDataHolder>();
		if(buildable(trap)){
				Trap foundTrap = GameInformation.main.availableTraps.Find(delegate (Trap trp) {return trp.trapName == trap.trapName;});
				GameInformation.main.builtTraps.Add(foundTrap);
				DisplayGameInformation.main.updateAllDisplays();
				LogDisplay.main.parseBuildTrapLog(trap.trapName);
				NotificationHandler.main.newNotification("Successfuly built " + trap.trapName + "!");
		}
	}

	public void destroyBuiltTrap(){
		TrapDataHolder trap = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.GetComponent<TrapDataHolder>();
		Trap foundTrap = GameInformation.main.builtTraps.Find(delegate (Trap trp) {return trp.trapName == trap.trapName;});
		GameInformation.main.builtTraps.Remove(foundTrap);
		DisplayGameInformation.main.updateAllDisplays();
		Destroy(trap.gameObject);
	}


	private bool buildable(TrapDataHolder data){
		if(GameInformation.main.builtTraps.Count < 4){
			if(data.costs.Count > 0)
			{
				foreach(ResourceCost cost in data.costs)
				{
					PlayerResources resToFind = GameInformation.main.playerResources.Find(x => x.name == cost.resource);
					if(resToFind.currentValue < cost.amount) {
						NotificationHandler.main.newNotification("Insufficient resources to build trap.");			
						return false;
					}
					else 
					{
						resToFind.currentValue -= cost.amount;
					}
				}
			}
		}
		else
		{ 
			NotificationHandler.main.newNotification("There's no more space to build a new trap.");		
			return false; 
		}

		// returns true if it gets this far without a problem
		return true;
	}

	public void showTrapPanel(){
		buildTrapPanel.SetActive(true);
		foreach(Trap availableTrap in GameInformation.main.availableTraps){
			GameObject trapBtn = Instantiate(trapBtnPrefab, trapListPanel.transform.position, trapListPanel.transform.rotation, trapListPanel.transform);
			trapBtn.GetComponent<TrapDataHolder>().trapName = availableTrap.trapName;
			trapBtn.GetComponent<TrapDataHolder>().trapDescrip = availableTrap.trapDescrip;
			trapBtn.GetComponent<TrapDataHolder>().reuseable = availableTrap.reuseable;
			trapBtn.GetComponent<TrapDataHolder>().costs = availableTrap.costs;
			trapBtn.transform.Find("NameText").GetComponent<Text>().text = availableTrap.trapName;
		}
	}

	public void showDestroyTrapPanel(){
		destroyTrapPanel.SetActive(true);
		foreach(Trap builtTrap in GameInformation.main.builtTraps){
			GameObject trapBtn = Instantiate(destroyTrapBtnPrefab, trapListPanel.transform.position, trapListPanel.transform.rotation, destroyTrapListPanel.transform);
			trapBtn.GetComponent<TrapDataHolder>().trapName = builtTrap.trapName;
			trapBtn.GetComponent<TrapDataHolder>().trapDescrip = builtTrap.trapDescrip;
			trapBtn.GetComponent<TrapDataHolder>().reuseable = builtTrap.reuseable;
			trapBtn.GetComponent<TrapDataHolder>().costs = builtTrap.costs;
			trapBtn.transform.Find("NameText").GetComponent<Text>().text = builtTrap.trapName;
		}
	}

	public void closeTrapPanel(){
		foreach(Transform child in trapListPanel.transform){
			Destroy(child.gameObject);
		}
		buildTrapPanel.SetActive(false);
	}

	public void closeDestroyTrapPanel(){
		foreach(Transform child in destroyTrapListPanel.transform){
			Destroy(child.gameObject);
		}
		destroyTrapPanel.SetActive(false);
	}
}
