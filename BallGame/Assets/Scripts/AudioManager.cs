using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
     string[] bgStr = { 
                                "bg_main",
                                "bg_match",
                                "bg_challenge",
                                "bg_double",
                                "bg_award"
                            };

     string[] effectStr = {
                                    "ef_click",
                                    "ef_clonLess",
                                    "ef_coinBuy",
                                    "ef_collsion",
                                    "ef_eliminate",
                                    "ef_fail",
                                    "ef_score",
                                    "ef_serve",
                                    "ef_unlock",
                                    "ef_win",
                                };


    private AudioSource bgAudio;


    public static AudioManager Instance;
	void Awake () 
    {
        if (StaticData.isFirstLogin)
        {
            bgAudio = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            Instance = this;

            StaticData.isFirstLogin = false;
        }
        else
        {
            Destroy(gameObject);
        }
	}
    /// <summary>
    /// 0 主界面
    /// 1 比赛
    /// 2 挑战
    /// 3 双人
    /// 4 领奖
    /// </summary>
    /// <param name="_id"></param>
    public void PlayBgAudio(int _id)
    {
        if(SaveData.AudioState==false)
        {
            return;
        }
        bgAudio.clip = Resources.Load<AudioClip>("Audio/" + bgStr[_id]);
        bgAudio.Play();
        bgAudio.loop = true;
        bgAudio.volume = SaveData.MusicValue;
    }
    public void ChangeBgVolume()
    {
        bgAudio.volume = SaveData.MusicValue;
    }
    public void StopBgAudio()
    {
        if(bgAudio!=null)
        {
            bgAudio.Stop();
        }
    }
    /// <summary>
    /// 0 按钮点击
    /// 1 金币不足
    /// 2 金币购买成功
    /// 3 橄榄球碰撞
    /// 4 球队淘汰
    /// 5 失败
    /// 6 触地得分
    /// 7 发球
    /// 8 解锁
    /// 9 胜利
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_tr"></param>
    public void PlayEffectAudio(int _id,Transform _tr)
    {
        if (SaveData.AudioState == false)
        {
            return;
        }

        GameObject go = new GameObject("Audio:" + _id);
        if(_tr!=null)
        {
            go.transform.position = _tr.position;
        }
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("Audio/" + effectStr[_id]);
        source.volume = SaveData.SoundValue;
        source.loop = false;
        source.Play();

        GameObject.DestroyObject(go, source.clip.length);
     
    }
}
