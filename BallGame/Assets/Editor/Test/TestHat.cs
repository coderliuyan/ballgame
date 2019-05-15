using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHat : MonoBehaviour {

    Rigidbody2D rb;

    public float speed = 10;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
