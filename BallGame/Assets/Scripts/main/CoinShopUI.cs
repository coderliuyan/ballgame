using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinShopUI : MonoBehaviour 
{
    Canvas canvas;
    public Text coinText;

    public Text[] T_coin;
    public Text[] T_cost;
    public GameObject flash;
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
	void Start () 
    {
		for(int i=0;i<6;i++)
        {
            T_coin[i].text = StaticData.SHOP_COIN[i].ToString();
            T_cost[i].text = StaticData.SHOP_COST[i].ToString();
        }
	}
    public void SetState(bool _state)
    {
        canvas.enabled = _state;
        flash.SetActive(_state);
        if(_state)
        {
            coinText.text = SaveData.CoinNum.ToString();
        }
    }
    public void OnBuy(int _id)
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);

		//添加内购支付 成功返回true 
		//bool dis =  apple.buy(_id);

        SetBuyState(true, _id);
    }
    public void OnReturn()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        SetState(false);
        MainManager.GetInstance().UpdateLastLayer();
    }
    /// <summary>
    /// 返还购买结果 及id
    /// </summary>
    /// <param name="state"></param>
    /// <param name="_id"></param>
    public void SetBuyState(bool state,int _id)
    {
        if(state)
        {
            AudioManager.Instance.PlayEffectAudio(2, transform);
            SaveData.CoinNum += StaticData.SHOP_COIN[_id];
            SaveData.SaveCoinData();
            coinText.text = SaveData.CoinNum.ToString();

            TipEffectMng.GetInstance().ShowTipMsg(1, transform);
            //Debug.Log("coin buy num=" + StaticData.SHOP_COIN[_id]);

            StartCoroutine(DelayFirstCharge());
        }
        else
        {
            Debug.Log("购买失败");
        }
    }
    IEnumerator DelayFirstCharge()
    {
        yield return new WaitForSeconds(0.5f);
        GetFirstChargePrize();
    }
    void GetFirstChargePrize()
    {
        return;
        if (SaveData.IsFirstCharge)
        {
            SaveData.IsFirstCharge = false;
            SaveData.SaveFirstCharge();

            TableValue teamData = XmlHelper.Instance.ReadFile("TeamMsg");
            if (SaveData.TeamUnlockState[8])
            {
                //队伍已拥有 奖励金币

                int prizeValue = teamData.GetInt("09", "unlock");
               // TipEffectMng.GetInstance().ShowTipMsg("首冲充值，奖励金币+" + prizeValue, transform);

                SaveData.CoinNum += prizeValue;
                SaveData.SaveCoinData();
            }
            else
            {
                string teamName = teamData.GetString("09", "name");
                //TipEffectMng.GetInstance().ShowTipMsg("首冲充值，奖励新队伍:" + teamName, transform);

                SaveData.TeamUnlockState[8] = true;
                SaveData.SaveTeamData(8);
            }
        }
    }
}
