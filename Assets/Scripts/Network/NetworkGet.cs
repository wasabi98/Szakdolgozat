using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGet : MonoBehaviour
{
	public delegate void ListDelegate(List<HighscoreElement> list);

	public List<HighscoreElement> list = new();

	public void GetAll(ListDelegate UpdateList)
	{
		StartCoroutine(Download(UpdateList));
		
	}
	 IEnumerator Download(ListDelegate UpdateList)
	{
		using (UnityWebRequest www = UnityWebRequest.Get("https://szakdoga.vigyor.hu/index.php?allarray=kalap"))
		{
			yield return www.SendWebRequest();

			if (www.result == UnityWebRequest.Result.Success)
			{
				Debug.Log("Get success");
				HighscoreElement[] data = JsonConvert.DeserializeObject<HighscoreElement[]>(www.downloadHandler.text);
				
				list = new List<HighscoreElement>(data);
				UpdateList(list);
			}
			else
			{
				Debug.Log("Error: " + www.error);
			}
		} 	
	}
}
