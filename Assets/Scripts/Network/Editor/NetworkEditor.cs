using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NetworkPost))]
public class NetworkEditor : Editor
{
	NetworkPost network;

	public void Awake()
	{
		network = (NetworkPost)target;
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