using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextAtlas : MonoBehaviour
{
    public Sprite[] textSpr;
    int textSize;

    public string wordStr="";

    GameObject[] numGo;
	// Use this for initialization
	void Start () 
    {
        textSize = Mathf.RoundToInt(textSpr[0].textureRect.width);
        Debug.Log("textSize=" + textSize);

        GameObject go = new GameObject("x_Image", typeof(Image));
        go.GetComponent<Image>().raycastTarget = false;
        go.transform.SetParent(transform);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
	}
    void SplitStr()
    {
        char[] chars = wordStr.ToCharArray();
        
        int num=chars.Length;
        if(num>0)
        {
            for(int i=0;i<num;i++)
            {
               // byte num0 = byte.Parse(chars[i].ToString());

            }           
        }
    }
}
