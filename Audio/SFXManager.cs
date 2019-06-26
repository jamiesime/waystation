using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	public static SFXManager main;

	public AudioSource sfx1;
	public AudioSource sfx2;

	// Use this for initialization
	void Start () {
		if(main == null){
			main = this;
		} else {
			Destroy(this.gameObject);
		}
		sfx1 = transform.Find("SFX1").GetComponent<AudioSource>();		
		sfx2 = transform.Find("SFX2").GetComponent<AudioSource>();		
	}
	
	public void playSoundOnce(AudioClip clip, int track = 1, float vol = 1){
		switch (track){
			case 1:
				sfx1.volume = vol;
				sfx1.PlayOneShot(clip);
			break;
			case 2: 
				sfx2.volume = vol;
				sfx2.PlayOneShot(clip);
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
