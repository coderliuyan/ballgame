using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏中需要保存的数据
/// </summary>
public class SaveData
{
    //保存信息
    private const string SAVE_MAXSCORE = "ScoreMax";
    private const string SAVE_COIN = "CoinNum";
    private const string SAVE_TEAMSKIN = "TeamSkin1";
    private const string SAVE_UNLOCKSTATE = "TeamUnlockState";
    private const string SAVE_MATCHLEVEL = "TeamMatchLevel";

    private const string SAVE_SOUNDVALUE = "SoundValue";
    private const string SAVE_MUSICVALUE = "MusicValue";
    private const string SAVE_AUDIOSTATE = "AudioState";

    private const string SAVE_FIRSTCHARGE = "FirstCharge";
    private const string SAVE_FIRSTMATCH = "FirstMatch";
    ////////需要存储的游戏数据//////////////////////////////////////////////

    /// <summary>
    /// 挑战模式最高分
    /// </summary>
    public static int ScoreMax = 0;



    /// <summary>
    /// 角色购买状态
    /// </summary>
    public static bool[] TeamUnlockState ={true,false,false,false,false,false,false,false,
                                          false,false,false,false,false,false,false,false};
    /// <summary>
    /// 角色赛事通关状态
    /// </summary>
    public static int[] TeamMatchLevel = { 0, 0, 0, 0, 0, 0, 0, 0, 
                                           0, 0, 0, 0, 0, 0, 0, 0 };
    /// <summary>
    /// 金币数量
    /// </summary>
    public static int CoinNum = 0;

    /// <summary>
    /// 音效状态
    /// </summary>
    public static float SoundValue = 1;
    /// <summary>
    /// 音乐状态
    /// </summary>
    public static float MusicValue = 1;
    /// <summary>
    /// 声音状态
    /// </summary>
    public static bool AudioState = true;

    /// <summary>
    /// 首次充值状态  true 则奖励队伍 8 电商队
    /// </summary>
    public static bool IsFirstCharge = true;
    /// <summary>
    /// 首次通关世界杯 true 则奖励队伍 9 明星队
    /// </summary>
    public static bool IsFristMatchComplete = true;
    public static void LoadAllData()
    {
        ScoreMax = PlayerPrefs.GetInt(SAVE_MAXSCORE, 0);
        CoinNum = PlayerPrefs.GetInt(SAVE_COIN, 0);

        for (int i = 0; i < StaticData.TEAM_NUMS; i++)
        {
            if (i != 0)
            {
                TeamUnlockState[i] = PlayerPrefs.GetInt(SAVE_UNLOCKSTATE + i.ToString(), 0) == 1;
            }
            TeamMatchLevel[i] = PlayerPrefs.GetInt(SAVE_MATCHLEVEL + i.ToString(), 0);
        }

       // SoundValue = PlayerPrefs.GetFloat(SAVE_SOUNDVALUE, 1);
       // MusicValue = PlayerPrefs.GetFloat(SAVE_MUSICVALUE, 1);
        AudioState = PlayerPrefs.GetInt(SAVE_AUDIOSTATE, 0) == 0;

        IsFirstCharge = PlayerPrefs.GetInt(SAVE_FIRSTCHARGE, 0) == 0;
        IsFristMatchComplete = PlayerPrefs.GetInt(SAVE_FIRSTMATCH, 0) == 0;
    }
    /// <summary>
    /// 保存音乐状态
    /// </summary>
    public static void SaveAudioData()
    {
       // PlayerPrefs.SetFloat(SAVE_SOUNDVALUE, SoundValue);
      //  PlayerPrefs.SetFloat(SAVE_MUSICVALUE, MusicValue);
        PlayerPrefs.SetInt(SAVE_AUDIOSTATE, AudioState ? 0 : 1);
    }
    /// <summary>
    /// 保存挑战模式最高分
    /// </summary>
    public static void SaveMaxScoreData()
    {
        PlayerPrefs.SetInt(SAVE_MAXSCORE,ScoreMax);
    }
    /// <summary>
    /// 保存金币数量
    /// </summary>
    public static void SaveCoinData()
    {
        PlayerPrefs.SetInt(SAVE_COIN, CoinNum);
    }
    /// <summary>
    /// 保存队伍联赛模式进度
    /// </summary>
    /// <param name="_id"></param>
    public static void SaveMatchLevelData(int _id)
    {
        PlayerPrefs.SetInt(SAVE_MATCHLEVEL + _id, TeamMatchLevel[_id]);
    }
    /// <summary>
    /// 保存队伍解锁状态
    /// </summary>
    /// <param name="_id"></param>
    public static void SaveTeamData(int _id)
    {
        PlayerPrefs.SetInt(SAVE_UNLOCKSTATE + _id, 1);
    }
    /// <summary>
    /// 保存 首冲状态
    /// </summary>
    public static void SaveFirstCharge()
    {
        PlayerPrefs.SetInt(SAVE_FIRSTCHARGE, 1);
    }
    /// <summary>
    ///保存 首次通关世界杯 状态
    /// </summary>
    public static void SaveFirstMatch()
    {
        PlayerPrefs.SetInt(SAVE_FIRSTMATCH, 1);
    }
}
