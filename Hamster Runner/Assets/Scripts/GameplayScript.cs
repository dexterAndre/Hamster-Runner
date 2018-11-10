using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScript : MonoBehaviour
{
    //Hitbox at bottom. We have to check what this button is, whether it's up or down, and if it is within the hitbox. 
    //Make a curObject that is assigned anew for each instantiated button. 
    //We will then have a if that checks what the curbobject and then check if the player hit correctly and if they hit within the box. 
    //When rotating the buttons, we can have an empty object in the middle that will spin quaternion around itself. 
    //spawn in world and then parent it
    private Collider box;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
