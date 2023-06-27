using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower
{
	public string name;
	public int cost;
	public GameObject towerPrefab;
	public float bps;

	public Tower(string name, int cost, GameObject towerPrefab, float bps)
	{
		this.name = name;
		this.cost = cost;
		this.towerPrefab = towerPrefab;
		this.bps = bps;
	}
}
