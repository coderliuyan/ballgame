using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameDoubleUI : MonoBehaviour 
{
    public SpriteRenderer[] scoreSpr;

    public SpriteRenderer timeSpr;     //开始游戏倒计时
    int timeNum = 3;

    public GameObject pauseLayer;
    public GameObject overLayer;

    public Image im_tip;

    GameObject[] allBtns;
	void Start () 
    {    
        Time.timeScale = 1;
        Physics2D.gravity = new Vector2(0, StaticData.g_gravity);

        StaticData.g_gameEnd = false;

        GameManager.GetInstance();
        AudioManager.Instance.PlayBgAudio(3);
        ShowScore();
       

        StartCoroutine(TimeShow());
        timeSpr.sprite = MyTools.LoadSprite("Prefabs/Num/timeNum/" + timeNum.ToString());

        if (StaticData.g_score != 0)
        {
            im_tip.gameObject.SetActive(false);
        }
        Slider musicSlider = pauseLayer.transform.Find("S_music").GetComponent<Slider>();
        musicSlider.value = SaveData.MusicValue;
        Slider soundSlider = pauseLayer.transform.Find("S_sound").GetComponent<Slider>();
        soundSlider.value = SaveData.SoundValue;

        allBtns = new GameObject[7];
        allBtns[0] = transform.Find("btn_left1").gameObject;
        allBtns[1] = transform.Find("btn_right1").gameObject;
        allBtns[2] = transform.Find("btn_drop1").gameObject;
        allBtns[3] = transform.Find("btn_left2").gameObject;
        allBtns[4] = transform.Find("btn_right2").gameObject;
        allBtns[5] = transform.Find("btn_drop2").gameObject;
        allBtns[6] = transform.Find("btn_pause").gameObject;
	}
    IEnumerator TimeShow()
    {

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1);
            timeNum--;
            //Debug.Log("timeNum=" + timeNum);
            if (timeNum < 0)
            {
                timeSpr.gameObject.SetActive(false);
                GameManager.GetInstance().StartGame();

                im_tip.gameObject.SetActive(false);
            }
            else
            {
                timeSpr.sprite = MyTools.LoadSprite("Prefabs/Num/timeNum/" + timeNum.ToString());
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(StaticData.SCENENAME_MAIN);
        }
    }
    public void OnMusicChanged(float _v)
    {
        //Debug.Log("OnMusicChanged " + _v);
        SaveData.MusicValue = _v;
        AudioManager.Instance.ChangeBgVolume();
    }
    public void OnSoundChanged(float _v)
    {
        //Debug.Log("OnSoundChanged " + _v);
        SaveData.SoundValue = _v;
    }
    /// <summary>
    /// 双人模式重新开始
    /// </summary>
    public void OnReSetScene()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        StaticData.g_gameEnd = false;
        StaticData.g_score = 0;

        //Time.timeScale = 1;
        SceneManager.LoadScene(StaticData.SCENENAME_DOUBLE);
    }
    public void OnReturn()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        SceneManager.LoadScene(StaticData.SCENENAME_MAIN);
    }
    /// <summary>
    /// 双人模式继续
    /// </summary>
    public void OnNext()
    {
        SceneManager.LoadScene(StaticData.SCENENAME_DOUBLE);
    }
    public void OnLeft1()
    {
        GameManager.GetInstance().TeamBack1();
    }
    public void OnRight1()
    {
        GameManager.GetInstance().TeamFront1();
    }
    public void OnDrop1()
    {
        GameManager.GetInstance().OnDoropBall(0);
    }
    public void OnLeft2()
    {
        GameManager.GetInstance().TeamFront2();
    }
    public void OnRight2()
    {
        GameManager.GetInstance().TeamBack2();
    }
    public void OnDrop2()
    {
        GameManager.GetInstance().OnDoropBall(1);
    }
    public void OnPause()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        if (StaticData.g_gameEnd)
        {
            return;
        }
        Time.timeScale = 0;
        pauseLayer.SetActive(true);

        SetAllBtnState(false);
    }
    public void OnResume()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        Time.timeScale = 1;
        pauseLayer.SetActive(false);

        SetAllBtnState(true);
    }

   void SetResult(bool _state)
    {
        StaticData.g_gameEnd = true;
  
        overLayer.SetActive(true);

        int num1 = StaticData.g_score / 10;
        int num2 = StaticData.g_score % 10;
        Image im_num1 = overLayer.transform.Find("ScoreNum/Num1").GetComponent<Image>();
        Image im_num2 = overLayer.transform.Find("ScoreNum/Num2").GetComponent<Image>();
        im_num1.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num1.ToString());
        im_num2.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num2.ToString());

        Image im_badge1 = overLayer.transform.Find("Im_team1").GetComponent<Image>();
        Image im_badge2 = overLayer.transform.Find("Im_team2").GetComponent<Image>();
        im_badge1.sprite = MyTools.LoadSprite("Prefabs/Badge/im_badge" + (StaticData.TeamSkin1 + 1).ToString());
        im_badge2.sprite = MyTools.LoadSprite("Prefabs/Badge/im_badge" + (StaticData.TeamSkin2 + 1).ToString());

        AudioManager.Instance.PlayEffectAudio(9, transform);
        if(_state)
        {
           // T_result.text = "1赢了"; 
            GameObject go = overLayer.transform.Find("Win2").gameObject;
            go.SetActive(false);
        }
        else
        {
            //    T_result.text = "2赢了";
            GameObject go = overLayer.transform.Find("Win1").gameObject;
            go.SetActive(false);
        }
    }
    public void SetScore(bool win)
    {
        if (StaticData.g_gameEnd)
        {
            return;
        }
        HideAllBtn();
        StaticData.g_gameEnd = true;
        if(win)
        {
            StaticData.g_score += 10;
        }
        else
        {
            StaticData.g_score += 1;
        }
        ShowScore();    
    }
    void HideTip()
    {
        im_tip.gameObject.SetActive(false);

        int num1 = StaticData.g_score / 10;
        int num2 = StaticData.g_score % 10;
        if (num1 >= 3)
        {
            SetResult(true);
        }
        else if (num2 >= 3)
        {
            SetResult(false);
        }
        else
        {
            Invoke("DelayToNext", 0.5f);
        }
    }
    /// <summary>
    /// 回合结束自动跳转下一关
    /// </summary>
    void DelayToNext()
    {
        OnNext();
    }
    void HideAllBtn()
    {
        SetAllBtnState(false);

        im_tip.gameObject.SetActive(true);
        im_tip.sprite = MyTools.LoadSprite("Prefabs/im_tip3");
        im_tip.SetNativeSize();
        im_tip.transform.localPosition = new Vector3(0, 337, 0);
        Animator amt = im_tip.GetComponent<Animator>();
        amt.enabled = true;
        amt.speed = 3;
        Invoke("HideTip", 0.5f);
    }
    void SetAllBtnState(bool _state)
    {
        for (int i = 0; i < 7; i++)
        {
            allBtns[i].SetActive(_state);
        }
    }
    void ShowScore()
    {
        int num1 = StaticData.g_score / 10;
        int num2 = StaticData.g_score % 10;
        scoreSpr[0].sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum1/" + num1.ToString());
        scoreSpr[1].sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum1/" + num2.ToString());
    }
}
