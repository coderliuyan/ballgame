using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机控制
/// </summary>
public class CameraControl : MonoBehaviour {

    //Camera maincamera;       //1920-3072  1080-1728      5.4f->8.64
    Transform[] allGo;
    Transform ballGo;
    BallControl ballScript;
    private int goNum=0;

    private float leftX = 0;
    private float rightX = 0;
    private float tempX = 0;   //摄像机坐标x
    private float edgeX = 0;   //摄像机最大移动位置   8.64->0  5.4->5.76   (8.64-s)*1.78
    private float edgeY = 2;   //摄像机最大移动位置Y  8.64->2  6.8->0   5.4->    2-1.087*(8.64-s)

    private GameObject ballEffect;
    bool stopMove = false;
	void Start () 
    {
        allGo = new Transform[6];
        for(int i=1;i<=3;i++)
        {
            GameObject go = GameObject.Find("RoleManager/Own" + i.ToString());
            if(go!=null)
            {
                allGo[goNum] = go.transform;
                goNum++;
            }
            GameObject go1 = GameObject.Find("RoleManager/AI" + i.ToString());
            if (go1 != null)
            {
                allGo[goNum] = go1.transform;
                goNum++;
            }
        }

        ballGo = GameObject.Find("RoleManager/Ball").transform;

        ballScript = ballGo.GetComponent<BallControl>();
        Camera.main.orthographicSize = 6.8f;

        ballEffect = GameObject.Find("RoleManager/im_edgeTip");
	}
    void LateUpdate()
    {
        if(!StaticData.g_gameStart)
        {
            return;
        }
        if (StaticData.g_gameEnd)
        {
            stopMove = true;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, 2 * Time.deltaTime);
            edgeX = (8.64f - Camera.main.orthographicSize) * 1.78f;
            edgeY = 2 - 1.087f * (8.64f - Camera.main.orthographicSize);
            if(ballGo.transform.position.x<0)
            {
                tempX = -edgeX;
            }
            else
            {
                tempX = edgeX;
            }
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(tempX, edgeY, 0), 4 * Time.deltaTime);
        }
        else
        {
            if(stopMove)
            {
                return;
            }
            leftX = 0;
            rightX = 0;
            for (int i = 0; i < goNum; i++)
            {
                if (allGo[i].localPosition.x < leftX)
                {
                    leftX = allGo[i].localPosition.x;
                }
                if (allGo[i].localPosition.x > rightX)
                {
                    rightX = allGo[i].localPosition.x;
                }

            }
            if (!ballScript.isCatched)
            {
                if (ballGo.localPosition.x < leftX)
                {
                    leftX = ballGo.localPosition.x;
                }
                if (ballGo.localPosition.x > rightX)
                {
                    rightX = ballGo.localPosition.x;
                }
                if (ballGo.transform.position.x <= Camera.main.transform.localPosition.x - Camera.main.orthographicSize * 1.77f)
                {
                    ballEffect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    ballEffect.transform.position = new Vector3(Camera.main.transform.localPosition.x - Camera.main.orthographicSize * 1.77f + 0.64f, ballGo.transform.position.y, 0);
                }
                else if (ballGo.transform.position.x >= Camera.main.transform.localPosition.x + Camera.main.orthographicSize * 1.77f)
                {
                    ballEffect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    ballEffect.transform.position = new Vector3(Camera.main.transform.localPosition.x + Camera.main.orthographicSize * 1.77f - 0.64f, ballGo.transform.position.y, 0);
                }
                else if (ballGo.transform.position.y >= Camera.main.orthographicSize)
                {
                    ballEffect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    ballEffect.transform.position = new Vector3(ballGo.transform.position.x, Camera.main.orthographicSize - 0.64f, 0);
                }
                else
                {
                    ballEffect.transform.position = new Vector3(0, 20, 0);
                }
            }
            else
            {
                ballEffect.transform.position = new Vector3(0, 20, 0);
            }

            Camera.main.orthographicSize = Mathf.Min(8.64f, Mathf.Max(ballGo.transform.position.y+1, 5.4f, (rightX - leftX + 4) * 0.28125f /*/2/ 16 * 9*/));
            edgeX = (8.64f - Camera.main.orthographicSize) * 1.78f;
            edgeY = 2 - 1.087f * (8.64f - Camera.main.orthographicSize);
            tempX = Mathf.Min(Mathf.Max(-edgeX, (leftX + rightX) * 0.5f), edgeX);
           // Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(tempX, 0, 0), 4 * Time.deltaTime);
            Camera.main.transform.localPosition = new Vector3(tempX, edgeY, 0);
        }
        //Debug.Log("orthographicSize=" + Camera.main.orthographicSize);       
    }
}
