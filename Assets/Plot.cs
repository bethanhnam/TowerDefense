using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Color hoverColor;
	private GameObject towerObj;
	public Turret turret;
	public TurretSlowmo slowTurret;
	private Color startColor;
	private void Start()
	{
		startColor = _spriteRenderer.color;
	}

	private void OnMouseEnter()
	{
		_spriteRenderer.color = hoverColor;
	}
	private void OnMouseExit()
	{
		_spriteRenderer.color = startColor;
		DetailManager.instance.ResetDetail();
	}
	private void OnMouseDown()
	{
		if (UIManager.Instance.IsHoveringUI()) return;
		if (towerObj != null)
		{
			
			if (towerObj.name == "Turret(Clone)")
			{
				turret.OpenUpgradeUI();
				DetailManager.instance.SetDetail(0);
				turret.CalculateSellCost(BuildManager.instance.towers[0].cost);
			}
			if (towerObj.name == "FastShooterTurret(Clone)")
			{
				turret.OpenUpgradeUI();
				DetailManager.instance.SetDetail(1);
				turret.CalculateSellCost(BuildManager.instance.towers[1].cost);
			}
			if (towerObj.name == "SlowTurret(Clone)")
			{
				slowTurret = GameObject.Find("SlowTurret(Clone)").GetComponent<TurretSlowmo>();
				slowTurret.OpenUpgradeUI();
				DetailManager.instance.SetDetail(2);
				slowTurret.CalculateSellCost(BuildManager.instance.towers[2].cost);
			}
			return;
		}
		if (BuildManager.instance.currentSelectedTower == 0)
		{
			return;
		}
		Tower towerToBuild = BuildManager.instance.GetSelectedTower();
		if (towerToBuild.cost > LevelManager.Instance.currency)
		{
			Debug.Log("ko mua noi");
			return;
		}
		LevelManager.Instance.SpendCurrency(towerToBuild.cost);

		towerObj = Instantiate(towerToBuild.towerPrefab, transform.position, Quaternion.identity);
		turret = towerObj.GetComponent<Turret>();
		BuildManager.instance.currentSelectedTower = 0;
		DetailManager.instance.ResetDetail();
	
	}
	private void Update()
	{
		if (!turret)
		{
			turret = null;
		}
	}
}
