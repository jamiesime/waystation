using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DisplayTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject toolTipPrefab;
	private GameObject tipInstance;

	public void OnPointerEnter(PointerEventData eventData){
		tipInstance =  Instantiate(toolTipPrefab, Input.mousePosition + (Vector3.down * 100), this.transform.rotation, GameObject.Find("MainUI").transform);
		tipInstance.GetComponent<Tooltip>().belongsTo = eventData.pointerEnter;
	}

	public void OnPointerExit(PointerEventData eventData){
		Destroy(tipInstance.gameObject);
	}


}
