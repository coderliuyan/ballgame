using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 联赛模式 选择比赛模式 
/// 俱乐部 超级联赛 世界杯
/// </summary>
public class MatchChooseUI : MonoBehaviour 
{
    public GameObject grayState2;
    public GameObject grayState3;
    private GameObject tempGo;

    public Image[] stars1;
    public Image[] stars2;
    public Image[] stars3;

    public Material grayMat;


	void Awake () 
    {

    }
    void Start()
    {
        InitMatchData();
    }
    public void SetState(bool _state)
    {
        gameObject.SetActive(_state);
        if(_state)
        {
            InitMatchData();
        }
    }
    void InitMatchData()
    {
        InitRoleMsg();
    }
    void InitRoleMsg()
    {
        //赛事 奖杯状态
        int level=SaveData.TeamMatchLevel[StaticData.TeamSkin1];
       
        if(level<1)
        {
            grayState2.SetActive(true);
            grayState3.SetActive(true);

            GameObject flash2 = transform.Find("btn_type2/flash").gameObject;
            flash2.SetActive(false);
            GameObject flash3 = transform.Find("btn_type3/flash").gameObject;
            flash3.SetActive(false);

        }
        else if(level<2)
        {
            
            grayState3.SetActive(true);

            GameObject flash3 = transform.Find("btn_type3/flash").gameObject;
            flash3.SetActive(false);

            if (StaticData.g_newMatchTeam == StaticData.TeamSkin1)
            {
                grayState2.SetActive(true);

                GameObject lockGo = grayState2.transform.Find("lock").gameObject;
                lockGo.SetActive(false);
                TipEffectMng.GetInstance().ShowActionEffect(1, grayState2.transform, Vector3.zero);
                tempGo = grayState2;
                Invoke("HideGray", 1);
                AudioManager.Instance.PlayEffectAudio(8, transform);
            }
            else
            {
                grayState2.SetActive(false);
            }

        }
        else
        {
            grayState2.SetActive(false);
         

            if (StaticData.g_newMatchTeam == StaticData.TeamSkin1)
            {
                grayState3.SetActive(true);

                GameObject lockGo = grayState3.transform.Find("lock").gameObject;
                lockGo.SetActive(false);
                TipEffectMng.GetInstance().ShowActionEffect(1, grayState3.transform, Vector3.zero);
                tempGo = grayState3;
                Invoke("HideGray", 1);
            }
            else
            {
                grayState3.SetActive(false);
            }
        }
        SetStars();
    }
    void HideGray()
    {
        tempGo.SetActive(false);
    }
    void SetStars()
    {
        for (int i = 0; i < 3; i++)
        {
            if (SaveData.TeamMatchLevel[StaticData.TeamSkin1] > 0)
            {
                stars1[i].sprite = MyTools.LoadSprite("Prefabs/match/im_start2");
            }
            else
            {
                stars1[i].sprite = MyTools.LoadSprite("Prefabs/match/im_start1");
            }

            if (SaveData.TeamMatchLevel[StaticData.TeamSkin1] > 1)
            {
                stars2[i].sprite = MyTools.LoadSprite("Prefabs/match/im_start2");
            }
            else
            {
                stars2[i].sprite = MyTools.LoadSprite("Prefabs/match/im_start1");
            }

            if (SaveData.TeamMatchLevel[StaticData.TeamSkin1] > 2)
            {
                stars3[i].sprite = MyTools.LoadSprite("Prefabs/match/im_start2");
            }
            else
            {
                stars3[i].sprite = MyTools.LoadSprite("Prefabs/match/im_start1");
            }
        }
    }
    public void OnReturn()
    {
        SetState(false);
        MainManager.GetInstance().SetMainState(true);
        AudioManager.Instance.PlayEffectAudio(0, transform);
    }
    public void OnMathchChoose(int _id)
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (SaveData.TeamMatchLevel[StaticData.TeamSkin1]>=_id)
        {
            StaticData.g_matchType = (byte)_id;
            //生成随机ai角色
            int[] numArray = new int[StaticData.TEAM_NUMS];
            for (int i = 0; i < StaticData.TEAM_NUMS;i++ )
            {
                if(i==StaticData.TeamSkin1)
                {
                    numArray[i] = 0;
                }
                else
                {
                    numArray[i] = i;
                }             
            }
            int randNum=0;
            int count=0;
            while(true)
            {
                randNum=Random.Range (0,StaticData.TEAM_NUMS);
                if(numArray[ randNum]>0)
                {
                    StaticData.g_aiSkins[count] = (byte)numArray[randNum];
                    numArray[randNum]=0;
                    count++;
                    if(count==StaticData.MATCH_TIMES[_id])
                    {
                        break;
                    }
                }
            }
//             for (int i = 0; i < StaticData.MATCH_TIMES[_id];i++ )
//             {
//                 Debug.Log("id=" + StaticData.g_aiSkins[i]);
//             }
//             Debug.Log("--------------------------");

            StaticData.g_newMatchTeam = 100;
            SceneManager.LoadScene(StaticData.SCENENAME_READY);
        }      
    }

}
