using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 抽奖 随机获得队伍 其他随机给与10-100金币
/// </summary>
public class LotteryUI : MonoBehaviour 
{
    /// <summary>
    /// 16个队伍 抽奖获得概率
    /// </summary>
    private  float[] CHANCE_NUM = { 0, 5, 5, 5, 5, 3.75f, 3.75f, 3.75f, 
          3.75f ,2.5f,2.5f,2.5f,0.625f,0.625f,0.625f,0.625f};
    /// <summary>
    /// 奖励滚动次数 30
    /// </summary>
    const int ROLL_TIMES = 30;
    /// <summary>
    /// 奖励滚动间隔时间 0.1
    /// </summary>
    const float ROLL_INTERVAL = 0.1f;
    /// <summary>
    /// 显示奖品的格数 9
    /// </summary>
    const int GRID_NUM = 9;


    /// <summary>
    /// 抽奖按钮
    /// </summary>
    Button LotteryBtn;
    /// <summary>
    /// 抽奖界面
    /// </summary>
    GameObject prizePanel;
    /// <summary>
    /// 显示金币数量
    /// </summary>
    Text CoinNumShow;



    /// <summary>
    /// 随机奖励物品
    /// </summary>
    GameObject[] prizeMsg;
    /// <summary>
    /// 开始抽奖按钮
    /// </summary>
    Button StartPrizeBtn;
    /// <summary>
    /// 抽奖时选中图标
    /// </summary>
    GameObject ChooseIm;
    /// <summary>
    /// 是否正在抽奖
    /// </summary>
    bool IsRunning = false;


    /// <summary>
    /// 累计滚动次数 控制抽奖动作的停止
    /// </summary>
    int rollNum = 0;
    /// <summary>
    /// 奖励物品   队伍1-15  金币10-100
    /// </summary>
    int prizeValue = 0;
    /// <summary>
    /// 奖励物品类型 0错误类型  1随机队伍  2随机金币  3抽中已有队伍 返还金币
    /// </summary>
    int prizeType = 0;

	void Start () 
    {
        LotteryBtn = GetComponent<Button>();
        LotteryBtn.onClick.AddListener(delegate()
        {
             this.OnShowPanel();
        });
	}
    void OnShowPanel()
    {
        SetPanelState(true);
    }
    /// <summary>
    /// 显示抽奖界面
    /// </summary>
    /// <param name="state"></param>
    void SetPanelState(bool state)
    {
        if (IsRunning)
        {
            return;
        }
        if(prizePanel==null)
        {
            GameObject prefab = Resources.Load("Prefabs/UI/LotteryPanel", typeof(GameObject)) as GameObject;
            prizePanel = Instantiate(prefab, GameObject.Find("MainCanvas").transform);
            prizePanel.transform.localPosition = Vector3.zero;
            prizePanel.transform.localScale = Vector3.one;
            Button returnBtn = prizePanel.transform.Find("ReturnBtn").GetComponent<Button>();
            returnBtn.onClick.AddListener(delegate() { this.SetPanelState(false); });
            CoinNumShow = prizePanel.transform.Find("CoinShow/Text").GetComponent<Text>();
            Button coinBtn = prizePanel.transform.Find("CoinShow/Button").GetComponent<Button>();
            coinBtn.onClick.AddListener(delegate() { this.OnShowCoinShop(); });
            InitPanelData();
        }
        prizePanel.SetActive(state);
        UpdateCoinShow();
    }
    void InitPanelData()
    {   
        prizeMsg = new GameObject[GRID_NUM];
        for (int i = 1; i <= GRID_NUM; i++)
        {
            prizeMsg[i-1] = prizePanel.transform.Find("Prize"+i).gameObject;
        }

        ChooseIm = prizePanel.transform.Find("Choose").gameObject;
        ChooseIm.SetActive(false);
        StartPrizeBtn = prizePanel.transform.Find("StartPrizeBtn").GetComponent<Button>();
        StartPrizeBtn.onClick.AddListener(delegate()
        {
            this.OnRandUse();
        });


    }
    void OnShowCoinShop()
    {
        MainManager.GetInstance().SetShopState(3, true);
    }
    public void UpdateCoinShow()
    {
        CoinNumShow.text = SaveData.CoinNum.ToString();
    }
    /// <summary>
    /// 随机抽取 
    /// </summary>
    void OnRandUse()
    {
        if (IsRunning)
        {
            return;
        }
        if(SaveData.CoinNum>=100)
        {
            Debug.Log("开始抽奖");
            SaveData.CoinNum -= 100;
            SaveData.SaveCoinData();
            UpdateCoinShow();

            TipEffectMng.GetInstance().ShowTipMsg(4, prizePanel.transform);

            IsRunning = true;

            float randNum = Random.Range(0.0001f, 100.0f);
            // Debug.Log("概率:" + randNum);
            float num = 0;
            prizeValue = 16;
            for (int i = 0; i < 16; i++)
            {
                num += CHANCE_NUM[i];
                if (randNum < num)
                {
                    prizeValue = i;
                    break;
                }
            }
            if (prizeValue < 16)
            {
                //随机到队伍奖励 
                if (SaveData.TeamUnlockState[prizeValue])
                {
                    //队伍已存在 奖励队伍购买金币
                    prizeType = 3;      
                }
                else
                {
                    //奖励队伍 
                    prizeType = 1;
                }
            }
            else
            {
                //未抽中队伍  奖励金币
                prizeType = 2;
                prizeValue = Random.Range(10, 101);
            }

            rollNum = 0;
            StartCoroutine(PrizeRoll());
        }
        else
        {
            TipEffectMng.GetInstance().ShowTipMsg(2, prizePanel.transform);
        }
    }
    IEnumerator PrizeRoll()
    {
        yield return new WaitForSeconds(0.1f);
        ChooseIm.SetActive(true);

        int randNum = 0;
        while (rollNum < ROLL_TIMES)
        {
            rollNum++;
            randNum = Random.Range(0, GRID_NUM);
            ChooseIm.transform.localPosition = prizeMsg[randNum].transform.localPosition;
            yield return new WaitForSeconds(ROLL_INTERVAL);
        }
        Debug.Log("奖励动画完成");
      
     

        if(prizeType==1)
        {
            Debug.Log("获得队伍:" + prizeValue);
            TipEffectMng.GetInstance().ShowPrize(2, prizeValue, prizePanel.transform);

            SaveData.TeamUnlockState[prizeValue] = true;
            SaveData.SaveTeamData(prizeValue);
        }
        else if(prizeType==2)
        {
            Debug.Log("获得金币:" + prizeValue);
            TipEffectMng.GetInstance().ShowPrize(3, prizeValue, prizePanel.transform);

            SaveData.CoinNum += prizeValue;
            SaveData.SaveCoinData();
        }
        else if(prizeType==3)
        {      
            TableValue teamData = XmlHelper.Instance.ReadFile("TeamMsg");
            string nameStr = (prizeValue + 1).ToString().PadLeft(2, '0');
            int  coinValue = teamData.GetInt(nameStr, "unlock");

            Debug.Log("抽中队伍已存在 队伍:" + prizeValue + "->金币:" + coinValue);

            TipEffectMng.GetInstance().ShowPrize(1, prizeValue, prizePanel.transform);

            SaveData.CoinNum += coinValue;
            SaveData.SaveCoinData();
        }

        UpdateCoinShow();
        IsRunning = false;

    }
}
