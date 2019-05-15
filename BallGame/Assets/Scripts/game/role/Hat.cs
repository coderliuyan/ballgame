using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 帽子掉落控制
/// </summary>
public class Hat : MonoBehaviour {

	// Use this for initialization
    Rigidbody2D rb;
   // FixedJoint2D fj;
    bool inHead = true;  //是否掉落
	void Start () 
    {
       // fj = GetComponent<FixedJoint2D>();
       // fj.breakForce = StaticData.g_breakForce;
      //  fj.breakTorque = StaticData.g_breakTorque;

      //  rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(new Vector2(StaticData.g_addForce, 0));

      
	}
    /// <summary>
    /// 碰撞 检测 击中帽子掉落条件
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if(inHead&&col.gameObject.layer==(int)LayerMask.BALL)
        {
            Rigidbody2D rb2 = col.gameObject.GetComponent<Rigidbody2D>();
            if(rb2.velocity.magnitude>StaticData.g_hatSpeed)
            {
                inHead = false;

                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.mass = StaticData.g_headMassNum;
                rb.gravityScale = 1;



                transform.SetParent(null);
            }
        }
    }
//     void  OnJointBreak2D(Joint2D brokenJoint)
//     {
//         Debug.Log("OnJointBreak " + brokenJoint.reactionForce +","+ brokenJoint.reactionTorque);
//         rb.gravityScale = 1;
// 
//         transform.SetParent(GameObject.Find("RoleManager").transform);
//     }
}
