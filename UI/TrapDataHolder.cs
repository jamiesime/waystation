using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrapDataHolder : MonoBehaviour {

	public string trapName;
	public Sprite icon;
	public string trapDescrip;
	public List<ResourceCost> costs;
	public bool reuseable;


}

[Serializable]
public struct ResourceCost {
	public ResourceNames resource;
	public int amount;
}
