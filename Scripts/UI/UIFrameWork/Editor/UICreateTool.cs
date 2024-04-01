using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

#if UNITY_EDITOR
/// <summary>
/// UIFrameWork �ı����������࣬������ݵ����ɳ��������Ľű�
/// </summary>
public class UICreateTool : Editor
{
    // �˵����Assets�˵��´���"CreatePanelCS"����ʱִ��CreatePanelCS����
    [MenuItem("Assets/Create/CreatePanelCS", false, 1)]
    private static void CreatePanelCS()
    {
        // ��ȡ��ǰ��Hierarchy�����ѡ�е���Ϸ��������
        var objs = Selection.gameObjects;
        // ��ȡѡ�еĶ����GUID��ȫ��Ψһ��ʶ��������
        var selected = Selection.assetGUIDs;

        // ����ѡ�е���Ϸ��������
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject obj = objs[i];
            // ��ȡ��Ϸ���������
            string name = obj.name;
            // �����ű��ļ���·����Assets/Scripts/UI/Panel/ObjectName.cs��
            string filePath = $"Assets/Scripts/UI/Panel/{name}.cs";
            // ��ȡѡ�ж����·��
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            // �滻.prefab��׺��Assets/Resources/ǰ׺���õ����·��
            if (objPath.Contains(".prefab"))
                objPath = objPath.Replace(".prefab", "");
            if (objPath.Contains("Assets/Resources/"))
                objPath = objPath.Replace("Assets/Resources/", "");

            // ����ļ�������
            if (!File.Exists(filePath))
            {
                // ����C#���ļ�������
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;

/// <summary>
/// {name}�����
/// </summary>
public class {name} : BasePanel
{{
    /// <summary>
    /// �����Դ·��
    /// </summary>
    static readonly string path = ""{objPath}"";

    /// <summary>
    /// ���췽��
    /// </summary>
    public {name}() : base(new UIType(path))
    {{
        
    }}

    /// <summary>
    /// ��ʼ���¼�
    /// </summary>
    protected override void InitEvent()
    {{
        // ע�ᰴť����¼�
        // ActivePanel.GetOrAddComponentInChildren<Button>(""BtnExit"").onClick.AddListener(() =>
        // {{
        //            Pop();
        // }});
    }}

    /// <summary>
    /// ��ʼʱ����
    /// </summary>
    public override void OnStart()
    {{
        base.OnStart();
    }}

    /// <summary>
    /// �л����ʱ����
    /// </summary>
    public override void OnChange(BasePanel newPanel)
    {{
        // {name} panel = newPanel as {name};
    }}
}}
";
                #endregion
                // ������д���ļ�
                File.WriteAllText(filePath, content);
                Debug.Log($"�������\n·��Ϊ{filePath}");
                // ˢ��AssetDatabase��ʹ�����ɵ��ļ���Unity�пɼ�
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("�Ѵ��ڸ��ļ�");
        }
    }

    // �˵����Assets�˵��´���"GenerateSceneCS"����ʱִ��GenerateSceneCS����
    [MenuItem("Assets/Create/GenerateSceneCS", false, 2)]
    private static void GenerateSceneCS()
    {
        // ��ȡ��ǰ��Hierarchy�����ѡ�е���Ϸ��������
        var objs = Selection.gameObjects;
        // ��ȡѡ�еĶ����GUID��ȫ��Ψһ��ʶ��������
        var selected = Selection.assetGUIDs;

        // ����ѡ�е���Ϸ��������
        for (int i = 0; i < selected.Length; i++)
        {
            // ��ȡѡ�ж����·��
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            // ������ѡ��Ķ���Ϊ�����ļ���.unity�������
            if (!objPath.EndsWith(".unity"))
                continue;

            // �滻.unity��׺���õ���������
            objPath = objPath.Replace(".unity", "");
            // ��ȡ��������
            int index = objPath.LastIndexOf('/');
            string name = objPath;
            if (index > 0)
                name = objPath.Substring(index + 1);

            // �����ű��ļ���·����Assets/Scripts/Scene/SceneNameScene.cs��
            string filePath = $"Assets/Scripts/UI/Scene/{name}.cs";

            // ����ļ�������
            if (!File.Exists(filePath))
            {
                // ����C#���ļ�������
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;

/// <summary>
/// {name}������
/// </summary>
public class {name}Scene : SceneState
{{
    /// <summary>
    /// ���췽��
    /// </summary>
    public {name}Scene()
    {{
        sceneName = ""{name}"";
    }}

    /// <summary>
    /// ���볡��ʱ����
    /// </summary>
    public override void OnEnter()
    {{
        // panelManager.Push(new {name}Panel());
    }}
}}
";
                #endregion
                // ������д���ļ�
                File.WriteAllText(filePath, content);
                Debug.Log($"�������\n·��Ϊ{filePath}");
                // ˢ��AssetDatabase��ʹ�����ɵ��ļ���Unity�пɼ�
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("�Ѵ��ڸ��ļ�");
        }
    }
}
#endif