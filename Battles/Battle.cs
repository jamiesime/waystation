using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Battle : ScriptableObject {

	public string enemyName;
	public int enemyHealth;
	public int enemyBaseAtk;
	public ResourceNames enemyFav;
	public int enemyBaseCost;
	public bool fleeable;
	public Image enemySprite;
	public Image battleBG;
	public List<string> flavourDialogue;

	
}
