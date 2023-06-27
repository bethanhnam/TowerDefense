using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Animator animator;
    [Header("Attributesd")]
    [SerializeField] private int objectHealth = 2;
    private bool isDestroyed = false;
    [SerializeField] private int currencyWorth = 10;
	[SerializeField] private GameObject[] dameTextPrefab;
	[SerializeField] private GameObject canvas;
	int currentPrefab;
	private void Start()
	{
		animator = GetComponent<Animator>();
		canvas = GameObject.Find("Canvas");
		currentPrefab = Random.Range(0, dameTextPrefab.Length);
	}
	public void TakeDamage(int damage)
    {
		objectHealth -= damage;
		animator.SetBool("Hit",true);
		PopDameText();
		Invoke("ResetAnimator", 0.5f);
		if (objectHealth <= 0&&!isDestroyed)
        {
            EnemySpawner.OnEnemyDestroy.Invoke();
            LevelManager.Instance.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
	private void ResetAnimator()
	{
		animator.SetBool("Hit", false);
	} 
	public void PopDameText()
	{
		var dameText = Instantiate(dameTextPrefab[currentPrefab], transform.position, Quaternion.identity,canvas.transform);
	}
}
