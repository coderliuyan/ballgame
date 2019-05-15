using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameMatchUI : MonoBehaviour 
{
    public SpriteRenderer[] scoreSpr;

    public SpriteRenderer timeSpr;       //开始游戏倒计时
    int timeNum = 3;

    public GameObject pauseLayer;
    public GameObject overLayer;          //比赛失败显示
    //public GameObject winLayer;         //比赛胜利显示
    public GameObject nextLayer;          //当前回合胜利显示

    public Image im_tip;

    GameObject[] allBtns;


	void Start () 
    {    
        Time.timeScale = 1;
        Physics2D.gravity = new Vector2(0, StaticData.g_gravity);
        StaticData.g_gameEnd = false;

        GameManager.GetInstance();
        AudioManager.Instance.PlayBgAudio(1);

        ShowScore();


        StartCoroutine(TimeShow());
        timeSpr.sprite = MyTools.LoadSprite("Prefabs/Num/timeNum/" + timeNum.ToString());

        if (StaticData.g_score != 0)
        {
            im_tip.gameObject.SetActive(false);
        }
        else
        {
            if (StaticData.g_times == StaticData.MATCH_TIMES[StaticData.g_matchType] - 1)
            {
                im_tip.gameObject.SetActive(true);
                im_tip.sprite = MyTools.LoadSprite("Prefabs/im_tip2");
                im_tip.SetNativeSize();
            }
            else
            {
                im_tip.gameObject.SetActive(true);
                im_tip.sprite = MyTools.LoadSprite("Prefabs/im_tip1");
                im_tip.SetNativeSize();
            }
        }
        Slider musicSlider = pauseLayer.transform.Find("S_music").GetComponent<Slider>();
        musicSlider.value = SaveData.MusicValue;
        Slider soundSlider = pauseLayer.transform.Find("S_sound").GetComponent<Slider>();
        soundSlider.value = SaveData.SoundValue;

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
    /// 单人模式重新开始
    /// </summary>
    public void OnReSetScene()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        StaticData.g_gameEnd = false;
        StaticData.g_score = 0;
        SceneManager.LoadScene(StaticData.SCENENAME_MATCH);
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

         SceneManager.LoadScene(StaticData.SCENENAME_MATCH);
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

  public void SetResult(bool _state)
    {
        StaticData.g_gameEnd = true;     
        if(_state)
        {
            AudioManager.Instance.PlayEffectAudio(9, transform);

            if(SaveData.TeamMatchLevel[StaticData.TeamSkin1]< StaticData.g_matchType+1)//012 
            {
                //解锁新的比赛
                StaticData.g_newMatchTeam = StaticData.TeamSkin1;
                SaveData.TeamMatchLevel[StaticData.TeamSkin1] = StaticData.g_matchType + 1;
                SaveData.SaveMatchLevelData(StaticData.TeamSkin1);

                //通关世界杯
                if(StaticData.g_matchType==2)
                {
                    GetFirstCompletePrize();
                }
            }        
        }
        else
        {
            AudioManager.Instance.PlayEffectAudio(5, transform);

            int num1 = StaticData.g_score / 10;
            int num2 = StaticData.g_score % 10;

            //T_result.text = "你输了";       
            GameManager.GetInstance().SetSceneGray();
            overLayer.SetActive(true);
            Image im_num1 = overLayer.transform.Find("ScoreNum/Num1").GetComponent<Image>();
            Image im_num2 = overLayer.transform.Find("ScoreNum/Num2").GetComponent<Image>();
            im_num1.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num1.ToString());
            im_num2.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num2.ToString());

            Image im_badge1 = overLayer.transform.Find("Im_team1").GetComponent<Image>();
            Image im_badge2 = overLayer.transform.Find("Im_team2").GetComponent<Image>();
            im_badge1.sprite = MyTools.LoadSprite("Prefabs/Badge/im_badge" +(StaticData.TeamSkin1+1).ToString());
            im_badge2.sprite = MyTools.LoadSprite("Prefabs/Badge/im_badge" + (StaticData.TeamSkin2 + 1).ToString());
        }
    }
    void HideTip()
  {
      im_tip.gameObject.SetActive(false);

      int num1 = StaticData.g_score / 10;
      int num2 = StaticData.g_score % 10;
      int winNum = 3;
      if (StaticData.g_times == StaticData.MATCH_TIMES[StaticData.g_matchType] - 1)
      {
          winNum = 5;
      }

      if (num1 >= winNum)
      {
          StaticData.g_times++;
          if (StaticData.g_times >= StaticData.MATCH_TIMES[StaticData.g_matchType])
          {
              SetResult(true);         
          }
         // else
          {
              //当前回合胜利
              StaticData.g_score = 0;
              nextLayer.SetActive(true);
              // T_result.text = "你赢了";    
              Image im_num1 = nextLayer.transform.Find("ScoreNum/Num1").GetComponent<Image>();
              Image im_num2 = nextLayer.transform.Find("ScoreNum/Num2").GetComponent<Image>();
              im_num1.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num1.ToString());
              im_num2.sprite = MyTools.LoadSprite("Prefabs/Num/scoreNum2/" + num2.ToString());

              Image im_badge1 = nextLayer.transform.Find("Im_team1").GetComponent<Image>();
              Image im_badge2 = nextLayer.transform.Find("Im_team2").GetComponent<Image>();
              im_badge1.sprite = MyTools.LoadSprite("Prefabs/Badge/im_badge" + (StaticData.TeamSkin1 + 1).ToString());
              im_badge2.sprite = MyTools.LoadSprite("Prefabs/Badge/im_badge" + (StaticData.TeamSkin2 + 1).ToString());


              //杯赛模式没胜利一次 加金币10
              SaveData.CoinNum += 10;
              SaveData.SaveCoinData();
             
              Text T_coin = nextLayer.transform.Find("T_coin").GetComponent<Text>();
              T_coin.text = "10";
          }
      }
      else if (num2 >= winNum)
      {
          SetResult(false);
      }
      else
      {
          Invoke("DelayToNext", 0.5f);
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
      if (win)
      {
          StaticData.g_score += 10;
      }
      else
      {
          StaticData.g_score += 1;
      }
    
      ShowScore();
  }
  /// <summary>
  /// 回合结束自动跳转下一关
  /// </summary>
  void DelayToNext()
  {
      OnNext();
  }
   public void OnNextRound()
  {
      if (StaticData.g_times >= StaticData.MATCH_TIMES[StaticData.g_matchType])
      {
          SceneManager.LoadScene(StaticData.SCENENAME_MATCHWIN);
      }
      else
      {
          SceneManager.LoadScene(StaticData.SCENENAME_READY);
      }
    
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
        for (int i = 0; i < 4; i++)
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
  void GetFirstCompletePrize()
  {
		//问题的地方
      return;
      if (SaveData.IsFristMatchComplete)
      {
          SaveData.IsFristMatchComplete = false;
          SaveData.SaveFirstMatch();

          TableValue teamData = XmlHelper.Instance.ReadFile("TeamMsg");
          if (SaveData.TeamUnlockState[9])
          {
              //队伍已拥有 奖励金币

              int prizeValue = teamData.GetInt("10", "unlock");
              //TipEffectMng.GetInstance().ShowTipMsg("首次通关世界杯，金币+" + prizeValue, transform);

              SaveData.CoinNum += prizeValue;
              SaveData.SaveCoinData();
          }
          else
          {
              string teamName = teamData.GetString("10", "name");
              //TipEffectMng.GetInstance().ShowTipMsg("首次通关世界杯，奖励新队伍:" + teamName, transform);

              SaveData.TeamUnlockState[9] = true;
              SaveData.SaveTeamData(9);
          }
      }
  }
}
