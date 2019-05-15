using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingEffect : MonoBehaviour
{
    //public float aliveTime=1.5f;
    public float rotateSpeed = 200;
    void Start()
    {
        //Destroy(gameObject, aliveTime);
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,0,rotateSpeed));
    }
}
