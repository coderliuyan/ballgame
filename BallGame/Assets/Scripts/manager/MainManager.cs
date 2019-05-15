using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager
{
    private static MainManager ms_Ptr = null;
    public static MainManager GetInstance()
    {
        if (ms_Ptr == null)
        {
            ms_Ptr = new MainManager();
            ms_Ptr.Start();
        }
        return ms_Ptr;
    }
	void Start () 
    {
		
	}


    private DoubleChooseUI    dcUI;        //双人模式选择界面
    private TeamChooseUI tcUI;             //角色队伍选择界面
    private MatchChooseUI     mcUI;        //杯赛模式选择界面
    private CoinShopUI shopUI;             //商城界面
    private MainUI mainUI;                 //主界面
    private LotteryUI lotteryUI;

    private byte lastLayer = 0;            // 1比赛模式 2挑战模式 3抽奖界面

    bool GetDoubleChooseUI()
    {
        if (dcUI == null)
        {
            GameObject dcGo = null;
            if (dcGo == null)
            {
                GameObject dcPfb = Resources.Load("Prefabs/UI/DoubleChoose", typeof(GameObject)) as GameObject;
                if (dcPfb != null)
                {
                    dcGo = Object.Instantiate(dcPfb, GameObject.Find("MainCanvas").transform);
                    dcGo.transform.localPosition = Vector3.zero;
                    dcGo.transform.localScale = Vector3.one;
                }
                else
                {
                    Debug.LogError("dcPfb is not exist");
                    return false;
                }          
            }         
            if(dcGo!=null)
            {
                dcUI = dcGo.GetComponent<DoubleChooseUI>();               
            }
            else
            {
                Debug.LogError("dcGo is not exist");
                return false;
            }
        }
        return true;
    }
    public void SetDoubleChooseState(bool _state)
    {
        if(GetDoubleChooseUI())
        {
            dcUI.SetState(_state);
        }
    }
    bool GetTeamChooseUI()
    {
        if (tcUI == null)
        {
            GameObject ccGo = null;
            if (ccGo == null)
            {
                GameObject ccPfb = Resources.Load("Prefabs/UI/TeamChoose", typeof(GameObject)) as GameObject;
                if (ccPfb != null)
                {
                    ccGo = Object.Instantiate(ccPfb, GameObject.Find("MainCanvas").transform);
                    ccGo.transform.localPosition = Vector3.zero;
                    ccGo.transform.localScale = Vector3.one;
                }
                else
                {
                    Debug.LogError("ccPfb is not exist");
                    return false;
                }
            }
            if (ccGo != null)
            {
                tcUI = ccGo.GetComponent<TeamChooseUI>();
            }
            else
            {
                Debug.LogError("ccGo is not exist");
                return false;
            }
        }
        return true;
    }
    public void SetTeamChooseState(bool _state)
    {
        if (GetTeamChooseUI())
        {
            tcUI.SetState(_state);
        }
    }
    bool GetMatchChooseUI()
    {
        if (mcUI == null)
        {
            GameObject mcGo = null;
            if (mcGo == null)
            {
                GameObject ccPfb = Resources.Load("Prefabs/UI/MatchChoose", typeof(GameObject)) as GameObject;
                if (ccPfb != null)
                {
                    mcGo = Object.Instantiate(ccPfb, GameObject.Find("MainCanvas").transform);
                    mcGo.transform.localPosition = Vector3.zero;
                    mcGo.transform.localScale = Vector3.one;
                }
                else
                {
                    Debug.LogError("ccPfb is not exist");
                    return false;
                }
            }
            if (mcGo != null)
            {
                mcUI = mcGo.GetComponent<MatchChooseUI>();
            }
            else
            {
                Debug.LogError("ccGo is not exist");
                return false;
            }
        }
        return true;
    }
    public void SetMatchChooseState(bool _state)
    {
        if (GetMatchChooseUI())
        {
            mcUI.SetState(_state);
        }
    }
    bool GetCoinShopUI()
    {
        if (shopUI == null)
        {
            GameObject coinGo = GameObject.Find("CoinCanvas");
            if (coinGo == null)
            {
                Debug.LogError("coinGo is not exist");
                return false;
            }
            shopUI = coinGo.GetComponent<CoinShopUI>();
            if (shopUI == null)
            {
                Debug.LogError("shopUI is not exist");
                return false;
            }
        }

        return true;
    }
    public void SetShopState(byte _id,bool _state)
    {
        lastLayer = _id;
        if(GetCoinShopUI())
        {
            shopUI.SetState(_state);
        }
    }
    bool GetMainUI()
    {
        if (mainUI == null)
        {
            GameObject mainGo = GameObject.Find("MainCanvas");
            if (mainGo == null)
            {
                Debug.LogError("mainGo is not exist");
                return false;
            }
            mainUI = mainGo.GetComponent<MainUI>();
            if (mainUI == null)
            {
                Debug.LogError("mainUI is not exist");
                return false;
            }
        }

        return true;
    }
    public void SetMainState(bool _state)
    {
        if(GetMainUI())
        {
            mainUI.SetBaseState(_state);
        }
    }
    bool GetLotteryUI()
    {
        if (lotteryUI == null)
        {
            GameObject lotteryGo = GameObject.Find("LotteryBtn");
            if (lotteryGo == null)
            {
                Debug.LogError("LotteryBtn is not exist");
                return false;
            }
            lotteryUI = lotteryGo.GetComponent<LotteryUI>();
            if (lotteryUI == null)
            {
                Debug.LogError("lotteryUI is not exist");
                return false;
            }
        }

        return true;
    }
    public void UpdateLastLayer()
    {
        if(lastLayer==2)
        {
            if (GetTeamChooseUI())
            {
                tcUI.UpdateCoinShow();
            }
        }
        else if(lastLayer==3)
        {
            if (GetLotteryUI())
            {
                lotteryUI.UpdateCoinShow();
            }
        }
        lastLayer = 0;
    }
}
