using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GetPrize : MonoBehaviour {

    public RoleControl[] ownCc;       //队伍 角色控制
    public Image im_cup;
    public Image im_title;
    public Text T_coin;

    public SpriteRenderer handCup;
    //ai 简单机制
    float delayTime = 1;             //AI延迟反应时间
    float isCDTime = 1.0f;           //AI跳跃时间间隔
	void Start () 
    {
        StaticData.g_gameStart = true;
        Time.timeScale = 1;
        AudioManager.Instance.PlayBgAudio(4);

        InvokeRepeating("ActionState", delayTime, isCDTime);


        //杯赛模式最终胜利  20 50  100
        SaveData.CoinNum += StaticData.MATCH_PRIZES[StaticData.g_matchType];
        SaveData.SaveCoinData();
        T_coin.text = StaticData.MATCH_PRIZES[StaticData.g_matchType].ToString();

        im_title.sprite = MyTools.LoadSprite(string.Format("Prefabs/match/im_matchTitle{0}", StaticData.g_matchType + 1));
        im_cup.sprite = MyTools.LoadSprite(string.Format("Prefabs/match/im_prize{0}", StaticData.g_matchType + 1));
        handCup.sprite = MyTools.LoadSprite(string.Format("Prefabs/match/im_prize{0}", StaticData.g_matchType + 1));
	}

    void ActionState()
    {
        for (int i = 0; i < 3; i++)
        {
            ownCc[i].SetFreezeState(false);
        }
        if (JudgeDir())
        {
            TeamFront();
        }
        else
        {
            TeamBack();
        }
       
    }
    bool JudgeDir()
    {
        int randNum1=Random.Range(0,100);
        if(randNum1>50)
        {
           return false;
        }
         return true;
    }
    public void TeamFront()
    {
        for (int i = 0; i < 3; i++)
        {
            ownCc[i].OnFrontClick();
        }
    }
    public void TeamBack()
    {
        for (int i = 0; i < 3; i++)
        {
            ownCc[i].OnBackClick();
        }
    }
    public void OnReturn()
    {
        AudioManager.Instance.PlayEffectAudio(0,transform);
        SceneManager.LoadScene(StaticData.SCENENAME_MAIN);
    }
}
