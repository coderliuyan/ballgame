using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager ms_Ptr = null;
    public static GameManager GetInstance()
    {
        if (ms_Ptr == null)
        {
            ms_Ptr = new GameManager();
            ms_Ptr.Start();
        }
        return ms_Ptr;
    }

    //public GameObject[] ownArray = new GameObject[3];
    //public GameObject[] enemyArray = new GameObject[3];
    //public GameObject ballGo;

    public GameChallengeUI gcUI;
    public GameDoubleUI gdUI;
    public GameMatchUI gmUI;

    public RoleManager roleMng;
    void Start()
    {
        /* ballGo = GameObject.Find("ball");*/
    }
    public void SetCatchState(int _id, bool _state)
    {
        if (StaticData.g_gameMode == 3)
        {
            return;
        }
        if(GetRoleMng())
        {
            roleMng.isCatchBall = _state;
        }
    }

    bool GetGameMatchUI()
    {
        if (gmUI == null)
        {
            GameObject chatGo = GameObject.Find("MainCanvas");
            if (chatGo == null)
            {
                Debug.LogError("mainGo is not exist");
                return false;
            }
            gmUI = chatGo.GetComponent<GameMatchUI>();
            if (gmUI == null)
            {

                Debug.LogError("gmUI is not exist");
                return false;
            }
        }

        return true;

    }
    bool GetGameChallengeUI()
    {
        if (gcUI == null)
        {
            GameObject chatGo = GameObject.Find("MainCanvas");
            if (chatGo == null)
            {
                Debug.LogError("mainGo is not exist");
                return false;
            }
            gcUI = chatGo.GetComponent<GameChallengeUI>();
            if (gcUI == null)
            {
                Debug.LogError("gcUI is not exist");
                return false;
            }
        }
        return true;

    }
    bool GetGameDoubleUI()
    {
        if (gdUI == null)
        {
            GameObject chatGo = GameObject.Find("MainCanvas");
            if (chatGo == null)
            {
                Debug.LogError("mainGo is not exist");
                return false;
            }
            gdUI = chatGo.GetComponent<GameDoubleUI>();
            if (gdUI == null)
            {
                Debug.LogError("gdUI is not exist");
                return false;
            }
        }

        return true;

    }
    public void SetGameResult(bool state)
    {
        AudioManager.Instance.PlayEffectAudio(6, null);
        if (StaticData.g_gameMode == 1)
        {
            if (GetGameMatchUI())
            {
                gmUI.SetScore(state);
            }
        }
        else if (StaticData.g_gameMode == 2)
        {
            if (GetGameChallengeUI())
            {
                if (state)
                {
                    gcUI.AddNewScore();
                }
                else
                {
                    gcUI.SetResult(false);
                }
            }
        }
        else
        {
            if (GetGameDoubleUI())
            {
                gdUI.SetScore(state);
            }

        }
    }


    bool GetRoleMng()
    {
        if (roleMng == null)
        {
            GameObject chatGo = GameObject.Find("RoleManager");
            if (chatGo == null)
            {
                Debug.LogError("RoleManager is not exist");
                return false;
            }
            else
            {
                roleMng = chatGo.GetComponent<RoleManager>();
                return true;
            }
        }
        else
        {
            return true;
        }
    }
    public void StartGame()
    {
        AudioManager.Instance.PlayEffectAudio(7, null);
        if (GetRoleMng())
        {
            roleMng.FreeBall();
        }
    }
    public void TeamFront1()
    {
        if (GetRoleMng())
        {
            roleMng.TeamFront1();
        }
    }
    public void TeamBack1()
    {
        if (GetRoleMng())
        {
            roleMng.TeamBack1();
        }
    }
    public void TeamFront2()
    {
        if (GetRoleMng())
        {
            roleMng.TeamFront2();
        }
    }
    public void TeamBack2()
    {
        if (GetRoleMng())
        {
            roleMng.TeamBack2();
        }
    }
    public void OnDoropBall(byte _id)
    {
        if (GetRoleMng())
        {
            roleMng.DorpBall(_id);
        }
    }
    public void SetRoleArmFlip(int _id)
    {
        if (GetRoleMng())
        {
            roleMng.SetArmFlip(_id);
        }
    }
    public void SetSceneGray()
    {
        GameObject builds = GameObject.Find("Building");
        SpriteRenderer[] srs = builds.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < srs.Length; i++)
        {
            srs[i].material.shader = Shader.Find("Sprites/Gray");
        }
    }
}
