using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;
	public Transform startPoint;
	public Transform[] path;

	public int currency;
	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	private void Start()
	{
		currency = 100;
	}
	public void IncreaseCurrency(int amount)
	{
		currency += amount;
	}
	public bool SpendCurrency(int amount)
	{
		if(amount <= currency)
		{
				currency -= amount;
				return true;
		}
		else
		{
			Debug.Log("not enough money");
			return false;
		}

	}
}
