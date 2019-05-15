using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 全局变量 静态数据
/// </summary>
public class StaticData 
{ 
    /////////////参数设定////////////////////////

    public static float g_gravity = -20;                 //重力参数

    public static float g_forceFrontNum = 10;            //跳跃力 向前
    public static float g_forceUpFNum = 10;              //跳跃力 向上   向前跳
    public static float g_forceBackNum = 10;             //跳跃力 向后
    public static float g_forceUpBNum = 10;              //跳跃力 向上   向后跳
   

    public static float g_cdTime = 1.0f;                 //跳跃间隔时间
    public static float g_massCenter = -0.6f;            //重心

    public static float g_bodyMassNum = 1;               //身体质量
    public static float g_bodyInertia = 0.15f;           //身体惯性张量


    public static float g_armMassNum = 0.025f;           //手臂质量
    public static float g_angularNum = 3.8f;             //旋转力矩
    public static float g_armInertia = 0.2f;             //手臂转动惯量

    public static float g_bodyDrag = 1.0f;               //身体阻力
    public static float g_armDrag = 0;                   //手臂阻力


    public static float g_ballMassNum = 0.3f;            //球质量
    public static float g_velocityNum = 35;              //球击中速度


    public static float[] testNum = { -20, 8.5f, 11.0f, 4.5f, 8.0f, 1, -0.6f, 1.0f, 0.15f, 0.025f, 3.8f, 0.2f, 1.0f, 0, 0.3f,35 };


    

   






    public static int g_hatSpeed = 20;
    public static float g_headMassNum = 0.2f;



    /////////////参数设定////////////////////////

    /// <summary>
    /// 角色跳跃施加力数值 前 中 后
    /// </summary>
    public static readonly float[] Force_FX = { 8.5f, 5.0f, 4.5f };
    public static readonly float[] Force_FY = { 11.0f, 8.8f, 8.0f };
    public static readonly float[] Force_BX = { 4.5f, 5.0f, 8.5f };
    public static readonly float[] Force_BY = { 8.0f, 8.8f, 11.0f};

    /// <summary>
    /// 游戏设计分辨率 宽度
    /// </summary>
    public const int SIZE_WIDTH = 1920; 
    /// <summary>
    /// 游戏设计分辨率 高度
    /// </summary>
    public const int SIZE_HEIGHT = 1080;

    /// <summary>
    /// 场景名称
    /// </summary>
    public const string SCENENAME_DOUBLE = "DoubleScene";
    public const string SCENENAME_MAIN = "MainScene";
    public const string SCENENAME_MATCH = "MatchScene";
    public const string SCENENAME_READY = "ReadyScene";
    public const string SCENENAME_CHALLENGE = "ChanllengeScene";
    public const string SCENENAME_MATCHWIN = "MatchWinScene";


    /// <summary>
    /// 杯赛次数  3分获胜 最后一局 5分获胜
    /// </summary>
    public static readonly int[] MATCH_TIMES = { 5, 7, 9 };

    public static readonly int[] MATCH_PRIZES = { 20, 50, 100 };
    /// <summary>
    /// 全部角色数量 16
    /// </summary>
    public const byte TEAM_NUMS = 16;

    public static readonly int[] SHOP_COIN = { 10, 33, 96, 156, 252, 480 };
    public static readonly int[] SHOP_COST = { 1, 3, 8, 12, 18, 30 };

   // public static readonly string[] MATCH_NAME = { "俱乐部","超级联赛","世界杯"};
    ////////全局变量////////////////////////////////////////////////////////////////////

    /// <summary>
    ///  //游戏模式 1杯赛模式 2挑战模式 3双人模式
    /// </summary>
    public static byte g_gameMode = 0;
    /// <summary>
    /// (0-2)比赛模式 三种赛事 // 5 7 9 三种比赛
    /// </summary>
    public static byte g_matchType = 0; 

    /// <summary>
    /// 本局比赛结束状态 
    /// </summary>
    public static bool g_gameEnd = false;
    /// <summary>
    /// 本局比赛开始状态 
    /// </summary>
    public static bool g_gameStart = false;
    /// <summary>
    /// //游戏分数 3分获胜
    /// </summary>
    public static byte g_score = 0;
    /// <summary>
    /// 游戏场次
    /// </summary>
    public static byte g_times = 0;

    public static byte[] g_aiSkins = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    /// <summary>
    /// 当前角色解锁新的比赛模式
    /// </summary>
    public static byte g_newMatchTeam = 100;


    public static byte g_weather = 0;
    public static bool isFirstLogin = true;

    /// <summary>
    /// 队伍 1 使用的皮肤id
    /// </summary>
    public static byte TeamSkin1 = 0;
    public static byte TeamSkin2 = 0;

}
public enum LayerMask
{
    /// <summary>
    /// 己方身体 所在layer 值
    /// </summary>
    OWNBODY = 8,
    /// <summary>
    /// 敌方身体 所在layer 值
    /// </summary>
    ENEMYBODY = 9,
    /// <summary>
    /// 己方手臂 所在layer 值
    /// </summary>
    OWNARM = 10,
    /// <summary>
    /// 敌方手臂 所在layer 值
    /// </summary>
    ENEMYARM = 11,
    /// <summary>
    /// 墙体 所在layer 值
    /// </summary>
    WALL = 12,
    /// <summary>
    /// 地面 所在layer 值
    /// </summary>
    FLOOR = 13,
    /// <summary>
    /// 球体 所在layer 值
    /// </summary>
    BALL = 14,
}