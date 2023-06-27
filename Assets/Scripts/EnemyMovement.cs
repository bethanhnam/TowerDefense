using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Attributes")]
    [SerializeField] private float moveSpeed =2f;
    private Transform target;
	int pathIndex;

	private float baseSpeed;
	private void Start()
	{
		baseSpeed = moveSpeed;
		rb = GetComponent<Rigidbody2D>();
		target = LevelManager.Instance.path[0];
	}
	private void Update()
	{
		if(Vector2.Distance(target.position,transform.position) < 0.1f)
		{
			pathIndex++;
			
		}
		if (pathIndex == LevelManager.Instance.path.Length)
		{
			EnemySpawner.OnEnemyDestroy.Invoke();
			Destroy(gameObject);
			GameManager.Instance.EndGame();
			return;
		}
		else
		{
			target = LevelManager.Instance.path[pathIndex];
		}
	}
	private void FixedUpdate()
	{
		Vector2 direction = (target.position - transform.position).normalized;
		rb.velocity = direction * moveSpeed;
	}
	public void UpdateSpeed(float newSpeed)
	{
		moveSpeed = newSpeed;
	}
	public void ResetSpeed()
	{
		moveSpeed = baseSpeed;
	}
}
