using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArm : MonoBehaviour {
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = StaticData.g_armMassNum;
        rb.inertia = StaticData.g_armInertia;
        rb.angularDrag = StaticData.g_armDrag;
	}
    public void OnFrontClick()
    {
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
 
        rb.AddTorque(-StaticData.g_angularNum, ForceMode2D.Impulse);    
    }
    public void OnBackClick()
    {
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
      
        rb.AddTorque(StaticData.g_angularNum, ForceMode2D.Impulse);
      
    }
}
