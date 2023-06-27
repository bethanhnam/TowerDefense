using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePop : MonoBehaviour
{
	private Vector3 offset = new Vector3(0f, 100f, 0f);
	private void Start()
	{
		Destroy(gameObject, 1f);

		transform.localPosition += offset;
	}
}
