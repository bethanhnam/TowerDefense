using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;
    private Vector3 _currentPosition;
    public Vector3 CurrentPosition => _currentPosition;
	public bool gameStart;
	private void Start()
	{
		gameStart = true;
		_currentPosition = transform.position;
	}
	public Vector3 GetWayPointPosition(int index)
	{
		return CurrentPosition + points[index];
	}
	private void OnDrawGizmos()
	{
		if(!gameStart && transform.hasChanged)
		{
			_currentPosition = transform.position;
		}
		for(int i = 0; i < points.Length;i++)
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(points[i] + _currentPosition, 0.5f);
			if (i < points.Length - 1)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1] + _currentPosition);
			}
		}
	}
}
