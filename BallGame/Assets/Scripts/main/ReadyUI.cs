using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReadyUI : MonoBehaviour {

    public Transform teamContent;

    private GameObject lastTeam;
    void Start()
    {
        //StaticData.g_matchType = 2;
        //StaticData.g_times = 7;

        InitTeamMsg();
    }
    void InitTeamMsg()
    {
        GameObject prefab = Resources.Load("Prefabs/UI/MtachTeam", typeof(GameObject)) as GameObject;
        for(int i=0;i<StaticData.MATCH_TIMES[StaticData.g_matchType];i++)
        {
            GameObject dcGo = Instantiate(prefab);
            dcGo.transform.SetParent(teamContent, false);

            Image im_bg = dcGo.transform.Find("Bg").GetComponent<Image>();
            im_bg.sprite = MyTools.LoadSprite(string.Format("Prefabs/teamBg/im_teamBg{0}" , i+1));

            Image im_type = dcGo.transform.Find("type").GetComponent<Image>();
            im_type.sprite = MyTools.LoadSprite(string.Format("Prefabs/match/im_matchLeve{0}", Mathf.Min(4 ,i/2+1)));


            //Text T_name = dcGo.transform.FindChild("T_name").GetComponent<Text>();
            // string nameStr = (StaticData.g_aiSkins[i] + 1).ToString().PadLeft(2, '0');
            //T_name.text = teamData.GetString(nameStr,"name");
            Image nameIm = dcGo.transform.Find("name").GetComponent<Image>();
            nameIm.sprite = MyTools.LoadSprite(string.Format("Prefabs/teamName/im_teamName{0}", StaticData.g_aiSkins[i] + 1));

            GameObject grayGo = dcGo.transform.Find("Gray").gameObject;
            if(i<StaticData.g_times-1)
            {
                grayGo.SetActive(true);
            }
            else if(i==(StaticData.g_times-1))
            {
                lastTeam = grayGo;
                grayGo.SetActive(false);
                Invoke("DelayShowEliminate", 0.3f);
            }         
            else
            {
                grayGo.SetActive(false);
            }
            if(i!=StaticData.g_times)
            {
                GameObject lightGo = dcGo.transform.Find("light").gameObject;
                lightGo.SetActive(false);
            }

            int randNum = Random.Range(0, 3)+1;
            int roleId = (StaticData.g_aiSkins[i] + 1) * 100 + randNum;
            Image body1 = dcGo.transform.Find("body").GetComponent<Image>();
            body1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_2", roleId));
            Image head1 = body1.transform.Find("head").GetComponent<Image>();
            head1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_1", roleId));
            Image arm1 = body1.transform.Find("arm").GetComponent<Image>();
            arm1.sprite = MyTools.LoadSprite(string.Format("Prefabs/role/role{0}_3", roleId));

            Image badge = dcGo.transform.Find("badge").GetComponent<Image>();
            badge.sprite = MyTools.LoadSprite(string.Format("Prefabs/Badge/im_badge{0}", StaticData.g_aiSkins[i] + 1));

        }


        GameObject prefab2 = Resources.Load("Prefabs/UI/MtachPrize", typeof(GameObject)) as GameObject;
        GameObject prizeGo = Instantiate(prefab2);
        prizeGo.transform.SetParent(teamContent, false);
        Image matchName = prizeGo.transform.Find("name").GetComponent<Image>();
        matchName.sprite = MyTools.LoadSprite(string.Format("Prefabs/match/im_matchName{0}", StaticData.g_matchType + 1));
        Image prize2 = prizeGo.transform.Find("prize").GetComponent<Image>();
        matchName.SetNativeSize();
        prize2.sprite = MyTools.LoadSprite(string.Format("Prefabs/match/im_matchPrize{0}", StaticData.g_matchType + 1));

        Image nowNum = prizeGo.transform.Find("nowNum").GetComponent<Image>();
        nowNum.sprite = MyTools.LoadSprite(string.Format("Prefabs/Num/matchNum/{0}", StaticData.g_times));
        Image maxNum = prizeGo.transform.Find("maxNum").GetComponent<Image>();
        maxNum.sprite = MyTools.LoadSprite(string.Format("Prefabs/Num/matchNum/{0}", StaticData.MATCH_TIMES[StaticData.g_matchType]));

        if(StaticData.g_times>2)
        {
            teamContent.transform.localPosition = new Vector3(-(StaticData.g_times - 2) * 300, 0, 0);
        }


    }
    void DelayShowEliminate()
    {
        AudioManager.Instance.PlayEffectAudio(4, transform);
        if(lastTeam!=null)
        {
            lastTeam.SetActive(true);
            Animator amt = lastTeam.transform.Find("eliminate").GetComponent<Animator>();
            amt.enabled = true;
        }
    }
    public void OnReturn()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        SceneManager.LoadScene(StaticData.SCENENAME_MAIN);
    }
    /// <summary>
    /// 游戏开始 确定AI 队伍皮肤
    /// </summary>
    public void OnStart()
    {
        AudioManager.Instance.PlayEffectAudio(0, transform);
        StaticData.TeamSkin2 = StaticData.g_aiSkins[StaticData.g_times];
        StaticData.g_weather = (byte)Random.Range(0, 3);

        SceneManager.LoadScene(StaticData.SCENENAME_MATCH);
    }
    public void OnLeft()
    {

    }
    public void OnRight()
    {

    }
}
