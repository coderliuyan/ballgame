using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SettingUI : MonoBehaviour {

   
    public InputField[] inputArray;
    private int arraySize;

    //public InputField[] array1;
	// Use this for initialization
	void Start ()
    {
#if !UNITY_EDITOR
        Debug.logger.logEnabled = false;
#endif

        arraySize = inputArray.Length;

        for (int i = 0; i < arraySize; i++)
        {
            inputArray[i].text = StaticData.testNum[i].ToString();
        }

        //array1[0].text = StaticData.g_hatSpeed.ToString();
       // array1[1].text = StaticData.g_headMassNum.ToString();
	}

    public void OnStartClick()
    {

      //int.TryParse(array1[0].text,out StaticData.g_hatSpeed);
     // float.TryParse(array1[1].text, out StaticData.g_headMassNum);
     // int.TryParse(array1[2].text, out StaticData.g_addForce);

        bool isRight = true;
        for (int i = 0; i < arraySize; i++)
        {
            isRight = float.TryParse(inputArray[i].text, out   StaticData.testNum[i]);
            if (isRight != true)
            {
                break;
            }
        }
        if(isRight)
        {
            StaticData.g_gravity = StaticData.testNum[0];         //重力


            StaticData.g_forceFrontNum = StaticData.testNum[1];
            StaticData.g_forceUpFNum = StaticData.testNum[2];
            StaticData.g_forceBackNum = StaticData.testNum[3];  
            StaticData.g_forceUpBNum = StaticData.testNum[4];

            StaticData.g_cdTime = StaticData.testNum[5];
            StaticData.g_massCenter = StaticData.testNum[6];   
            StaticData.g_bodyMassNum = StaticData.testNum[7];
            StaticData.g_bodyInertia = StaticData.testNum[8];

            StaticData.g_armMassNum = StaticData.testNum[9];
            StaticData.g_angularNum = StaticData.testNum[10];
            StaticData.g_armInertia = StaticData.testNum[11];

            StaticData.g_bodyDrag = StaticData.testNum[12];

            StaticData.g_armDrag = StaticData.testNum[13];

            StaticData.g_ballMassNum = StaticData.testNum[14];

            StaticData.g_velocityNum = StaticData.testNum[15];
          //  StaticData.g_cdTime = StaticData.testNum[8];          //跳跃cd            
          //  StaticData.g_massCenter = StaticData.testNum[9];     //身体重心

            SceneManager.LoadScene("TTTTT");
        }
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
}
