using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
		manager.ClearEnemies();
        manager.SetPlayerPosition(startPosition);
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

	protected abstract void RunProceduralGeneration();
}
