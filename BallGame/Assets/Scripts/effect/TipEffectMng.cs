using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipEffectMng
{
    private static TipEffectMng ms_Ptr = null;
    public static TipEffectMng GetInstance()
    {
        if (ms_Ptr == null)
        {
            ms_Ptr = new TipEffectMng();
        }
        return ms_Ptr;
    }
    /// <summary>
    /// 1购买成功
    /// 2金币不足
    /// 3添加新队伍
    /// 4减100 金币
    /// 
    /// </summary>
   public  void ShowTipMsg(int _id,Transform tr)
    {
       switch(_id)
       {
           case 1:
               {
                   GameObject tipShow = Resources.Load("Prefabs/TipMsg" + _id.ToString(), typeof(GameObject)) as GameObject;
                   GameObject tip1 = MonoBehaviour.Instantiate(tipShow);
                   tip1.transform.SetParent(tr);
                   tip1.transform.localScale = Vector3.one;
               }
               break;
           case 2:
               {
                   GameObject tipShow = Resources.Load("Prefabs/TipMsg" + _id.ToString(), typeof(GameObject)) as GameObject;
                   GameObject tip1 = MonoBehaviour.Instantiate(tipShow);
                   tip1.transform.SetParent(tr);
                   tip1.transform.localScale = Vector3.one;
               }
               break;
        
           case 3:
               {
                    GameObject tipShow = Resources.Load("Prefabs/TipMsg" + _id.ToString(), typeof(GameObject)) as GameObject;
                   GameObject tip1 = MonoBehaviour.Instantiate(tipShow);
                   tip1.transform.SetParent(tr);
                   tip1.transform.localScale = Vector3.one;
               }
               break;
           case 4:
               {
                   GameObject tipShow = Resources.Load("Prefabs/TipMsg" + _id.ToString(), typeof(GameObject)) as GameObject;
                   GameObject tip1 = MonoBehaviour.Instantiate(tipShow);
                   tip1.transform.SetParent(tr);
                   tip1.transform.localScale = Vector3.one;
               }
               break;
       }
     
    }
    /// <summary>
   /// 显示获得的奖励 1队伍换金币 2新队伍 3金币
    /// </summary>
   public void ShowPrize(int _type, int _id, Transform tr)
   {
       GameObject tipShow = Resources.Load("Prefabs/PrizeType/PrizePanel" + _type.ToString(), typeof(GameObject)) as GameObject;
		if (tipShow == null) {
			Debug.LogError ("对象没有加载出来！");
			Debug.Log ("错误的地方！！！！！");
		} else {
			Debug.Log (tipShow.name + " ............");
		}

       GameObject tip1 = MonoBehaviour.Instantiate(tipShow);
		if(tip1 == null){
			Debug.Log("初始化失败！！！！");
		}else {
			Debug.Log (tip1.name + " ............");
		}

       tip1.transform.SetParent(tr);
       tip1.transform.localScale = Vector3.one;
       IPrizeType prize = tip1.GetComponent<IPrizeType>();
       prize.ShowPrize(_id);
   }
    /// <summary>
    /// 显示文字提示信息
    /// </summary>
    /// <param name="_msg">文字内容</param>
    /// <param name="tr">父节点</param>
    public void ShowTipMsg(string _msg,Transform tr,string s)
   {
       GameObject tipShow = Resources.Load("Prefabs/TipMsg4", typeof(GameObject)) as GameObject;
       GameObject tip1 = MonoBehaviour.Instantiate(tipShow);
       Text msg = tip1.GetComponent<Text>();
       msg.text = _msg;

       tip1.transform.SetParent(tr);
       tip1.transform.localScale = Vector3.one;
   }
    /// <summary>
    /// 播放特效 
    /// 1解锁特效
    /// </summary>
   public void ShowActionEffect(int _id, Transform tr,Vector3 pos)
   {
       switch (_id)
       {
           case 1:
               {
                   GameObject tipShow = Resources.Load("Prefabs/Unlock", typeof(GameObject)) as GameObject;
                   GameObject tip1 = MonoBehaviour.Instantiate(tipShow);
                   tip1.transform.SetParent(tr);
                   tip1.transform.localPosition = pos;
                   tip1.transform.localScale = Vector3.one;
               }
               break;
       }
   }
}
