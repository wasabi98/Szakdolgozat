using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class RandomWalkGenerator  : MonoBehaviour 
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool randomEachTime = true;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        foreach (var position in floorPositions) 
        {
            Debug.Log(position);
        }
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++) 
        {
            var path = ProceduralGenerationAlgorithms.RandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path);
            if(randomEachTime)
            {
                currentPosition = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
            }

        }
        return floorPositions;
    }
}
