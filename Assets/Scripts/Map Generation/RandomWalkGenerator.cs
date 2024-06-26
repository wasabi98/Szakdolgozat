using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class RandomWalkGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected RandomWalkData randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkData parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.RandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path);
            if (parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
            }

        }
        if(!floorPositions.Contains(startPosition))
        { 
            List<Vector2Int> enemyPos = floorPositions.ToList();
			GameObject.Find("Manager").GetComponent<Manager>().GenerateEnemies(enemyPos.OrderBy(x => UnityEngine.Random.Range(0, enemyPos.Count)).Take(parameters.enemyCount).ToList());
		}
		return floorPositions;
    }

	
}
