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
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomQueue.Enqueue(spaceToSplit);
        while (roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth) 
            { 
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight*2)
                    {
                        SplitHorizontally(minWidth, roomQueue, room);
                    }
                    else if(room.size.x >= minWidth*2)
                    {
						SplitVertically(minHeight, roomQueue, room);
					}
                    else if( room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
					if (room.size.x >= minWidth * 2)
					{
						SplitVertically(minWidth, roomQueue, room);
					}
					else if(room.size.y >= minHeight * 2)
					{
						SplitHorizontally(minHeight, roomQueue, room);
					}
					else if (room.size.x >= minWidth && room.size.y >= minHeight)
					{
						roomsList.Add(room);
					}
				}
            }
        }
		return roomsList;
	}

	private static void SplitVertically(int minWidth,  Queue<BoundsInt> roomsQueue, BoundsInt room)
	{
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
	}

	private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
	{
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), 
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
		roomsQueue.Enqueue(room1);
		roomsQueue.Enqueue(room2);
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