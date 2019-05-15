using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

    public class BatchCreatePrefab : EditorWindow
    {
        [MenuItem("MyTools/生成预制")]
        private static void PrefabWrapTool()
        {
            GetWindow<BatchCreatePrefab>().Show();
        }

        private int m_index = 0;
        private static string[] s_outputDirectories;

        private void OnEnable()
        {
            Selection.selectionChanged += Repaint;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= Repaint;
        }

        /// <summary>
        /// 更新目录
        /// </summary>
        private void UpdateDirectory()
        {
            s_outputDirectories = Directory.GetDirectories("Assets\\Resources", "*", SearchOption.AllDirectories);
            System.Array.Sort(s_outputDirectories);
            Repaint();
        }
        private static string RemoveDirectory = "Assets";
        private void OnGUI()
        {
            if (s_outputDirectories == null)
            {
                UpdateDirectory();
            }
            EditorGUILayout.BeginHorizontal();
            float defaultLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 60;
            EditorGUILayout.PrefixLabel("输出目录");
            m_index = EditorGUILayout.Popup(m_index, s_outputDirectories);
            EditorGUIUtility.labelWidth = defaultLabelWidth;
            if (GUILayout.Button("刷新", GUILayout.MaxWidth(40)))
            {
                UpdateDirectory();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PrefixLabel("忽略层级目录：");
            RemoveDirectory = EditorGUILayout.TextField(RemoveDirectory);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            int selectionCount = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets).Length;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("生成(" + selectionCount + ")", GUILayout.Height(20), GUILayout.MaxWidth(100)))
            {
                CreateAllSelectionPrefab(s_outputDirectories[m_index]);
            }
            EditorGUILayout.EndHorizontal();
        }

        private static void CreateAllSelectionPrefab(string outputPath)
        {
            Object[] allSelectedTextuers = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
            if (allSelectedTextuers.Length == 0)
            {
                Debug.LogWarning("未选中物体");
                return;
            }

            GameObject spriteObject = new GameObject();
            SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
            for (int i = 0; i < allSelectedTextuers.Length; i++)
            {
                string path = AssetDatabase.GetAssetPath(allSelectedTextuers[i]).Replace("/", "\\");
                string replaceString = path.Replace("/", "\\");
                if (!Directory.Exists(RemoveDirectory))
                {
                    Object.DestroyImmediate(spriteObject);
                    Debug.LogError("忽略层级目录错误");
                    return;
                }
                string replaceDirectory = replaceString.Replace(RemoveDirectory, null);//替换字符串
                int lastCharIndex = replaceDirectory.LastIndexOf("\\");//得到物体的下标
                string removeString = replaceDirectory.Remove(lastCharIndex, replaceDirectory.Length - lastCharIndex);
                string finalOutputPath = outputPath + removeString;
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                spriteRenderer.sprite = sprite;
                spriteObject.name = allSelectedTextuers[i].name;
                CreateSpritePrefab(spriteObject, finalOutputPath);
            }
            Object.DestroyImmediate(spriteObject);
            Debug.Log("生成成功");
        }

        private static void CreateSpritePrefab(GameObject spriteObject, string outputPath)
        {
            if (Directory.Exists(outputPath) == false)
            {
                Directory.CreateDirectory(outputPath);
                AssetDatabase.Refresh();
            }
            outputPath = outputPath.Replace("\\", "/");
            PrefabUtility.CreatePrefab(outputPath + "/" + spriteObject.name + ".prefab", spriteObject);
            AssetDatabase.Refresh();
        }

    }
