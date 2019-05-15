using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : MonoBehaviour
{

    public float aliveTime=5;
    public float moveDistance = 200;
    void Start()
    {
        Destroy(gameObject, aliveTime);
    }

    void FixedUpdate()
    {
        //使文本在垂直方向山产生一个偏移    
        transform.Translate(Vector3.down * moveDistance * Time.deltaTime);
    }
}
