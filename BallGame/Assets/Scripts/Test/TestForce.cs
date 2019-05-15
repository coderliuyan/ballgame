using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForce : MonoBehaviour {

    Rigidbody2D rb;
    TestArm testArm;

    float foreceFX = 0;
    float foreceFY = 0;
    float foreceBX = 0;
    float foreceBY = 0;

    bool isCDState = false;            //连续跳跃cd
    float isCDTime = 0.5f;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector3(0, StaticData.g_massCenter, 0);
        rb.mass = StaticData.g_bodyMassNum;
        rb.inertia = StaticData.g_bodyInertia;
        rb.angularDrag = StaticData.g_bodyDrag;

        testArm = transform.Find("ccc_gebo1").GetComponent<TestArm>();

        foreceFX = StaticData.g_forceFrontNum;
        foreceFY = StaticData.g_forceUpFNum;
        foreceBX = StaticData.g_forceBackNum;
        foreceBY = StaticData.g_forceUpBNum;

        isCDTime = StaticData.g_cdTime;
	}
    public void OnForceType1()
    {
        testArm.OnFrontClick();
        if(isCDState)
        {
            return;
        }
        isCDState = true;
        Invoke("ResumeState", isCDTime);
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(foreceFX, foreceFY), ForceMode2D.Impulse);
      
    }
    public void OnForceType2()
    {
        testArm.OnBackClick();
        if (isCDState)
        {
            return;
        }
        isCDState = true;
        rb.velocity = Vector2.zero;
        Invoke("ResumeState", isCDTime);
        rb.AddForce(new Vector2(-foreceBX, foreceBY), ForceMode2D.Impulse);
       
    }
    void ResumeState()
    {
        isCDState = false;
    }
}
