using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSet : MonoBehaviour 
{
    SpriteRenderer bgSpr1;
    SpriteRenderer bgSpr2;
	void Start () 
    {
        //StaticData.g_weather = 0;

        bgSpr1 = transform.Find("bg1").GetComponent<SpriteRenderer>();
        bgSpr2 = transform.Find("bg2").GetComponent<SpriteRenderer>();

        bgSpr1.sprite = MyTools.LoadSprite(string.Format("Prefabs/Bg/im_bg1_{0}", StaticData.g_weather + 1));
        bgSpr2.sprite = MyTools.LoadSprite(string.Format("Prefabs/Bg/im_bg1_{0}", StaticData.g_weather + 1));

        if(StaticData.g_weather==0)
        {
            GameObject rainPfb = Resources.Load("Prefabs/weather/Cloud", typeof(GameObject)) as GameObject;
            if (rainPfb != null)
            {
                GameObject dcGo = MonoBehaviour.Instantiate(rainPfb, transform);
                dcGo.transform.localPosition = new Vector3(0, 0, 8);
                dcGo.transform.localScale = Vector3.one;
            }
        }
        else if(StaticData.g_weather==1)
        {
            GameObject rainPfb = Resources.Load("Prefabs/weather/Sunlight", typeof(GameObject)) as GameObject;
            if (rainPfb != null)
            {
                GameObject dcGo = MonoBehaviour.Instantiate(rainPfb, transform);
                dcGo.transform.localPosition = new Vector3(10.53f, 5.5f, 0);
                dcGo.transform.localScale = Vector3.one*0.5f;
            }
        }
        if (StaticData.g_weather == 2)
        {
            GameObject rainPfb = Resources.Load("Prefabs/weather/Rain_NoSplash_Legacy_Prefab", typeof(GameObject)) as GameObject;
            if (rainPfb != null)
            {
                GameObject dcGo = MonoBehaviour.Instantiate(rainPfb,transform);
                dcGo.transform.localPosition =new Vector3(0,30,-10);
                dcGo.transform.localScale = Vector3.one;
            }
        }
	}
}
