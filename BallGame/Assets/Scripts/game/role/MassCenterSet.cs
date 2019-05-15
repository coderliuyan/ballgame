using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设置中心位置
/// </summary>
public class MassCenterSet : MonoBehaviour 
{
    Rigidbody2D rb;
    public GameObject center;
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = center.transform.localPosition;
	}
}
