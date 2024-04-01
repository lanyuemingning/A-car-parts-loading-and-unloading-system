using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

#if UNITY_EDITOR
/// <summary>
/// UIFrameWork 的编译器工具类，用来快捷的生成场景和面板的脚本
/// </summary>
public class UICreateTool : Editor
{
    // 菜单项：在Assets菜单下创建"CreatePanelCS"项，点击时执行CreatePanelCS方法
    [MenuItem("Assets/Create/CreatePanelCS", false, 1)]
    private static void CreatePanelCS()
    {
        // 获取当前在Hierarchy面板中选中的游戏对象数组
        var objs = Selection.gameObjects;
        // 获取选中的对象的GUID（全局唯一标识符）数组
        var selected = Selection.assetGUIDs;

        // 遍历选中的游戏对象数组
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject obj = objs[i];
            // 获取游戏对象的名称
            string name = obj.name;
            // 构建脚本文件的路径（Assets/Scripts/UI/Panel/ObjectName.cs）
            string filePath = $"Assets/Scripts/UI/Panel/{name}.cs";
            // 获取选中对象的路径
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            // 替换.prefab后缀和Assets/Resources/前缀，得到相对路径
            if (objPath.Contains(".prefab"))
                objPath = objPath.Replace(".prefab", "");
            if (objPath.Contains("Assets/Resources/"))
                objPath = objPath.Replace("Assets/Resources/", "");

            // 如果文件不存在
            if (!File.Exists(filePath))
            {
                // 构建C#类文件的内容
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;

/// <summary>
/// {name}面板类
/// </summary>
public class {name} : BasePanel
{{
    /// <summary>
    /// 面板资源路径
    /// </summary>
    static readonly string path = ""{objPath}"";

    /// <summary>
    /// 构造方法
    /// </summary>
    public {name}() : base(new UIType(path))
    {{
        
    }}

    /// <summary>
    /// 初始化事件
    /// </summary>
    protected override void InitEvent()
    {{
        // 注册按钮点击事件
        // ActivePanel.GetOrAddComponentInChildren<Button>(""BtnExit"").onClick.AddListener(() =>
        // {{
        //            Pop();
        // }});
    }}

    /// <summary>
    /// 开始时调用
    /// </summary>
    public override void OnStart()
    {{
        base.OnStart();
    }}

    /// <summary>
    /// 切换面板时调用
    /// </summary>
    public override void OnChange(BasePanel newPanel)
    {{
        // {name} panel = newPanel as {name};
    }}
}}
";
                #endregion
                // 将内容写入文件
                File.WriteAllText(filePath, content);
                Debug.Log($"生成完毕\n路径为{filePath}");
                // 刷新AssetDatabase，使新生成的文件在Unity中可见
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("已存在该文件");
        }
    }

    // 菜单项：在Assets菜单下创建"GenerateSceneCS"项，点击时执行GenerateSceneCS方法
    [MenuItem("Assets/Create/GenerateSceneCS", false, 2)]
    private static void GenerateSceneCS()
    {
        // 获取当前在Hierarchy面板中选中的游戏对象数组
        var objs = Selection.gameObjects;
        // 获取选中的对象的GUID（全局唯一标识符）数组
        var selected = Selection.assetGUIDs;

        // 遍历选中的游戏对象数组
        for (int i = 0; i < selected.Length; i++)
        {
            // 获取选中对象的路径
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            // 仅处理选择的对象为场景文件（.unity）的情况
            if (!objPath.EndsWith(".unity"))
                continue;

            // 替换.unity后缀，得到场景名称
            objPath = objPath.Replace(".unity", "");
            // 获取场景名称
            int index = objPath.LastIndexOf('/');
            string name = objPath;
            if (index > 0)
                name = objPath.Substring(index + 1);

            // 构建脚本文件的路径（Assets/Scripts/Scene/SceneNameScene.cs）
            string filePath = $"Assets/Scripts/UI/Scene/{name}.cs";

            // 如果文件不存在
            if (!File.Exists(filePath))
            {
                // 构建C#类文件的内容
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;

/// <summary>
/// {name}场景类
/// </summary>
public class {name}Scene : SceneState
{{
    /// <summary>
    /// 构造方法
    /// </summary>
    public {name}Scene()
    {{
        sceneName = ""{name}"";
    }}

    /// <summary>
    /// 进入场景时调用
    /// </summary>
    public override void OnEnter()
    {{
        // panelManager.Push(new {name}Panel());
    }}
}}
";
                #endregion
                // 将内容写入文件
                File.WriteAllText(filePath, content);
                Debug.Log($"生成完毕\n路径为{filePath}");
                // 刷新AssetDatabase，使新生成的文件在Unity中可见
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("已存在该文件");
        }
    }
}
#endif