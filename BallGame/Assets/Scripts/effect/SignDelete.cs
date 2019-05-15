using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDelete : MonoBehaviour 
{

    public float aliveTime = 5.5f;

    public Vector3 startPos;
    public Vector3 moveDistance ;
    void Start()
    {
        transform.localPosition = startPos;
        Destroy(gameObject, aliveTime);
    }

    void FixedUpdate()
    {
        //使文本在垂直方向山产生一个偏移    
        transform.Translate(moveDistance * Time.deltaTime);
    }
}
