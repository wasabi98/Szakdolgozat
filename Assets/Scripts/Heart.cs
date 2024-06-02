using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField]
    GameObject heart;
    private List<GameObject> hearts;
    

	private void Start()
	{
		hearts = new List<GameObject>();
	}

	public void Setup(int numberOfHealth)
    {

        if(hearts.Count > 0)
        {
            foreach(GameObject heart in hearts)
            {
                Destroy(heart);
            }
        }
        Vector3 origin = new(100, 50, 0);
        Vector3 offset = new(50, 0, 0);


        for(int i = 0; i < numberOfHealth; i++)
        {
            hearts.Add(Instantiate(heart, origin + (i-1)* offset, Quaternion.Euler(0,0,0), transform));
        }
    }
    public void SetHealth(int healthRemaining)
    {
        healthRemaining = Mathf.Max(healthRemaining, 0);
        for(int i = 0; i < hearts.Count; i++)
        {
            if(i <= healthRemaining)
            {
                hearts.ElementAt(i).SetActive(true);
            }
            else
            {
				hearts.ElementAt(i).SetActive(false);
			}
        }
    }
}
