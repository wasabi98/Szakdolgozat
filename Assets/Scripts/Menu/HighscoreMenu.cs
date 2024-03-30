using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HighscoreMenu : MonoBehaviour
{

	List<HighscoreElement> list;
	[SerializeField]
	public NetworkGet network;
	[SerializeField]
	public GameObject highscoreListElement;

	
	public NetworkGet.ListDelegate updatelist;

	public void Refresh()
    {
		ClearTable();
		list = new();

		updatelist = UpdateList;
		network.GetAll(updatelist);

	}
	private void UpdateList(List<HighscoreElement> list)
	{
		Debug.Log(list.Count);
		foreach (var element in list)
		{
			GameObject highscore = Instantiate(highscoreListElement, GameObject.Find("Content").transform);
			
			highscore.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = element.name;
			highscore.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = element.score;
		}
	}
	private void ClearTable()
	{
		GameObject content = GameObject.Find("Content");
		for (var i = content.transform.childCount - 1; i >= 0; i--)
		{
			Destroy(content.transform.GetChild(i).gameObject);
		}
	}
	
}
