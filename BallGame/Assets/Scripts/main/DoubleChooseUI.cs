using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 双人对战选择 双方队伍
/// </summary>
public class DoubleChooseUI : MonoBehaviour 
{
    //public Text coinNum;

    public Text teamName1;
    public Text teamName2;

    public Image teamBadge1;
    public Image teamBadge2;

    public Image[] roles1;
    public Image[] roles2;

    public GameObject leftBtn1;
    public GameObject rightBtn1;
    public GameObject leftBtn2;
    public GameObject rightBtn2;
    public Button startBtn;

    public Material grayMat;

    TableValue teamData;
	void Awake () 
    {
        StaticData.TeamSkin2 = 0;
        teamData = XmlHelper.Instance.ReadFile("TeamMsg");
	}
    public void SetState(bool _state)
    {
        gameObject.SetActive(_state);
        if (_state)
        {
            InitDoubleData();
        }
    }
    void InitDoubleData()
    {
        //coinNum.text = SaveData.CoinNum.ToString();

        InitLeftRoleData();
        InitRightRoleData();
    }
    void InitLeftRoleData()
    {
        //角色信息
        teamBadge1.sprite = MyTools.LoadSprite(string.Format("Prefabs/Badge/im_badge{0}", StaticData.TeamSkin1 + 1));

        string nameStr = (StaticData.TeamSkin1 + 1).ToString().PadLeft(2, '0');
        teamName1.text = teamData.GetString(nameStr,"name");

        SetRole1();

        if (StaticData.TeamSkin1 == 0)
        {
            leftBtn1.SetActive(false);
        }
        else
        {
            leftBtn1.SetActive(true);
        }
        if (StaticData.TeamSkin1 == StaticData.TEAM_NUMS - 1)
        {
            rightBtn1.SetActive(false);
        }
        else
        {
            rightBtn1.SetActive(true);
        }
    }
    void InitRightRoleData()
    {
        //角色信息
        teamBadge2.sprite = MyTools.LoadSprite(string.Format("Prefabs/Badge/im_badge{0}", StaticData.TeamSkin2 + 1));
        string nameStr = (StaticData.TeamSkin2 + 1).ToString().PadLeft(2, '0');
        teamName2.text = teamData.GetString(nameStr, "name");

        SetRole2();

        if (StaticData.TeamSkin2 == 0)
        {
            leftBtn2.SetActive(false);
        }
        else
        {
            leftBtn2.SetActive(true);
        }
        if (StaticData.TeamSkin2 == StaticData.TEAM_NUMS - 1)
        {
            rightBtn2.SetActive(false);
        }
        else
        {
            rightBtn2.SetActive(true);
        }
    }
    public void OnReturn()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        SetState(false);

        if (!SaveData.TeamUnlockState[StaticData.TeamSkin1])
        {
            StaticData.TeamSkin1 = 0;
        }
        if (!SaveData.TeamUnlockState[StaticData.TeamSkin2])
        {
            StaticData.TeamSkin2 = 0;
        }

        MainManager.GetInstance().SetMainState(true);

      
    }
    public void OnStart()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);

        StaticData.g_weather = (byte)Random.Range(0, 3);
        SceneManager.LoadScene(StaticData.SCENENAME_DOUBLE);

        
    }
    public void OnLeft1()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.TeamSkin1 > 0)
        {
            StaticData.TeamSkin1--;
            InitLeftRoleData();
        }
      
    }
    public void OnRight1()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.TeamSkin1 < StaticData.TEAM_NUMS - 1)
        {
            StaticData.TeamSkin1++;
            InitLeftRoleData();
        }
     
    }
    public void OnLeft2()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.TeamSkin2 > 0)
        {
            StaticData.TeamSkin2--;
            InitRightRoleData();
        }
      
    }
    public void OnRight2()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.TeamSkin2 < StaticData.TEAM_NUMS - 1)
        {
            StaticData.TeamSkin2++;
            InitRightRoleData();
        }
      
    }
    void SetRole1()
    {
        int roleId = (StaticData.TeamSkin1 + 1) * 100 + 1;
        roles1[0].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head1 = roles1[0].transform.Find("head").GetComponent<Image>();
        head1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm1 = roles1[0].transform.Find("arm").GetComponent<Image>();
        arm1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        roleId = (StaticData.TeamSkin1 + 1) * 100 + 2;
        roles1[1].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head2 = roles1[1].transform.Find("head").GetComponent<Image>();
        head2.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm2 = roles1[1].transform.Find("arm").GetComponent<Image>();
        arm2.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        roleId = (StaticData.TeamSkin1 + 1) * 100 + 3;
        roles1[2].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head3 = roles1[2].transform.Find("head").GetComponent<Image>();
        head3.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm3 = roles1[2].transform.Find("arm").GetComponent<Image>();
        arm3.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        // 灰色显示
        Material mat = SaveData.TeamUnlockState[StaticData.TeamSkin1] ? null : grayMat;
        roles1[0].material = mat;
        head1.material = mat;
        arm1.material = mat;

        roles1[1].material = mat;
        head2.material = mat;
        arm2.material = mat;

        roles1[2].material = mat;
        head3.material = mat;
        arm3.material = mat;

          SetStartBtnState();
    }
    void SetRole2()
    {
        int roleId = (StaticData.TeamSkin2 + 1) * 100 + 1;
        roles2[0].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head1 = roles2[0].transform.Find("head").GetComponent<Image>();
        head1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm1 = roles2[0].transform.Find("arm").GetComponent<Image>();
        arm1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        roleId = (StaticData.TeamSkin2 + 1) * 100 + 2;
        roles2[1].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head2 = roles2[1].transform.Find("head").GetComponent<Image>();
        head2.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm2 = roles2[1].transform.Find("arm").GetComponent<Image>();
        arm2.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        roleId = (StaticData.TeamSkin2 + 1) * 100 + 3;
        roles2[2].sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
        Image head3 = roles2[2].transform.Find("head").GetComponent<Image>();
        head3.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
        Image arm3 = roles2[2].transform.Find("arm").GetComponent<Image>();
        arm3.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

        // 灰色显示
        Material mat = SaveData.TeamUnlockState[StaticData.TeamSkin2] ? null : grayMat;
        roles2[0].material = mat;
        head1.material = mat;
        arm1.material = mat;

        roles2[1].material = mat;
        head2.material = mat;
        arm2.material = mat;

        roles2[2].material = mat;
        head3.material = mat;
        arm3.material = mat;

        SetStartBtnState();
    }
    void SetStartBtnState()
    {
        startBtn.gameObject.SetActive(SaveData.TeamUnlockState[StaticData.TeamSkin1] && SaveData.TeamUnlockState[StaticData.TeamSkin2]);
    }
}
