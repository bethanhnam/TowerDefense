using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform turretRotationPoint;
	[SerializeField] private LayerMask enemyMask;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private Transform ShootingPosition;
	[SerializeField] private GameObject upgradeUI;
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Button sellButton;

	[Header("Attribute")]
	[SerializeField] private float targetingRange = 5f;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float bps = 1f; // bulet per second
	[SerializeField] private int baseUpgradeCost = 100;

	private float bpsBase;
	private float targetingRangeBase;

	private float timeUntilFire;
	private Transform target;

	private int level = 1;
	[SerializeField] private Text upgradeCost;
	[SerializeField] private GameObject upgradeTextPrefab;
	[SerializeField] private GameObject canvas;
	[SerializeField] private int sellCost;
	private void Awake()
	{
		turretRotationPoint = transform.Find("RotatePoint");
	}
	private void Start()
	{
		canvas = GameObject.Find("Canvas");
		bpsBase = bps;
		targetingRangeBase = targetingRange;
		upgradeButton.onClick.AddListener(Upgrade);
		sellButton.onClick.AddListener(Sell);

	}
	private void Update()
	{
		if (target == null)
		{
			FindTarget();
			return;
		}
		RotateTowardsTarget();
		if (!checkTargetIsInRange())
		{
			target = null;
		}
		else
		{
			timeUntilFire += Time.deltaTime;
			if (timeUntilFire >= 1f / bps)
			{
				Shooting();
				timeUntilFire = 0f;
			}
		}
	}

	private void Shooting()
	{
		GameObject bulletobj = Instantiate(bulletPrefab, ShootingPosition.position, Quaternion.identity);
		Bullet bulletScript = bulletobj.GetComponent<Bullet>();
		bulletScript.SetTarget(target);
	}

	private bool checkTargetIsInRange()
	{
		return Vector2.Distance(target.position, transform.position) < targetingRange;
	}

	private void RotateTowardsTarget()
	{
		float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotateSpeed * Time.deltaTime);
	}

	private void FindTarget()
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
		if (hits.Length > 0)
		{
			target = hits[0].transform;
		}
	}

	public void OpenUpgradeUI()
	{
		upgradeUI.SetActive(true);
		upgradeCost.text = CalculateCost().ToString();
	}
	public void CloseUpgradeUI()
	{
		upgradeUI.SetActive(false);
		UIManager.Instance.SetHoveringState(false);
	}
	public void Upgrade()
	{
		if (CalculateCost() <= LevelManager.Instance.currency)
		{
			LevelManager.Instance.SpendCurrency(CalculateCost());
			level++;
			Instantiate(upgradeTextPrefab, transform.position, Quaternion.identity, canvas.transform);
			bps = CalculateBPS();
			targetingRange = CalculateBPS();
			CloseUpgradeUI();
			Debug.Log("New BPS" + bps);
			Debug.Log("New BPS" + targetingRange);
			Debug.Log("New cost" + CalculateCost());
		}
	}
	public void Sell()
	{
		LevelManager.Instance.IncreaseCurrency(sellCost);
		CloseUpgradeUI();
		Destroy(gameObject);
	}
	private int CalculateCost()
	{
		return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
	}
	public void CalculateSellCost(float baseCost)
	{
		sellCost = Mathf.RoundToInt(baseCost + (level - 1) * baseUpgradeCost);
	}
	private float CalculateBPS()
	{
		return bpsBase * Mathf.Pow(level, 0.6f);
	}
	private float CalculateRange()
	{
		return targetingRangeBase * Mathf.Pow(level, 0.4f);
	}
	private void OnDrawGizmosSelected()
	{
		Handles.color = Color.cyan;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}
}
