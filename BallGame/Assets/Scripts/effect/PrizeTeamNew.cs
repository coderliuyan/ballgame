using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 获得新队伍奖励
/// </summary>
public class PrizeTeamNew : IPrizeType 
{
    private Image teamPrzie;
    private Text teamName;
	void Awake () 
    {
        teamPrzie = transform.Find("TeamPrize").GetComponent<Image>();
        teamName = transform.Find("Word2").GetComponent<Text>();
	}
    public override void ShowPrize(int _id)
    {
        teamPrzie.sprite = MyTools.LoadSprite("Prefabs/PrizeType/lottery_prize_" + _id);
        teamPrzie.SetNativeSize();

        TableValue teamData = XmlHelper.Instance.ReadFile("TeamMsg");
        string nameStr = (_id + 1).ToString().PadLeft(2, '0');
        string prizeValue = teamData.GetString(nameStr, "name");
        teamName.text = prizeValue;
    }
}
