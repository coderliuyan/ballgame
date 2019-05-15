using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色身体控制
/// </summary>
public class RoleControl : MonoBehaviour
{
    private float forceFront = 0;        //前进方向力大小
    private float forceBack= 0;          //后退方向力大小
    private float forceUpF = 0;          //垂直方向力大小
    private float forceUpB = 0;          //垂直方向力大小

    Rigidbody2D rb;
  
    byte isJumpState = 0;              // 角色状态 0落地 1向前跳 2向后跳
    bool isCDState = false;            //连续跳跃cd
    float isCDTime = 0.5f;
    bool isUseBack = false;            //切换状态

    public ArmControl armCc;

    [HideInInspector]
    public byte type = 0;              //角色类型
  void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        type = armCc.type;
        SetSkin(); 

        //Build Test
        SetFreezeState(true);

        rb.mass = StaticData.g_bodyMassNum;
        rb.centerOfMass = new Vector3(0,StaticData.g_massCenter,0);
        rb.inertia = StaticData.g_bodyInertia;
        rb.drag = 0;
        rb.angularDrag = StaticData.g_bodyDrag;
        
        if(type<4)
        {
            forceFront = StaticData.Force_FX[type-1];
            forceBack = StaticData.Force_BX[type - 1];
            forceUpF = StaticData.Force_FY[type - 1];
            forceUpB = StaticData.Force_BY[type - 1];
        }
        else
        {
            forceFront = StaticData.Force_FX[type - 4];
            forceBack = StaticData.Force_BX[type - 4];
            forceUpF = StaticData.Force_FY[type - 4];
            forceUpB = StaticData.Force_BY[type - 4];
        }
  
        isCDTime = StaticData.g_cdTime;
    }
    public void SetFreezeState(bool _state)
    {
        rb.freezeRotation = _state;
        if(_state)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }     
    }
    public void OnFrontClick()
    {
        if(gameObject.activeSelf!=true||this.enabled!=true)
        {
            return;   
        }
        if (isJumpState == 1)
        {
            return;
        }
        else if(isJumpState==2)
        {
            if (isUseBack)
            {
                return;
            }
            else
            {
                isUseBack = true;
                armCc.OnFrontClick();
                if (!StaticData.g_gameStart)
                {
                    return;
                }
                rb.velocity = Vector2.zero;
                if (type < 4)
                {
                    rb.AddForce(new Vector2(forceFront, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(-forceFront, 0), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            if (isCDState)
            {
                return;
            }
            isCDState = true;
            Invoke("ResumeState", isCDTime);

            armCc.OnFrontClick();
            if (!StaticData.g_gameStart)
            {
                return;
            }
            rb.velocity = Vector2.zero;
            if (type < 4)
            {
                rb.AddForce(new Vector2(forceFront, forceUpF), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-forceFront, forceUpF), ForceMode2D.Impulse);
            }
        }
       
        isJumpState = 1;
    }
    public void OnBackClick()
    {
        if (gameObject.activeSelf != true || this.enabled != true)
        {
            return;
        }
        if (isJumpState == 2)
        {
            return;
        }
        else if (isJumpState == 1)
        {
            if(isUseBack)
            {
                return;
            }
            else
            {
                isUseBack = true;
                armCc.OnBackClick();
                if (!StaticData.g_gameStart)
                {
                    return;
                }
                rb.velocity = Vector2.zero;
                if (type < 4)
                {
                    rb.AddForce(new Vector2(-forceBack, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(forceBack, 0), ForceMode2D.Impulse);
                }   
            }
        }
        else
        {
            if (isCDState)
            {
                return;
            }
            isCDState = true;
            Invoke("ResumeState", isCDTime);

            armCc.OnBackClick();
            if (!StaticData.g_gameStart)
            {
                return;
            }
            rb.velocity = Vector2.zero;
            if (type < 4)
            {
                rb.AddForce(new Vector2(-forceBack, forceUpB), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(forceBack, forceUpB), ForceMode2D.Impulse);
            }   
        }
        isJumpState = 2;
    }
    void ResumeJumpState()
    {
        isJumpState = 0;
    }
    void ResumeState()
    {
        isCDState = false;
        isUseBack = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer== (int)(LayerMask.OWNBODY)||
            col.gameObject.layer== (int)(LayerMask.ENEMYBODY)||
            col.gameObject.layer== (int)(LayerMask.FLOOR))
        {
            ResumeJumpState();    
        }
        
    }  
    void SetSkin()
    {
           int skinId=0;
        if(type<4)
        {
            skinId = (StaticData.TeamSkin1+1) * 100 + type;
        }
        else
        {
            skinId = (StaticData.TeamSkin2 + 1) * 100 + type - 3;
        }

        SpriteRenderer body = GetComponent<SpriteRenderer>();
        body.sprite = MyTools.LoadSprite(string.Format( "Prefabs/role/role{0}_2",skinId));

        SpriteRenderer head = transform.Find("head").GetComponent<SpriteRenderer>();
        head.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", skinId));

        SpriteRenderer arm = transform.Find("arm").GetComponent<SpriteRenderer>();
        arm.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", skinId));
    }
    /// <summary>
    /// 角色 抓住球时 碰到 地面和墙壁 胳膊角度反转
    /// </summary>
    public void SetArmFlip()
    {
        armCc.SetVelocityFlip();
    }
}