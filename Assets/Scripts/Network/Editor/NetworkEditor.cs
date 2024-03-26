using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Network))]
public class NetworkEditor : Editor
{
	Network network;

	public void Awake()
	{
		network = (Network)target;
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Send Data"))
		{
			network.SendScore("Janó2", 242);
		}
	}
}