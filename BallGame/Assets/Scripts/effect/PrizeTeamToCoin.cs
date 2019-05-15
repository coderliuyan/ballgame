using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 获得队伍奖励 队伍已拥有给予金币奖励
/// </summary>
public class PrizeTeamToCoin : IPrizeType
{
    private Image teamPrzie;
    private Text CoinNum;
    void Awake()
    {
        teamPrzie = transform.Find("TeamPrize").GetComponent<Image>();
        CoinNum = transform.Find("Word3").GetComponent<Text>();
    }
    public override void ShowPrize(int _id)
    {
        teamPrzie.sprite = MyTools.LoadSprite("Prefabs/PrizeType/lottery_prize_" + _id);
        teamPrzie.SetNativeSize();

        TableValue teamData = XmlHelper.Instance.ReadFile("TeamMsg");
        string nameStr = (_id + 1).ToString().PadLeft(2, '0');
        string  prizeValue = teamData.GetString(nameStr, "unlock");
        CoinNum.text = prizeValue;
    }

}
