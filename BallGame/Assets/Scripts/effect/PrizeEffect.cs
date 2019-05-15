using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeEffect : MonoBehaviour
{

   // public float aliveTime=5;
    public float moveDistance = 200;
    void Start()
    {
        //Destroy(gameObject, aliveTime);
    }

    void FixedUpdate()
    {
        //使文本在垂直方向山产生一个偏移    
        transform.Translate(Vector3.down * moveDistance * Time.deltaTime);
        if(transform.localPosition.y<-2200)
        {
            transform.localPosition = new Vector3(0, 2200, 0);
        }
    }
}
