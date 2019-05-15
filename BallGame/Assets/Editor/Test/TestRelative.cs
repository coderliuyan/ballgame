using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestRelative : MonoBehaviour
{      
    public float s2 = 0;
    public int m1 = 0,m2 = 0,s1 = 0,h1 = 0, h2 = 0;

    public Text M_Time;
	void Start () 
    {


	}

    void Update()
    {
        SecondToMinute();
        M_Time.text = "" +h1+h2 + ":" + m1 + m2 + ":" + s1 + (int)s2;

    }
    void SecondToMinute()
    {
        s2 += 1 * Time.deltaTime;
        if((int)s2 == 10)
        {
            s2 = 0;
            s1 += 1;
        }
        if(s1 == 6)
        {
            s1 = 0;
            m2 += 1;
        }
        if(m2 == 10)
        {
            m2 = 0;
            m1 += 1;
        }
        if (m1 == 60)
        {
            m1 = 0;
            h2 += 1;
        }
        if (h2 == 10)
        {
            h2 = 0;
            h1 += 1;
        }
    }

}
