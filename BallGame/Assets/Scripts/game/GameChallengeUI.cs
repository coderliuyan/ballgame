using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameChallengeUI : MonoBehaviour
{
    public SpriteRenderer[] scoreSpr;//分数显示
    public TextMesh scoreMax;

    public SpriteRenderer timeSpr;     //开始游戏倒计时
    int timeNum = 3;

    public GameObject pauseLayer;
    public GameObject overLayer;
    public Image im_tip;

    private Text T_coin;

    GameObject[] allBtns;
    void Awake()
    {
        int randNum = StaticData.TeamSkin2;
        StaticData.g_weather = (byte)Random.Range(0, 3);
        while (true)
        {
            randNum = Random.Range(0, StaticData.TEAM_NUMS);
            if (randNum != StaticData.TeamSkin2 && randNum != StaticData.TeamSkin1)
            {
                StaticData.TeamSkin2 = (byte)randNum;
                break;
            }
        }
        Debug.Log("AI team= " + StaticData.TeamSkin2);

        T_coin = transform.Find("T_coin").GetComponent<Text>();
        T_coin.gameObject.SetActive(false);
    }
    void Start()
    {
        Time.timeScale = 1;
        Physics2D.gravity = new Vector2(0, StaticData.g_gravity);

        StaticData.g_gameEnd = false;

        GameManager.GetInstance();
        AudioManager.Instance.PlayBgAudio(2);

        ShowScore();
        scoreMax.text = SaveData.ScoreMax.ToString();

        StartCoroutine(TimeShow());
        timeSpr.sprite = MyTools.LoadSprite("Prefabs/Num/timeNum/" + timeNum.ToString());

        Slider musicSlider = pauseLayer.transform.Find("S_music").GetComponent<Slider>();
        musicSlider.value = SaveData.MusicValue;
        Slider soundSlider = pauseLayer.transform.Find("S_sound").GetComponent<Slider>();
        soundSlider.value = SaveData.SoundValue;

        if (StaticData.g_score != 0)
        {
            im_tip.gameObject.SetActive(false);
        }

        allBtns = new GameObject[4];
        allBtns[0] = transform.Find("btn_left").gameObject;
        allBtns[1] = transform.Find("btn_right").gameObject;
        allBtns[2] = transform.Find("btn_drop").gameObject;
        allBtns[3] = transform.Find("btn_pause").gameObject;
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
    /// <summary>
    /// 单人模式重新开始
    /// </summary>
    public void OnReSetScene()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        StaticData.g_gameEnd = false;
        StaticData.g_score = 0;

        //Time.timeScale = 1;
        SceneManager.LoadScene(StaticData.SCENENAME_CHALLENGE);
    }
    public void OnReturn()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        SceneManager.LoadScene(StaticData.SCENENAME_MAIN);
    }
    /// <summary>
    /// 单人模式 继续
    /// </summary>
    public void OnNext()
    {       
        SceneManager.LoadScene(StaticData.SCENENAME_CHALLENGE);
    }
    public void OnLeft()
    {
        GameManager.GetInstance().TeamBack1();
    }
    public void OnRight()
    {
        GameManager.GetInstance().TeamFront1();
    }
    public void OnDrop()
    {
        GameManager.GetInstance().OnDoropBall(0);
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
    public void SetResult(bool _state)
    {
        if (StaticData.g_gameEnd)
        {
            return;
        }
        StaticData.g_gameEnd = true;
        HideAllBtn();

        Invoke("HideTip", 0.5f);
    }
    public void AddNewScore()
    {
        if (StaticData.g_gameEnd)
        {
            return;
        }
        HideAllBtn();

        StaticData.g_gameEnd = true;
        StaticData.g_score += 1;
        ShowScore();
        Invoke("DelayToNext", 0.5f);

        //挑战模式 金币增加计算 金币=（N/3）X5
       int addNum= Mathf.CeilToInt(StaticData.g_score / 3.0f) * 5;
       SaveData.CoinNum += addNum;
       SaveData.SaveCoinData();

       T_coin.text = addNum.ToString();
       T_coin.gameObject.SetActive(true);
      
    }
    /// <summary>
    /// 胜利自动跳转下一关
    /// </summary>
    void DelayToNext()
    {
        OnNext();
    }
    void HideAllBtn()
    {
        SetAllBtnState(false);

        im_tip.gameObject.SetActive(true);
        Animator amt = im_tip.GetComponent<Animator>();
        amt.enabled = true;
        amt.speed = 3;
      
    }
    void SetAllBtnState(bool _state)
    {
        for (int i = 0; i < 4; i++)
        {
            allBtns[i].SetActive(_state);
        }
    }
    void HideTip()
    {
        im_tip.gameObject.SetActive(false);

        GameManager.GetInstance().SetSceneGray();
        //T_result.text = "你输了";

        AudioManager.Instance.PlayEffectAudio(5, transform);

        overLayer.SetActive(true);
        Image im_num1 = overLayer.transform.Find("ScoreNum/Num1").GetComponent<Image>();
        Image im_num2 = overLayer.transform.Find("ScoreNum/Num2").GetComponent<Image>();
        if (StaticData.g_score < 10)
        {
            im_num2.gameObject.SetActive(false);
            im_num1.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + StaticData.g_score.ToString());
        }
        else if (StaticData.g_score < 100)
        {
            int num1 = StaticData.g_score / 10;
            int num2 = StaticData.g_score % 10;

            im_num1.transform.localPosition = new Vector3(-27, 0, 0);
            im_num2.transform.localPosition = new Vector3(27, 0, 0);

            im_num1.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num1.ToString());
            im_num2.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num2.ToString());
        }
        else
        {
            im_num1.transform.localPosition = new Vector3(-27, 0, 0);
            im_num2.transform.localPosition = new Vector3(27, 0, 0);

            im_num1.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/9");
            im_num2.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/9");
        }

        if (StaticData.g_score > SaveData.ScoreMax)
        {
            SaveData.ScoreMax = StaticData.g_score;
            SaveData.SaveMaxScoreData();
        }

    }
    void ShowScore()
    {
        if (StaticData.g_score < 10)
        {
            scoreSpr[0].sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum1/" + StaticData.g_score.ToString());
            scoreSpr[1].gameObject.SetActive(false);
        }
        else if (StaticData.g_score < 100)
        {
            Vector3 pos = scoreSpr[0].transform.localPosition;
            pos.x = -0.4f;
            scoreSpr[0].transform.localPosition = pos;
            pos.x = 0.4f;
            scoreSpr[1].transform.localPosition = pos;


            scoreSpr[0].sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum1/" + StaticData.g_score.ToString());
            scoreSpr[1].sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum1/" + StaticData.g_score.ToString());
        }
        else
        {
            scoreSpr[0].sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum1/9");
            scoreSpr[1].sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum1/9");
        }

    }

}
