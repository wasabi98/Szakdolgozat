using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkParameters",menuName = "RandomWalkData")]
public class RandomWalkData : ScriptableObject
{
	public int iterations = 10, walkLength = 10, enemyCount = 5;
	public bool startRandomlyEachIteration = true;
}
