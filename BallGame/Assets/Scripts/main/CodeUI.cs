using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 兑换码界面
/// </summary>
public class CodeUI : MonoBehaviour 
{
    /// <summary>
    /// 兑换界面
    /// </summary>
    private GameObject codePanel;
    /// <summary>
    /// 兑换码输入框
    /// </summary>
    private InputField codeInput;

    private Button codeBtn;

	void Start () 
    {
        codeBtn = transform.GetComponent<Button>();
        codeBtn.onClick.AddListener(delegate()
        {
            this.SetLayerState(true);
        });
	}
    void SetLayerState(bool state)
    {
        if(codePanel==null)
        {
            GameObject prefab = Resources.Load("Prefabs/UI/CodePanel", typeof(GameObject)) as GameObject;
            codePanel = Instantiate(prefab, GameObject.Find("MainCanvas").transform);
            codePanel.transform.localPosition = Vector3.zero;
            codePanel.transform.localScale = Vector3.one;
            Button CloseBtn = codePanel.transform.Find("CloseBtn").GetComponent<Button>();
            CloseBtn.onClick.AddListener(delegate() { this.SetLayerState(false); });

            Button SureBtn = codePanel.transform.Find("SureBtn").GetComponent<Button>();
            SureBtn.onClick.AddListener(delegate() { this.OnSure(); });

            codeInput = codePanel.transform.Find("CodeInput").GetComponent<InputField>();
        }
        codePanel.SetActive(state);
        codeInput.text = "";
    }
    void OnSure()
    {
        Debug.Log("OnSure :" + codeInput.text);
        this.SetLayerState(false);
    }
}
