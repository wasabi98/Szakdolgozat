using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI UserText;

	public void Play()
	{
		StartCoroutine(LoadLevel());
	}
	public void Quit()
	{
		Application.Quit();
	}
	IEnumerator LoadLevel()
	{
		Manager.userName = UserText.text;
		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(1);
		while (!loadOperation.isDone) 
		{
			yield return null;
		}
	}
}