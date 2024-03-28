using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HighscoreMenu : MonoBehaviour
{
    public void Refresh()
    {
		ClearTable();
		
	}
	private void ClearTable()
	{
		GameObject content = GameObject.Find("Content");
		for (var i = content.transform.childCount - 1; i >= 0; i--)
		{
			Destroy(content.transform.GetChild(i).gameObject);
		}
	}
}
