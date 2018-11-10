using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer = 0f;
    public bool canCollide = false;

	void Start ()
    {
        
	}
	
	void Update ()
    {
        transform.rotation = Quaternion.Euler(0, 0, -GetComponentInParent<Transform>().rotation.z);
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            canCollide = true;
        }
	}
}
