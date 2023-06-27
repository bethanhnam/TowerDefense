using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
   WayPoint wayPoint => target as WayPoint;
	private void OnSceneGUI()
	{
		Handles.color = Color.cyan;
		for(int i = 0; i < wayPoint.Points.Length; i++)
		{
			EditorGUI.BeginChangeCheck();
			//crete handles
			Vector3 currentWaypointPoint = wayPoint.CurrentPosition + wayPoint.Points[i];
			Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

			//create text
			GUIStyle textStyle = new GUIStyle();
			textStyle.fontStyle = FontStyle.Bold;
			textStyle.fontSize = 18;
			textStyle.normal.textColor = Color.white;
			Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
			Handles.Label(wayPoint.CurrentPosition + wayPoint.Points[i] + textAlligment,$"{i+1}",textStyle);
			EditorGUI.EndChangeCheck();
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Free Move Handle");
				wayPoint.Points[i] = newWaypointPoint - wayPoint.CurrentPosition;
			}
		}
	}
}
