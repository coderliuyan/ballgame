using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 奖励动画
/// </summary>
public class PrizeAction : MonoBehaviour 
{
    public Image BgImg;
    public Image TipImg;
	void Start ()
    {
        StartCoroutine(BlinkAction());
	}
    IEnumerator  BlinkAction()
    {
        while(gameObject.activeSelf)
        {
            TipImg.gameObject.SetActive(!TipImg.gameObject.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }     
    }
    void Update()
    {
        BgImg.transform.Rotate(Vector3.forward, 1);
    }
}
