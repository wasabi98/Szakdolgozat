using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
			GameObject.Find("Manager").GetComponent<Manager>().LoadNewLevel();
			Destroy(gameObject);
		}

	}
	
}
