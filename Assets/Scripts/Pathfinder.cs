using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;
using Unity.Burst.CompilerServices;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class Pathfinder : MonoBehaviour
{

	private static Tilemap tilemap;
	private static GameObject grid;
	static readonly ProfilerMarker p = new ProfilerMarker("Pathfind");
	static readonly ProfilerMarker p2 = new ProfilerMarker("Min Search");
	static readonly ProfilerMarker p3 = new ProfilerMarker("Neighbour Search");
	void Start()
	{
		tilemap = GameObject.Find("Floor").GetComponent<Tilemap>();
		grid = GameObject.Find("Grid");
	}
	public static IEnumerator HighlightPath(Transform from, Transform to)
	{
		ClearLog();
		
			if (tilemap != null && grid != null)
			{

				Vector3 fromPoz = from.position;
				Vector3 toPoz = to.position;

				Vector3Int cellPoz = grid.GetComponent<Grid>().WorldToCell(fromPoz);
				Vector3Int cellPoz2 = grid.GetComponent<Grid>().WorldToCell(toPoz);
				TileBase tile = tilemap.GetTile(cellPoz);

				foreach (var poz in tilemap.cellBounds.allPositionsWithin)
				{
					if (tilemap.HasTile(poz))
					{
						DrawRect(grid.GetComponent<Grid>().CellToWorld(poz), Color.gray);
					}
				}

				DrawRect(grid.GetComponent<Grid>().CellToWorld(cellPoz));
				DrawRect(grid.GetComponent<Grid>().CellToWorld(cellPoz2));
				/*foreach(var item in GetNeighbours(cellPoz2))
				{
					DrawRect(grid.GetComponent<Grid>().CellToWorld(item), Color.green);
				}*/
				List<Vector3Int> list = Pathfind(cellPoz, cellPoz2);
				if (list != null)
				{
					foreach (var item in list)
					{
						DrawRect(grid.GetComponent<Grid>().CellToWorld(item), Color.green);
					}

				}/**/


				yield return new WaitForSeconds(0.5f);

			}
		
		
		

	}
	public static List<Vector3Int> Pathfind(Vector3Int from, Vector3Int to)
	{
		using (p.Auto())
		{
			Node fromNode = new(from, g: 0, h: Distance(from, to));
			Dictionary<Vector3Int, Node> map = new();
			map.Add(from, fromNode);
			HashSet<Vector3Int> open = new() { from };
			int iterations = 0;

			while (open.Count > 0)
			{
				iterations++;
				//openbõl a legkisebb

				Node node = null;
				Vector3Int pos = Vector3Int.zero;
				p2.Begin();
				foreach (var itPos in open)
				{
					Node it;
					it = map[itPos];
					if (node == null)
					{
						node = it;
						pos = itPos;
					}
					if (node.f > it.f)
					{
						node = it;
						pos = itPos;
					}
				}
				p2.End();
				open.Remove(pos);
				DrawRect(pos, Color.yellow);

				//ha a legkisebb a cél akkor return

				if (pos == to)
				{
					List<Vector3Int> path = new();
					Node iterator = node;
					while (iterator != null)
					{
						path.Add(iterator.pos);
						iterator = iterator.parent;
					}
					path.Reverse();
					Debug.Log(iterations);
					return path;
				}
				p3.Begin();
				//legkisebb szomszédai
				foreach (var neighbourPos in GetNeighbours(pos))
				{
					//megnézni hogy a szomszéd benne van-e a map-ben
					Node neighbour;
					if (map.TryGetValue(neighbourPos, out neighbour))
					{
						float newG = node.g + Distance(neighbourPos, pos);
						if (neighbour.g >= newG)
						{
							neighbour.g = newG;
							neighbour.f = neighbour.h + newG;
							neighbour.parent = node;
							open.Add(neighbourPos);
						}
					}
					else
					{
						neighbour = new(neighbourPos, node, node.g + Distance(neighbourPos, pos), Distance(neighbourPos, to));
						map.Add(neighbourPos, neighbour);
						open.Add(neighbourPos);
					}
				}
				p3.End();
			}
		}
		return null;


	}
	/*public static List<Vector3Int> Pathfind_(Vector3Int from, Vector3Int to)
	{
		using(p.Auto())
		{
			int asd = 0;
			foreach (var poz in tilemap.cellBounds.allPositionsWithin)
			{
				if (tilemap.HasTile(poz))
				{
					asd++;
				}
			}
			Debug.Log(asd);

			List<Vector3Int> path = new();
		Node fromNode = new(from, g: 0.0f, h: Distance(from, to));
		HashSet<Node> open = new() { fromNode };
		HashSet<Node> closed = new();
		
		if (tilemap == null || grid == null)
		{
			return path;
		}

		
		while (open.Count > 0 )
		{
			
			Node min = null;
			foreach (Node node in open)
			{
				if (min == null)
				{
					min = node;
					continue;
				}
				if (node.f < min.f)
				{
					min = node;
				}
				if (Math.Abs(node.f - min.f) < 0.001)
				{
					if(node.h < min.h)
					min = node;
				}

				
			}
			open.Remove(min);
			closed.Add(min);
			//DrawRect(min.pos, Color.yellow);

			if (min.pos == to)
			{
				Node iterator = min;
				while (iterator != null)
				{
					path.Add(iterator.pos);
					iterator = iterator.parent;
				}
				path.Reverse();

				return path;
			}
			List<Vector3Int> neighbours = GetNeighbours(min.pos);
			foreach (var tile in neighbours)
			{
				Node node = new Node(tile, min, min.g + Distance(min.pos, tile), Distance(tile, to));
				bool cont = false;
				foreach (Node existing in closed)
				{
					if (existing.pos == node.pos && existing.f < node.f)
					{
						cont = true;
						closed.Remove(existing);
						open.Add(node);

						break;
					}			
				}
				foreach (Node existing in open)
				{
					
					if (existing.pos == node.pos && existing.g < node.g)
					{
						cont = true;
						existing.Copy(node);
						break;
					}		
				}
				if (cont)
				{
					continue;
				}
					
				open.Add(node);
			}
		}
		return null;
		}
	}*/
	private static float Distance(Vector3Int from, Vector3Int to)
	{
		float distance = 0.0f;
		//int diag = Math.Min(Math.Abs(from.x - to.x), Math.Abs(from.y - to.y));
		int straight = Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
		distance = straight; //- diag + (diag * 0.4f);
		return distance;
	}
	public static void DrawRect(Vector3 center, Color? color = null, float duration = 0.5f)
	{
		Color _color = color ?? Color.red;
		Vector3 offset = new Vector3(0.5f, 0.5f, 0);

		Vector3 A = center + new Vector3(-0.5f, 0.5f, 0)   +offset;
		Vector3 B = center + new Vector3(-0.5f, -0.5f, 0)  +offset;
		Vector3 C = center + new Vector3(0.5f, -0.5f, 0)   +offset;
		Vector3 D = center + new Vector3(0.5f, 0.5f, 0) + offset;

		Debug.DrawLine(B, C, _color, duration);
		Debug.DrawLine(C, D, _color, duration);
		Debug.DrawLine(D, A, _color, duration);
		Debug.DrawLine(A, B, _color, duration);
	}
	public static void ClearLog()
	{
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
		var type = assembly.GetType("UnityEditor.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}
	public static List<Vector3Int> GetNeighbours(Vector3Int tile)
	{
		List<Vector3Int> list = new();
		if (tilemap == null)
			return list;
		/*for(int i = -1;i <=1; i++)
		{
			for(int j = -1;j <=1; j++)
			{
				Vector3Int vector = tile + new Vector3Int(i,j);
				if((i != 0 || j != 0) && tilemap.HasTile(vector))
				{
					list.Add(vector);
				}
			}
		}*/
		Action<int, int> isValid = (int x, int y) =>
		{
			Vector3Int vector = new Vector3Int(x, y);
			if(tilemap.HasTile(tile + vector))
			{
				list.Add(tile + vector);
			}
		};
		isValid(0, 1);
		isValid(1, 0);
		isValid(0, -1);
		isValid(-1, 0);



		return list;
	}

}

class Node
{
	
	public Node parent;
	public Vector3Int pos;
	public float g, h, f;
	public Node(Vector3Int pos, Node parent = null, float g = 0.0f, float h = 0.0f)
	{
		this.pos = pos;
		this.parent = parent;
		this.g = g;
		this.h = h;
		f = g + h;
		
	}
	public void Copy(Node node)
	{
		Assert.IsTrue(node.pos == pos);
		pos = node.pos;
		parent = node.parent;
		g = node.g;
		h = node.h;
		f = node.f;
	}
}
