using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJoint : MonoBehaviour {

    //Rigidbody2D rb;
    WheelJoint2D wj2;
	// Use this for initialization
	void Start () {
       // rb = GetComponent<Rigidbody2D>();
        wj2 = GetComponent<WheelJoint2D>();
	}
    void OnCollisionEnter2D(Collision2D col)
    {
       
        //wj2.useMotor = false;

        JointMotor2D jm= wj2.motor;
        Debug.Log("speed  1= " + jm.motorSpeed); 
        jm.motorSpeed=-  jm.motorSpeed;
        wj2.motor=jm;
        Debug.Log("speed  2= " + jm.motorSpeed);
    }
}
