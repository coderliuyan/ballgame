using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 奖励金币
/// </summary>
public class PrizeCoin : IPrizeType
{
    private Text CoinNum;
	void Awake ()
    {
        CoinNum = transform.Find("Word2").GetComponent<Text>();
	}
    public override void ShowPrize(int _id)
    {
        CoinNum.text = _id.ToString();
    }
}
