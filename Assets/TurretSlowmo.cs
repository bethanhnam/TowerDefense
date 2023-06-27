using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretSlowmo : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private LayerMask enemyMask;

	[Header("Attribute")]
	[SerializeField] private float targetingRange = 5f;
	[SerializeField] public float aps;// attack per second
	[SerializeField] private float freezeTime = 0.5f;
	private float timeUntilFire;
	[SerializeField] public GameObject upgradeUI;
	int sellCost;
	private void Update()
	{
		timeUntilFire += Time.deltaTime;
		if (timeUntilFire >= 1f / aps)
		{
			FreezeEnemies();
			timeUntilFire = 0f;
		}
	}

	private void FreezeEnemies()
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
		if (hits.Length > 0)
		{
			for(int i = 0;i< hits.Length; i++)
			{
				RaycastHit2D hit = hits[i];
				EnemyMovement enemyMovement = hit.transform.GetComponent<EnemyMovement>();
				enemyMovement.UpdateSpeed(0.5f);
				StartCoroutine(ResetEnemySpeed(enemyMovement));
			}
		}
	}
	public void Sell()
	{
		LevelManager.Instance.IncreaseCurrency(sellCost);
		Destroy(gameObject);

	}
	public void CalculateSellCost(float baseCost)
	{
		sellCost = Mathf.RoundToInt(baseCost);
	}
	public void OpenUpgradeUI()
	{
		upgradeUI.SetActive(true);
	}
	private IEnumerator ResetEnemySpeed(EnemyMovement em)
	{
		yield return new WaitForSeconds(freezeTime);
		em.ResetSpeed();
	}
	private void OnDrawGizmosSelected()
	{
		Handles.color = Color.cyan;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}
}
