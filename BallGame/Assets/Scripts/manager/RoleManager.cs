using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化角色皮肤 数值信息
/// </summary>
public class RoleManager : MonoBehaviour
{
    public RoleControl[] ownCc;       //队伍1 角色控制
    public RoleControl[] enemyCc;     //队伍2 角色控制
    public BallControl ballCc;        //球 控制


    public SignDelete sign;           //发球效果
    public Animator signAnim;

    public SpriteRenderer nameSpr1;
    public SpriteRenderer nameSpr2;


    //ai 简单机制
    float delayTime = 6;             //AI延迟反应时间
    float isCDTime = 1.0f;           //AI跳跃时间间隔

    [HideInInspector]
    public bool isCatchBall = false;

    void Start()
    {
        nameSpr1.sprite = MyTools.LoadSprite(string.Format("Prefabs/teamName/im_name{0}", StaticData.TeamSkin1 + 1));
        nameSpr2.sprite = MyTools.LoadSprite(string.Format("Prefabs/teamName/im_name{0}", StaticData.TeamSkin2 + 1));
        if(StaticData.g_gameMode!=3)
        {
            InvokeRepeating("ActionState", delayTime, isCDTime);
        }
        StaticData.g_gameStart = false;
    }
    void ActionState()
    {
        if(StaticData.g_gameEnd)
        {
            return;
        }
        if (isCatchBall)
        {
            if(JudgeDir())
            {
                TeamFront2();
            }
            else
            {
                DorpBall(1);
            }
        }
        else
        {
            if (JudgeDir())
            {
                TeamFront2();
            }
            else
            {
                TeamBack2();
            }
        }
    }
    bool JudgeDir()
    {
        float posX = 0;
        for(int i=0;i<3;i++)
        {
            posX += enemyCc[i].transform.position.x - ballCc.transform.position.x;
        }
        if(posX>0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    /// <summary>
    /// 触发发球动画
    /// </summary>
    public void FreeBall()
    {
        signAnim.enabled=true;
        ballCc.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("DelayFree",0.5f);
    }
    public void DelayFree()
    {
        ballCc.GetComponent<Rigidbody2D>().gravityScale=1;
        
        sign.enabled = true;
        StaticData.g_gameStart = true;

        for (int i = 0; i < 3; i++)
        {
            ownCc[i].SetFreezeState(false);
            enemyCc[i].SetFreezeState(false);
        }
    }

    public void TeamFront1()
    {
        for (int i = 0; i < 3; i++)
        {
            ownCc[i].OnFrontClick();
        }
    }
    public void TeamBack1()
    {
        for (int i = 0; i < 3; i++)
        {
            ownCc[i].OnBackClick();
        }
    }
    public void TeamFront2()
    {
        for (int i = 0; i < 3; i++)
        {
            enemyCc[i].OnFrontClick();
        }
    }
    public void TeamBack2()
    {
        for (int i = 0; i < 3; i++)
        {
            enemyCc[i].OnBackClick();
        }
    }
    public void DorpBall(byte _id)
    {
        ballCc.OnDrop(_id);
    }
    public void SetArmFlip(int _type)
    {
        if(_type<4)
        {
            ownCc[_type - 1].SetArmFlip();
        }
        else
        {
            enemyCc[_type - 4].SetArmFlip();
        }

    }
}
