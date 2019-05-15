using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestDataSet : MonoBehaviour
{

	// Use this for initialization
	void Awake () {

        Physics2D.gravity = new Vector2(0, StaticData.g_gravity);
        StaticData.g_gameStart = true;
        StaticData.g_gameEnd = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SettingScene");
        }
	}
}
