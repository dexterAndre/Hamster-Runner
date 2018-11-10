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
    private GameObject wheelMid;
    private float rot = 0;
    [Header("Angle each time")]
    public float rotAngle = 12;

    private float roundTimer;
    private float subTimer;
    private Transform instTransform;
    public GameObject newButton;

    public Sprite up;
    public Sprite down;
    public GameObject curObject;

	void Start ()
    {
        box = GameObject.Find("HitBox").GetComponent<BoxCollider>();
        //instTransform.position = new Vector3(0, -1, -1);
        //instTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        wheelMid = GameObject.Find("SwingMeRoundBabe");
	}

    void Update()
    {
        roundTimer += Time.deltaTime;
        subTimer += Time.deltaTime;
        rot -= rotAngle * Time.deltaTime;
        
        if(rot <= 360)
        {
            rot += 360;
        }

        transform.rotation = Quaternion.Euler(0, 0, rot);

        if(roundTimer >= 7)
        {
            CreateButton();
            roundTimer = 0;

        }
        if (subTimer >= 1f)
        {
            CreateButton();
            subTimer = 0;
        }

    }
    //spawning of buttons
    private GameObject CreateButton()
    {
        //We need a way to set a random button and assign it a correct tag accordingly. 
        curObject = Instantiate(newButton);
        float newnumber = Random.Range(0, 2);
        //Sprite cursprite = curObject.GetComponent<SpriteRenderer>().sprite;
        if (newnumber == 0)
        {
            curObject.GetComponent<SpriteRenderer>().sprite = up;
            curObject.tag = "Up";
        }
        else if(newnumber == 1)
        {
            curObject.GetComponent<SpriteRenderer>().sprite = down;
            curObject.tag = "Down";
        }
        else
        {
            Debug.Log("No current spritenumber");
        }
        curObject.transform.SetParent(wheelMid.transform, true);
        curObject.transform.position = new Vector3(-0.2f, -1, -1) + wheelMid.transform.position;
        return curObject;
    }
}
