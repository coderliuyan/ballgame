using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色胳膊控制
/// </summary>
public class ArmControl : MonoBehaviour
{
    public Transform BallCenter;
    //public Transform MassCenter;
    Rigidbody2D rb;
    public byte type = 0;
    
    public Vector3 ballPos;       //持球位置

    //public float inertiaNum = 0;
    //private byte actionType = 0; //动作状态 0待机 1向左 2向右 3持球
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();

        rb.inertia = StaticData.g_armInertia;
        rb.angularDrag = StaticData.g_armDrag;
        //rb.centerOfMass = BallCenter.localPosition;
        rb.mass = StaticData.g_armMassNum;
        rb.angularVelocity = 0;
        ballPos = BallCenter.localPosition;
	}
    public void OnFrontClick()
    {
        //rb.AddTorque(1000);
        //rb.angularVelocity = StaticData.g_angularNum;
      //  rb.angularVelocity = 800;
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
        if(type<4)
        {
            rb.AddTorque(-StaticData.g_angularNum, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddTorque(StaticData.g_angularNum, ForceMode2D.Impulse);
        }
       
    }
    public void OnBackClick()
    {
        //rb.angularVelocity = -StaticData.g_angularNum;
        //rb.AddTorque(-1000);
       // rb.angularVelocity = -800;
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
        if (type < 4)
        {
            rb.AddTorque(StaticData.g_angularNum, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddTorque(-StaticData.g_angularNum, ForceMode2D.Impulse);
        }
    }
    public void SetVelocityFlip()
    {
        rb.angularVelocity = -rb.angularVelocity;
    }
}
