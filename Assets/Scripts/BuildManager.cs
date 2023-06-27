using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;

	[Header("References")]
	//[SerializeField] private GameObject[] towerPrefabs;
	[SerializeField] public Tower[] towers;

	public int currentSelectedTower;

	private void Awake()
	{
		instance = this;
	}
	public Tower GetSelectedTower()
	{
		if (currentSelectedTower==0)
		{
			return null;
		}
		return towers[currentSelectedTower-1];
	}
	public void SetSelectedTower(int selectedTower)
	{
		currentSelectedTower = selectedTower;
	}
	public void SetDetail()
	{
		DetailManager.instance.SetDetail();
	}
}
