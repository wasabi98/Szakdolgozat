using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject drop;
	[SerializeField] GameObject interLevel;
	[SerializeField] GameObject text;
    [SerializeField] GameObject hearts;
	[SerializeField] GameObject gameOver;
	[SerializeField] GameObject finalScoretext;
	[SerializeField] GameObject scoretext;


	[HideInInspector]
    private List<GameObject> enemies;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0f);
    private int floor = 1;
    private bool exitGiven = false;
	private int score = 0;

	public static string userName = "User1";

    private void Generate()
    {
		enemies = new();
		GameObject.Find("DungeonGenerator").GetComponent<CorridorFirstDungeonGenerator>().GenerateDungeon();
	}
	private void Start()
	{
        Generate();
		interLevel.SetActive(false);
		gameOver.SetActive(false);
	}
	
	public void GenerateEnemies(List<Vector2Int> enemyPositions)
    {
        foreach (var item in enemyPositions)
        {
			Vector3 enemyWorldPosition = GameObject.Find("Grid").GetComponent<Grid>().CellToWorld(new Vector3Int(item.x,item.y ,0));
            GameObject enemyInstance = Instantiate(enemy, enemyWorldPosition + offset, Quaternion.Euler(0, 0, 0));
			enemies.Add(enemyInstance);
		}
            
	}
    public void ClearEnemies()
    {
        foreach(var item in enemies)
            Destroy(item.gameObject);
    }
    public void SetPlayerPosition(Vector2Int pos)
    {
		Vector3 playerPos = GameObject.Find("Grid").GetComponent<Grid>().CellToWorld(new Vector3Int(pos.x, pos.y, 0));
        GameObject.Find("Player").transform.position = playerPos;
	}

	internal void LoadNewLevel()
	{
        floor++;
		text.GetComponent<TextMeshProUGUI>().text = "Floor : " + floor;
		StartCoroutine(InterLevel());
	}
    IEnumerator InterLevel()
    {
		
        if(interLevel != null)
        {
            interLevel.SetActive(true);
            Generate();
            yield return new WaitForSeconds(3);
		    interLevel.SetActive(false);
        }
		yield return null;
	}
    public void EnemyDied(GameObject Enemy)
    {
		
		enemies.Remove(Enemy);
		AddScore(10);
		if (enemies.Count <= 5 && !exitGiven)
		{
            if (enemies.Count <= 1)
            {
                enemies.ElementAt(0).GetComponent<EnemyBehaviour>().drop = drop;

			}
            else
            {
				enemies.ElementAt(UnityEngine.Random.Range(0, enemies.Count - 1)).GetComponent<EnemyBehaviour>().drop = drop;

			}
			exitGiven = true;  
		}
	}


	internal void SetHealth(int health)
	{
		hearts.GetComponent<Heart>().Setup(health);
	}
	internal void UpdateHealth(int healthRemaining)
	{
		hearts.GetComponent<Heart>().SetHealth(healthRemaining);
	}
	internal void GameOver()
	{
		ClearEnemies();
		gameOver.SetActive(true);
		finalScoretext.GetComponent<TextMeshProUGUI>().text = "Final Score: " + score;
	}
	internal void AddScore(int plus)
	{
		score += plus;
		scoretext.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
	}
	public void SendScore()
	{
		GameObject.Find("NetworkModule").GetComponent<NetworkPost>().SendScore(userName,score);
	}
	public void BackToMainMenu()
	{
		SceneManager.LoadSceneAsync(0);
	}
}
