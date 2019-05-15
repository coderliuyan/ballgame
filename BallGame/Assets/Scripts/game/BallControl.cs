using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 橄榄球控制
/// </summary>
public class BallControl : MonoBehaviour 
{
    private const byte AreaX = 9;     //进球区域边界值

    Rigidbody2D rb;
    FixedJoint2D fixedJoint;
  
    private int type = 0;         //持球者id
    private bool isNew = false;
    //private Transform armTr;


    public bool isCatched = false;// 球被抓住
    bool isCatchEnabled = true;

    public PhysicsMaterial2D ballMat;

	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = StaticData.g_ballMassNum;

        transform.localRotation=Quaternion.Euler(new Vector3(0,0,Random.Range(0,360)));
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if(StaticData.g_gameEnd)
        {
            return;
        }
        // Debug.Log("layer =" + col.gameObject.layer);
        if (col.gameObject.layer == (int)LayerMask.OWNARM)
        {
            if (isCatchEnabled != true)
            {
                return;
            }
            //Debug.Log("OnCollisionEnter2D  layer =" + col.gameObject.layer);

            if(rb!=null)
            {
                Destroy(rb);
            }
            ArmControl tt = col.gameObject.GetComponent<ArmControl>();

            //Debug.Log("type " + type + "  :" + tt.type);
            isNew = false;
            if(type==0||type>3)
            {
                isNew = true;
            }
            if (isNew)
            {
                if (type > 3)
                {
                    GameManager.GetInstance().SetCatchState(type, false);
                }

                isCatched = true;
                type = tt.type;

               // armTr = col.transform;
//
                transform.SetParent(col.transform);
                transform.localPosition = tt.ballPos;
                transform.localRotation = Quaternion.identity;

                if(type>3)
                {
                    GameManager.GetInstance().SetCatchState(type, true);
                }

            }
        }
        else if (col.gameObject.layer == (int)LayerMask.ENEMYARM)
        {
            if (isCatchEnabled != true)
            {
                return;
            }
            //Debug.Log("OnCollisionEnter2D  layer =" + col.gameObject.layer);

            if (rb != null)
            {
                Destroy(rb);
            }
            ArmControl tt = col.gameObject.GetComponent<ArmControl>();

            //Debug.Log("type " + type + "  :" + tt.type);
            isNew = false;
            if (type == 0 || type < 4)
            {
                isNew = true;
            }
            if (isNew)
            {
                if (type > 3)
                {
                    GameManager.GetInstance().SetCatchState(type, false);
                }

                isCatched = true;
                type = tt.type;

               // armTr = col.transform;

                transform.SetParent(col.transform);
                transform.localPosition = tt.ballPos;
                transform.localRotation = Quaternion.identity;

                if (type > 3)
                {
                    GameManager.GetInstance().SetCatchState(type, true);
                }
            }
           }
        else if (col.gameObject.layer == (int)LayerMask.FLOOR)
        {
            AudioManager.Instance.PlayEffectAudio(3, transform);
            if(isCatched)
            {
                GameManager.GetInstance().SetRoleArmFlip(type);
            }
            if (rb != null && rb.velocity.magnitude < 4)
            {
                rb.velocity = Vector2.zero;
            }

            //Build Test
            //return;
            if (type > 3 && isCatched && transform.position.x < -AreaX)
            {
                Debug.Log("Game Fail");
                Time.timeScale = 0.3f;
                GameManager.GetInstance().SetGameResult(false);
            }
            else if (type < 4 && isCatched && transform.position.x > AreaX)
            {
                 Debug.Log("Game Win");
                Time.timeScale = 0.3f;
                GameManager.GetInstance().SetGameResult(true);
            }
        }
        else if(col.gameObject.layer == (int)LayerMask.WALL)
        {
            AudioManager.Instance.PlayEffectAudio(3, transform);
            if (isCatched)
            {
                GameManager.GetInstance().SetRoleArmFlip(type);
            }
        }
    }
    public void OnDrop(int id)
    {
        if(isCatched==false)
        {
            return;
        }

        if (type < 4)
        {
            if (id==0)
            {
                transform.SetParent(null);
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.sharedMaterial = ballMat;
                rb.gravityScale = 1;
                rb.mass = StaticData.g_ballMassNum;
                //Debug.Log("vec=" + vec);
                rb.velocity = Vector2.right * StaticData.g_velocityNum;

                isCatchEnabled = false;
                Invoke("OpenCatch", 0.5f);

                type = 0;
                isCatched = false;
            }                       
        }
        else 
        {
            if(id==1)
            {
                GameManager.GetInstance().SetCatchState(type, false);

                transform.SetParent(null);
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.sharedMaterial = ballMat;
                rb.gravityScale = 1;
                rb.mass = StaticData.g_ballMassNum;
                //Debug.Log("vec=" + vec);
                rb.velocity = Vector2.left * StaticData.g_velocityNum;

                isCatchEnabled = false;
                Invoke("OpenCatch", 0.5f);

                type = 0;
                isCatched = false;
            }
        }   
    }
    void OpenCatch()
    {
        isCatchEnabled = true;
    }
}
