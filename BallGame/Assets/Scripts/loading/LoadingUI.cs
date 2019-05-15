using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingUI : MonoBehaviour {

    public Text T_loadNum;
    private int loadNum = 0;
	// Use this for initialization
	void Start () 
    {
        InvokeRepeating("TimeUpdate", 0.5f, 0.02f);
	}
    void TimeUpdate()
    {
        loadNum++;
        if(loadNum>=100)
        {
            loadNum = 100;
            ToNextScene();
            this.CancelInvoke();
        }
        T_loadNum.text = loadNum.ToString() + "%";
    }

    void ToNextScene()
    {
        SceneManager.LoadScene(StaticData.SCENENAME_MAIN);
    }
}
