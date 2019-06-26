using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour {

	public static AmbienceManager main;
	public AudioSource ambience;
	public bool fadeInCall;
	public bool fadeOutCall;
	private float targetVol;
	private float targetTime;

	// Use this for initialization
	void Start () {
		if(main == null){
			main = this;
		} else {
			Destroy(this.gameObject);
		}
		ambience = this.GetComponent<AudioSource>();
	}

	void Update(){
		if(fadeInCall && ambience.volume < targetVol){
			ambience.volume = iTween.FloatUpdate(ambience.volume, targetVol, targetTime);
		} else {
			fadeInCall = false;
		}
		if(fadeOutCall && ambience.volume > 0f){
			ambience.volume = iTween.FloatUpdate(ambience.volume, targetVol, targetTime);
		} else {
			fadeOutCall = false;
		}
	}
	
	public void newAmbience(AudioClip clip, float vol = 0.3f){
		ambience.clip = clip;
		targetVol = vol;
		ambience.volume = vol;
		ambience.Play();
	}

	public void silenceAmbience(float time){
		fadeOutCall = true;
		targetVol = 0f;
		targetTime = time;
	}

	public void resumeAmbience(float time, float vol = 0.3f){
		targetVol = vol;
		targetTime = time;
		fadeInCall = true;
	}
}
