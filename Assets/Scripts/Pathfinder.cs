using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    public static void HighlightPath(Transform from, Transform to)
	{
		ClearLog();
		Tilemap tilemap = GameObject.Find("Floor").GetComponent<Tilemap>();
		RaycastHit2D[] hits;
		Vector2 from2 = from.position;
		Vector2 dir = to.position - from.position;
		hits = Physics2D.RaycastAll(from2, dir, (to.position - from.position).magnitude);
		
		for (int i = 0; i < hits.Length; i++)
		{
			
			RaycastHit2D hit = hits[i];
			
			
			GameObject grid = GameObject.Find("Grid");
			//Debug.Log();
			if (tilemap != null && grid != null)
			{ 
				TileBase tile = tilemap.GetTile(grid.GetComponent<Grid>().WorldToCell(hit.transform.position));
				Debug.Log(grid.GetComponent<Grid>().WorldToCell(hit.transform.position) + "  tile");
				Debug.DrawLine(from.position, to.position, Color.white, 0.1f);
				Debug.DrawLine(hit.transform.position - new Vector3(1, 0, 0), hit.transform.position + new Vector3(1, 0, 0), Color.red, 3f);
				

			}
			/*if (rend)
			{
				Debug.Log(hit.transform.position.ToString() + "  hit position");
				rend.material.shader = Shader.Find("Transparent/Diffuse");
				Color tempColor = rend.material.color;
				tempColor.a = 0.3F;
				rend.material.color = tempColor;
			}*/
		}
		


	}
	public static void ClearLog()
	{
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
		var type = assembly.GetType("UnityEditor.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}

}
