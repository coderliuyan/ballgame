using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWWW : MonoBehaviour {

    float time1,time2;
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {

        sr = GetComponent<SpriteRenderer>();

        time1 = Time.time;
        StartCoroutine(Init());


        time2 = Time.time;
        InitR();
	}

    public string url = "file://E:/u3d5.5projects/BallGame/Assets/Texture/role/role005/head5.png";
    IEnumerator Init()
    {
        WWW www = new WWW(url);
        yield return www;
       // Renderer renderer = GetComponent<Renderer>();
        //renderer.material.mainTexture = www.texture;
        Texture2D texture = www.texture;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
      
        sr.sprite = sprite;
        Debug.Log("WWW  " + (Time.time - time1));
    }
    void InitR()
    {
        sr.sprite = MyTools.LoadSprite("Prefabs/role/head5");
        Debug.Log("Res  " + (Time.time - time2));
    }
}
