using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MainUI : MonoBehaviour
{
    GameObject basePanel;
    Toggle audioToggle;
    void Awake()
    {
        //PlayerPrefs.DeleteAll();

        SaveData.LoadAllData();

        XmlHelper.Instance.LoadFile("TeamMsg");

        TableValue tb = XmlHelper.Instance.ReadFile("TeamMsg");
        Debug.Log(tb+"    !!!!!!!!!!!!!!!!!!!!!!");

        basePanel = transform.Find("BasePanel").gameObject;
        audioToggle = basePanel.transform.Find("Music").GetComponent<Toggle>();
      
    }
    void Start()
    {
        Time.timeScale = 1;

        audioToggle.isOn = SaveData.AudioState;
        AudioManager.Instance.PlayBgAudio(0);
    }


    public void SetBaseState(bool _state)
    {
        basePanel.SetActive(_state);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;

#else 
		Application.Quit();
#endif
        }
    }
    /// <summary>
    /// 选择游戏模式
    /// </summary>
    public void OnMode1()
    {
        try
        {
            AudioManager.Instance.PlayEffectAudio(0, transform);
            StaticData.g_gameMode = 1;

            InitGameData();

            MainManager.GetInstance().SetTeamChooseState(true);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }

    }
    public void OnMode2()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        StaticData.g_gameMode = 2;

        InitGameData();

        MainManager.GetInstance().SetTeamChooseState(true);
    }
    public void OnMode3()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        StaticData.g_gameMode = 3;

        InitGameData();

        MainManager.GetInstance().SetDoubleChooseState(true);
    }
    void InitGameData()
    {     
        //StaticData.g_newMatchTeam = 2; 
        //SaveData.TeamMatchLevel[2] = 1;

        StaticData.g_times = 0;
        StaticData.g_score = 0;
        StaticData.g_gameEnd = false;
        StaticData.g_gameStart = false;

        //SetBaseState(false);
    }
    public void OnAbout()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
    }
    public void OnValueChanged(bool _state)
    {
        //Debug.Log("OnValueChange " + _state);
        SaveData.AudioState = _state;
        SaveData.SaveAudioData();
        if(_state==false)
        {
            AudioManager.Instance.StopBgAudio();
        }
        else
        {
            AudioManager.Instance.PlayBgAudio(0);
        }
    }
}
