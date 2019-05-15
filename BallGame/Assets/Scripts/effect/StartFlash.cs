using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlash : MonoBehaviour 
{
	void Start () 
    {
		
	}
    public void AnimationEnd()
    {
        //Debug.Log("AnimationEnd");
        GameObject flash = transform.Find("flash").gameObject;
        flash.SetActive(true);
    }
}
