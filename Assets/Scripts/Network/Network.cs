using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

public class Network : MonoBehaviour
{
    public void SendScore(string name, int score)
    {
		StartCoroutine(Upload(name, score));
	}
	IEnumerator Upload(string name, int score)
	{
		WWWForm form = new WWWForm();
		/*form.AddField("name", name);
		form.AddField("score", score);
		form.AddField("new", "kalap");*/

		//
		using UnityWebRequest www = UnityWebRequest.Post("https://szakdoga.vigyor.hu/index.php?new=kalap&name=" + name + "&score=" + score, form);
		www.downloadHandler = new DownloadHandlerBuffer();
		yield return www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
		{
			Debug.LogError(www.error);
		}
		else
		{
			Debug.Log(www.downloadHandler.text);
			Debug.Log("Score upload complete!");
		}
	}

}
