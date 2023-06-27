using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameTextPop : MonoBehaviour
{
	private Vector3 offset = new Vector3(0f,4f,0f);
	private void Start()
	{
		Destroy(gameObject,1f);

		transform.localPosition += offset;
	}
}
