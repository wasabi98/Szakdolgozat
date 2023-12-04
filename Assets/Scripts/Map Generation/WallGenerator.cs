using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
	{
		var basicWallPositions = FindWallsInDirection(floorPositions, Direction.cardinalDirections);
		var cornerWallPositions = FindWallsInDirection(floorPositions, Direction.diagonalDirections);
		CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
		CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
	}

	private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPosition, HashSet<Vector2Int> floorPositions)
	{
		foreach (var position in cornerWallPosition)
		{
			string neighboursBinaryType = "";
			foreach (var direction in Direction.eightDirections)
			{
				var neighbourPosition = position + direction;
				if (floorPositions.Contains(neighbourPosition))
				{
					neighboursBinaryType += "1";
				}
				else
				{
					neighboursBinaryType += "0";
				}
			}
			tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
		}
	}

	private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
	{
		foreach (var position in basicWallPositions)
		{
			string neighboursBinaryType = "";
			foreach (var direction in Direction.cardinalDirections)
			{
				var neighbourPosition = position + direction;
				if(floorPositions.Contains(neighbourPosition))
				{
					neighboursBinaryType += "1";
				}
				else
				{
					neighboursBinaryType += "0";
				}
			}
			tilemapVisualizer.PaintBasicWall(position, neighboursBinaryType);
		}
	}

	private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
	{
		HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
		foreach (var positiron in floorPositions)
		{
			foreach (var direction in directionList)
			{
				var neightbourPosition = positiron + direction;
				if(floorPositions.Contains(neightbourPosition) == false)
					wallPositions.Add(neightbourPosition);
			}
		}
		return wallPositions;
	}
}
