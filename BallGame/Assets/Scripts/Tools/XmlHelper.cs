using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

/// <summary>
/// xml 读取方法
/// </summary>
public class XmlHelper
{
    private static XmlHelper instance = null;
    public static XmlHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new XmlHelper();
            }
            return instance;
        }
      
    }
    private Dictionary<string, TableValue> tableDict = null;
    private XmlHelper()
    {
        tableDict = new Dictionary<string, TableValue>();
    }
 
    public void LoadFile(string filename)
    {
		string path = "";
#if !UNITY_EDITOR
         path = Application.dataPath +"/"+ filename;
#endif
#if UNITY_EDITOR
        path = filename;
#endif
		if (tableDict.ContainsKey(filename))
        {
			Debug.LogWarning(filename + " 已经加载过");
            return;
        }

		Debug.Log ("load file path ==" + path);

		LoadXmlFile(filename);
       
    }
    public TableValue ReadFile(string filename)
    {

		if (!tableDict.ContainsKey(filename))
        {
			Debug.LogError(filename + " 配置文件未加载");
            return null;
        }    
		return tableDict[filename];
    }
    void LoadXmlFile(string filename)
    {
		Debug.Log (" load xml file   filename = " + filename);

		string data = ((TextAsset)Resources.Load(filename)).text.ToString();


		Debug.Log ("data ======= " + data);
        //XDocument xmlDoc = new XDocument();
        XDocument xmlDoc= XDocument.Parse(data);

        TableValue tb = new TableValue();
        foreach (XElement elt in xmlDoc.Element("plist").Elements())
        {
            //Debug.Log(":::" + elt.Name);
             LineValue lv = new LineValue();
             IEnumerable<XElement> newElementEleColl = elt.Elements();
            foreach (XElement element in newElementEleColl)
            {            
                lv.AddItem(element.Name.ToString(), element.Value);
                //Debug.Log(":::" + element.Name +","+ element.Value);
            }
            string eleName = elt.Name.ToString().Remove(0,8);
            tb.AddLine(eleName, lv);
        }    
        tableDict.Add(filename, tb);
    }
    public void ClearAll()
    {
        tableDict.Clear();
    }
}
public class LineValue
{
    public string lineName;
    private Dictionary<string, string> dataValue = new Dictionary<string, string>();

    public void AddItem(string key, string value)
    {
        if (dataValue.ContainsKey(key))
        {
            Debug.LogError(key + "  is a  same key");
        }
        else
        {
            dataValue.Add(key, value);
        }
    }
     public string GetString(string key)
    {
         if(!dataValue.ContainsKey(key))
         {
             Debug.LogError("LineValue is not contain key="+key);
             return null;
         }
        return dataValue[key];
    }
//     public string this[string key]
//     {
//         get 
//         {
//             if (dataValue.ContainsKey(key))
//             {
//                 return dataValue[key]; 
//             }
//             else
//             {
//                 Debug.LogError(key + " is not exist");
//                 return null;
//             }
//            
//         }
// 
//         set { AddItem(key, value); }
//     }
//     public IEnumerator GetEnumerator()
//     {
//         foreach (KeyValuePair<string, string> item in dataValue)
//         {
//             yield return item;
//         }
//     }
}
public class TableValue
{
    private Dictionary<string, LineValue> dataValue ;
    public TableValue()
    {
        dataValue = new Dictionary<string, LineValue>();
    }
    public void AddLine(string key, LineValue value)
    {
        if (dataValue.ContainsKey(key))
        {
            Debug.LogError(key + "  is a  same key");
        }
        else
        {
            value.lineName = key;
            dataValue.Add(key, value);
        }
    }
    public int GetCountNum()
    {
        return dataValue.Count;
    }
    public string GetString(string key,string value)
    {
         if(!dataValue.ContainsKey(key))
         {
             Debug.LogError("TableValue is not contain key=" + key);
             return null;
         }
         return dataValue[key].GetString(value);
    }
    public int GetInt(string key,string value)
    {
        string str = GetString(key, value);
        if(str==null)
        {
            return 0;
        }
        int num = 0;
        if(int.TryParse(str,out num))
        {
            return num;
        }
        else
        {
            Debug.LogError("Wrong format  Int//" + str);
            return 0;
        }
    }
    public float GetFloat(string key, string value)
    {
        string str = GetString(key, value);
        if (str == null)
        {
            return 0;
        }
        float num = 0;
        if (float.TryParse(str, out num))
        {
            return num;
        }
        else
        {
            Debug.LogError("Wrong format  Float//" + str);
            return 0;
        }
    }
//     public LineValue this[string key]
//     {
//         get 
//         {
//             if(dataValue.ContainsKey(key))
//             {
//                 return dataValue[key]; 
//             }
//             else
//             {
//                 Debug.LogError(key + " is not exist");
//                 return null;
//             }
//         }
//         set { AddLine(key, value); }
//     }
//     public IEnumerator GetEnumerator()
//     {
//         foreach (var item in dataValue)
//         {
//             yield return item.Value;
//         }
//     }
}