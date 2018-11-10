using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool canCollide;
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
	}
    //We don't want to be destroyed before it has gone a whole round, check that. 
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        canCollide = other.gameObject.GetComponent<Timer>().canCollide;
//        if (canCollide)
//        {
//            Debug.Log("canCOLLIDEUP");
//            if (other.CompareTag("Up") && Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
//            {
//                Debug.Log("Pressed up");
//                Destroy(other.gameObject);
//            }
//            else if (other.CompareTag("Down"))
//            {
//                Destroy(other.gameObject);
//            }
//        }
//    }

    private void OnTriggerStay2D(Collider2D other)
    {
        canCollide = other.gameObject.GetComponent<Timer>().canCollide;
        if (canCollide)
        {
            Debug.Log("canCOLLIDEDOWN");
            if (other.CompareTag("Up") && Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) //Add input here to check
            {
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Down") && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canCollide = collision.gameObject.GetComponent<Timer>().canCollide;
        if (canCollide)
        {
            if (collision.CompareTag("Up"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("Down"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
