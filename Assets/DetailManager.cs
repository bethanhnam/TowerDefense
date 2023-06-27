using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailManager : MonoBehaviour
{
	public static DetailManager instance;
	public Button[] buttons;
	[SerializeField] Image turretImage;
	[SerializeField] Text turretName;
	[SerializeField] Text turretDamage;
	[SerializeField] Text turretCost;
	[SerializeField] GameObject detailPanel;
	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		turretImage.sprite = buttons[0].GetComponent<Image>().sprite;
	}
	public void SetDetail()
	{
		detailPanel.SetActive(true);
		turretImage.sprite = buttons[BuildManager.instance.currentSelectedTower - 1].GetComponent<Image>().sprite;
		turretName.text = BuildManager.instance.towers[BuildManager.instance.currentSelectedTower - 1].name.ToString();
		turretDamage.text = "Attack per second :" + BuildManager.instance.towers[BuildManager.instance.currentSelectedTower - 1].bps.ToString();
		turretCost.text = "Cost :" + BuildManager.instance.towers[BuildManager.instance.currentSelectedTower - 1].cost.ToString();

	}
	public void SetDetail(int i)
	{
		detailPanel.SetActive(true);
		turretImage.sprite = buttons[i].GetComponent<Image>().sprite;
		turretName.text = BuildManager.instance.towers[i].name.ToString();
		turretDamage.text = "Attack per second :" + BuildManager.instance.towers[i].bps.ToString();
		turretCost.text = "Cost :" + BuildManager.instance.towers[i].cost.ToString();
	}
	public void ResetDetail()
	{
		detailPanel.SetActive(false);
	}
}
