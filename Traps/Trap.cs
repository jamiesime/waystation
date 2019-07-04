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
}
