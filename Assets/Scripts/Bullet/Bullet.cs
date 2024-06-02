using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	public int damage = 1;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		CharacterBody ch = collision.gameObject.GetComponent<CharacterBody>();
		if (ch != null)
			ch.Damage(damage);

		Destroy(gameObject);
	}
	
}
