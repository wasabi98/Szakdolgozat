using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationAlgorithms 
{
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++) 
        { 
            var newPosition = previousPosition + Direction.GetRandomDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction.GetRandomDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
    
}

public static class Direction
{
    public static List<Vector2Int> cardinalDirections = new List<Vector2Int>
    {
        new Vector2Int(0,1),    
        new Vector2Int(1,0),    
        new Vector2Int(0,-1),   
        new Vector2Int(-1,0)    
    };
	public static List<Vector2Int> diagonalDirections = new List<Vector2Int>
	{
		new Vector2Int(1,1),
		new Vector2Int(1,-1),
		new Vector2Int(-1,-1),
		new Vector2Int(-1,1)
	};

    public static List<Vector2Int> eightDirections = new List<Vector2Int>
    {
        new Vector2Int(0,1),
        new Vector2Int(1,1),
        new Vector2Int(1,0),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1,0),
        new Vector2Int(-1,1)
    };
	public static Vector2Int GetRandomDirection()
    {
        return cardinalDirections[Random.Range(0,cardinalDirections.Count)];
    }
}