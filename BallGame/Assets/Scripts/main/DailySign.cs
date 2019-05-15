using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 每日登陆
/// </summary>
public class DailySign : MonoBehaviour 
{
    private const string SAVE_TIME = "SaveTime";
    private const string SAVE_DAYS = "LoginDays";

    Button SignBtn;
    GameObject signPanel;
    GameObject EndImgGo;
    Button GetPrizeBtn;

    //上一次登陆时间
    int lastYear = 0;
    int lastMonth = 0;
    int lastDay = 0;
    //当前时间
    int nowYear = 0;
    int nowMonth = 0;
    int nowDay = 0;

    /// <summary>
    /// 已经领取奖励
    /// </summary>
    bool IsGetPrize = false;
    /// <summary>
    /// 登陆天数
    /// </summary>
    int loginDays = 0;
	void Start () 
    {
        SignBtn = GetComponent<Button>();
        SignBtn.onClick.AddListener(delegate()
        {
            this.OnShowPanel();
        });
        CheckSignState();

        if(!IsGetPrize)
        {
            Invoke("ShowSign", 1);
        }
      
      
	}
    void CheckSignState()
    {
        loginDays = PlayerPrefs.GetInt(SAVE_DAYS, 0);
        if(loginDays>=7)
        {
            IsGetPrize = true;
        }

        string timeStr = PlayerPrefs.GetString(SAVE_TIME, "0/0/0");
        string[] times = timeStr.Split('/');
        lastYear = int.Parse(times[0]);
        lastMonth = int.Parse(times[1]);
        lastDay = int.Parse(times[2]);
        Debug.Log("SaveTime:  " + lastYear + "/" + lastMonth + "/" + lastDay);

        System.DateTime nowTime = System.DateTime.Now;
        nowYear = nowTime.Year;
        nowMonth = nowTime.Month;
        nowDay = nowTime.Day;
        Debug.Log("NowTime:  " + nowYear + "/" + nowMonth + "/" + nowDay);

        if (lastYear == 0)
        {
            Debug.Log("第一次登陆 可以领取奖励 loginDays=" + loginDays);
            IsGetPrize = false;
        }
        else if (nowYear > lastYear || nowMonth > lastMonth || nowDay > lastDay)
        {
            Debug.Log("连续登陆 可以领取奖励 loginDays=" + loginDays);
            IsGetPrize = false;
        }
        else
        {
            Debug.Log("重复登陆 无奖励 loginDays=" + loginDays);
            IsGetPrize = true;
        }
    }
    void ShowSign()
    {
        if(signPanel==null)
        {
            GameObject prefab = Resources.Load("Prefabs/UI/SignPanel", typeof(GameObject)) as GameObject;
            signPanel = Instantiate(prefab, GameObject.Find("MainCanvas").transform);
            signPanel.transform.SetAsLastSibling();
            signPanel.transform.localPosition = Vector3.zero;
            signPanel.transform.localScale = Vector3.one;

            Button returnBtn = signPanel.transform.Find("ReturnBtn").GetComponent<Button>();
            returnBtn.onClick.AddListener(delegate()
            {
                this.OnReturn();
            });
            signPanel.SetActive(true);

            for (int i = 0; i < 7; i++)
            {
                GameObject prizeGo = signPanel.transform.Find("Prize" + (i + 1).ToString()).gameObject;

                Button btn_get = prizeGo.transform.Find("GetBtn").GetComponent<Button>();
                GameObject endGo = prizeGo.transform.Find("End").gameObject;
        
                if (i > loginDays)
                {
                    btn_get.gameObject.SetActive(true);
                    btn_get.interactable = false;
                    endGo.SetActive(false);
                }
                else if (i == loginDays)
                {
                    EndImgGo = endGo;
                    GetPrizeBtn = btn_get;
                    if (IsGetPrize)
                    {
                        btn_get.gameObject.SetActive(true);
                        btn_get.interactable = false;
                        endGo.SetActive(false);
                    }
                    else
                    {
                        btn_get.gameObject.SetActive(true);
                        btn_get.interactable = true;
                        endGo.SetActive(false);
                        btn_get.onClick.AddListener(delegate()
                        {
                            this.OnGetPrize();
                        });
                    }
                }
                else
                {
                    btn_get.gameObject.SetActive(false);
                    endGo.SetActive(true);
                }
            }
        }
        signPanel.SetActive(true);

    }
    void OnShowPanel()
    {
        ShowSign();
    }
    void OnReturn()
    {
        signPanel.SetActive(false);
    }
    /// <summary>
    /// 领取每日奖励 更新保存数据
    /// </summary>
    void OnGetPrize()
    {
        Debug.Log("领取每日奖励 更新保存数据");
        GetPrizeBtn.gameObject.SetActive(false);
        EndImgGo.SetActive(true);


        //第5和7天奖励队伍 其他奖励金币
        int[] prizeNum={5,10,20,30,1,40,12};
        if(loginDays==4||loginDays==6)
        {
         

            if(SaveData.TeamUnlockState[prizeNum[loginDays]])
            {
                //队伍已拥有 奖励金币
                TableValue teamData = XmlHelper.Instance.ReadFile("TeamMsg");
                string nameStr = (prizeNum[loginDays] + 1).ToString().PadLeft(2, '0');
                int prizeValue = teamData.GetInt(nameStr, "unlock");
                TipEffectMng.GetInstance().ShowPrize(1, prizeNum[loginDays], signPanel.transform);

                SaveData.CoinNum += prizeValue;
                SaveData.SaveCoinData();
            }
            else
            {
                TipEffectMng.GetInstance().ShowPrize(2, prizeNum[loginDays], signPanel.transform);

                SaveData.TeamUnlockState[prizeNum[loginDays]] = true;
                SaveData.SaveTeamData(prizeNum[loginDays]);
            }
        }
        else
        {
            TipEffectMng.GetInstance().ShowPrize(3, prizeNum[loginDays], signPanel.transform);
            SaveData.CoinNum += prizeNum[loginDays];
            SaveData.SaveCoinData();
        }
        loginDays++;
        SaveSignData();

    }
    void SaveSignData()
    {
        PlayerPrefs.SetInt(SAVE_DAYS, loginDays);
        string saveStr = nowYear + "/" + nowMonth + "/" + nowDay;
        PlayerPrefs.SetString(SAVE_TIME, saveStr);
    }
    IEnumerator TestTime()
    {
 
        yield return new WaitForSeconds(1f);


        WWW www = new WWW("http://cgi.im.qq.com/cgi-bin/cgi_svrtime");
        yield return www;
        Debug.Log("北京时间:" + www.text);

    }
}
