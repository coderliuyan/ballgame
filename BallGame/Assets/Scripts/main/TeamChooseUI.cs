using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 选择队伍 解锁队伍 ( 联赛模式/挑战模式 )
/// </summary>
public class TeamChooseUI : MonoBehaviour 
{
    public Text coinNum;   //金币显示

    public Image lockIm;         //未解锁显示
    public Button startBtn;      //开始按钮

    //角色信息
    public Image badgeIm;
    public Text nameText;
    public Image[] prizeIm;
    public Text msgText;
    public Image[] teamRole;

    public GameObject leftBtn;
    public GameObject rightBtn;

    public Material grayMat;
    private bool unlockState=false;

    TableValue teamData;
    void Awake()
    {
        teamData = XmlHelper.Instance.ReadFile("TeamMsg");
    }
	void Start () 
    {
		
	}
    public void SetState(bool _state)
    {
        gameObject.SetActive(_state);
        if (_state)
        {
            InitTeamData();
        }
    }
    void InitTeamData()
    {
        UpdateCoinShow();

        InitRoleMsg();
    }
    void InitRoleMsg()
    {
        //赛事 奖杯状态
        int level = SaveData.TeamMatchLevel[StaticData.TeamSkin1];
        for (int i = 0; i < 3; i++)
        {
            if (i < level)
            {
                prizeIm[i].gameObject.SetActive(true);
            }
            else
            {
                prizeIm[i].gameObject.SetActive(false);
            }
        }
        //角色解锁状态
        unlockState = SaveData.TeamUnlockState[StaticData.TeamSkin1];
        lockIm.gameObject.SetActive(!unlockState);
        startBtn.gameObject.SetActive(unlockState);

        string nameStr = (StaticData.TeamSkin1 + 1).ToString().PadLeft(2, '0');

        //角色信息
     
        badgeIm.sprite = MyTools.LoadSprite(string.Format("Prefabs/Badge/im_badge{0}", StaticData.TeamSkin1 + 1));
        nameText.text = teamData.GetString(nameStr, "name");
        msgText.text = teamData.GetString(nameStr,"introduce");

        SetRole();

        if(StaticData.TeamSkin1==0)
        {
            leftBtn.SetActive(false);
        }
        else
        {
            leftBtn.SetActive(true);
        }
        if(StaticData.TeamSkin1==StaticData.TEAM_NUMS-1)
        {
            rightBtn.SetActive(false);
        }
        else
        {
            rightBtn.SetActive(true);
        }
    }

    public void OnReturn()
    {
        SetState(false);

        if(!SaveData.TeamUnlockState[ StaticData.TeamSkin1])
        {
            StaticData.TeamSkin1 = 0;
        }
        MainManager.GetInstance().SetMainState(true);
        AudioManager.Instance.PlayEffectAudio(0, transform);
    }
    public void OnStart()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.g_gameMode == 2)
        {
            SceneManager.LoadScene(StaticData.SCENENAME_CHALLENGE);
        }
        else if (StaticData.g_gameMode == 1)
        {
            SetState(false);
            MainManager.GetInstance().SetMatchChooseState(true);
        }
      
    }
    public void OnLeft()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.TeamSkin1 > 0)
        {
            StaticData.TeamSkin1--;
            InitRoleMsg();
        }
    }
    public void OnRight()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.TeamSkin1 < StaticData.TEAM_NUMS - 1)
        {
            StaticData.TeamSkin1++;
            InitRoleMsg();
        }
    }
    public void OnUnlock()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        int costNum = 100;
        if (SaveData.CoinNum >= costNum)
        {
            SaveData.CoinNum -= costNum;
            SaveData.SaveCoinData();

            UpdateCoinShow();

            SaveData.TeamUnlockState[StaticData.TeamSkin1] = true;
            SaveData.SaveTeamData(StaticData.TeamSkin1);
       

            InitRoleMsg();
            TipEffectMng.GetInstance().ShowTipMsg(3, transform);
            TipEffectMng.GetInstance().ShowActionEffect(1, transform, new Vector3(-240, -15, 0));
            AudioManager.Instance.PlayEffectAudio(8, transform);
        }
        else
        {
            TipEffectMng.GetInstance().ShowTipMsg(2,transform);
            //Debug.Log("金币不足！！!");
            AudioManager.Instance.PlayEffectAudio(1, transform);
        }
    }
    public void OnShop()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        MainManager.GetInstance().SetShopState(2, true);
    }
    public void UpdateCoinShow()
    {
        coinNum.text = SaveData.CoinNum.ToString();
    }
    void SetRole()
    {
        int roleId = (StaticData.TeamSkin1 + 1) * 100 + 1;
        teamRole[0].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head1 = teamRole[0].transform.Find("head").GetComponent<Image>();
        head1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm1 = teamRole[0].transform.Find("arm").GetComponent<Image>();
        arm1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        roleId = (StaticData.TeamSkin1 + 1) * 100 + 2;
        teamRole[1].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head2 = teamRole[1].transform.Find("head").GetComponent<Image>();
        head2.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm2 = teamRole[1].transform.Find("arm").GetComponent<Image>();
        arm2.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        roleId = (StaticData.TeamSkin1 + 1) * 100 + 3;
        teamRole[2].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head3 = teamRole[2].transform.Find("head").GetComponent<Image>();
        head3.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm3 = teamRole[2].transform.Find("arm").GetComponent<Image>();
        arm3.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));


        Material mat = unlockState ? null : grayMat;

        teamRole[0].material = mat;
        head1.material = mat;
        arm1.material = mat;

        teamRole[1].material = mat;
        head2.material = mat;
        arm2.material = mat;

        teamRole[2].material = mat;
        head3.material = mat;
        arm3.material = mat;
      
    }
}
