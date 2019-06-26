using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceDayAuto : MonoBehaviour {

	public GameObject lastDay, nextDay;
	public AudioClip dayTickSfx;
	public AudioClip newDaySting;

	// Use this for initialization
	void Start () {
		AmbienceManager.main.silenceAmbience(5f);
		lastDay = transform.Find("LastDay").gameObject;
		nextDay = transform.Find("NextDay").gameObject;
		lastDay.GetComponent<Text>().text = (GameInformation.main.currentDay - 1).ToString();
		nextDay.GetComponent<Text>().text = (GameInformation.main.currentDay).ToString();
		Vector3 target = lastDay.transform.position;
		iTween.MoveTo(nextDay, iTween.Hash("position", target, "time", 1f, "easetype", "easeInOutQuad"));
		iTween.MoveTo(lastDay, iTween.Hash("position", target + (Vector3.down * 100), "time", 1f, "easetype", "easeInOutQuad"));
		SFXManager.main.playSoundOnce(dayTickSfx, 1);
		StartCoroutine(waitThenRemove());
	}
	
	IEnumerator waitThenRemove(){
		yield return new WaitForSeconds(1f);
		// SFXManager.main.playSoundOnce(newDaySting, 2, 0.3f);
		yield return new WaitForSeconds(1f);
		AmbienceManager.main.resumeAmbience(1f, 0.1f);
		Destroy(this.gameObject);
	}
}
