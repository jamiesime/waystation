using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Trap : ScriptableObject {

	public string trapName;
	public Sprite icon;
	[TextArea(1, 2)]
	public string trapDescrip;
	public List<ResourceCost> costs;
	public bool reuseable;
	public AudioClip useSound;
	public int baseDamage;
	public int chanceToHit;
	public StatusEffect applyEffect;
	public int applyChance;
	public StatusEffect effectThatMultplies;
	[Range(1, 2)]
	public float multiplier = 1f;

	[ExecuteInEditMode]
	void OnValidate(){
		chanceToHit = chanceToHit > 100 ? 100 : chanceToHit; 
		applyChance = applyChance > 100 ? 100 : applyChance; 
		chanceToHit = chanceToHit < 0 ? 0 : chanceToHit; 
		applyChance = applyChance < 0 ? 0 : applyChance; 
	}
}
