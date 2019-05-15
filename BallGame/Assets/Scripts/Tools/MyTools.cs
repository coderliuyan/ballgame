using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTools
{
    public static Sprite LoadSprite(string _name)
    {
        GameObject go = Resources.Load<GameObject>(_name);
        if (go == null)
        {
            Debug.LogError("LoadSprite: " + _name);
            return null;
        }
        return go.GetComponent<SpriteRenderer>().sprite;
    }
    public static int Text_Length(string Text)
    {
        int len = 0;

        for (int i = 0; i < Text.Length; i++)
        {
            byte[] byte_len = System.Text.Encoding.Default.GetBytes(Text.Substring(i, 1));
            if (byte_len.Length > 1)
                len += 2;  //如果长度大于1，是中文，占两个字节，+2
            else
                len += 1;  //如果长度等于1，是英文，占一个字节，+1
        }

        return len;
    }
}
