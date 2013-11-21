using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchHero : MonoBehaviour 
{	
	public GameObject hero;
    public List<Material> heroes;
    
    private Texture texture;
    private int count = 0;

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{   
            hero.renderer.material = heroes[count];
            count++;

            if (count >= heroes.Count) 
            {
                count = 0;
            }
		} 
	}
}
