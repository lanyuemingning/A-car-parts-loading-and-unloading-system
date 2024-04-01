using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UIFrameWork.Extent;
namespace UIFrameWork
{
    /// <summary>
    /// 面板管理器
    /// </summary>
    public class PanelManager
    {
        // 存储所有的 UI 以及 UI 对应的 GameObject ,key 为路径
        private Dictionary<string, GameObject> dictUI;

        // 存储所有的 UI 以及 UI 对应的 BasePanel ,key 为路径
        private Dictionary<string, BasePanel> dictPanel;

        // 管理面板的栈
        private Stack<BasePanel> panelStack;

        // 画布名称
        private string canvasName;

        // 画布对象
        private GameObject canvasObj;

        public PanelManager()
        {
            dictUI = new Dictionary<string, GameObject>();
            dictPanel = new Dictionary<string, BasePanel>();
            panelStack = new Stack<BasePanel>();
            canvasName = "Canvas";
            canvasObj = GameObject.Find(canvasName);
        }

        public PanelManager(string name)
        {
            dictUI = new Dictionary<string, GameObject>();
            dictPanel = new Dictionary<string, BasePanel>();
            panelStack = new Stack<BasePanel>();
            canvasName = name;
        }

        // 得到 UI 
        private GameObject GetSingleUI(UIType ui)
        {
            if (dictUI.ContainsKey(ui.Path))
            {
                ui.Init = true;
                return dictUI[ui.Path];
            }
            if (canvasObj == null)
            {
                canvasObj = GameObject.Find(canvasName);
                if (canvasObj == null)
                {
                    Debug.LogError($"名为{canvasName}的画布不存在");
                    return null;
                }
            }
        GameObject obj= null;
#if UNITY_EDITOR
            //obj = GameObject.Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Resources/{ui.Path}.prefab"), canvasObj.transform);
            obj = GameObject.Instantiate(Resources.Load<GameObject>(ui.Path), canvasObj.transform);
#else
            // obj = Resources.Load<GameObject>($"Assets/Resources/{ui.Path}.prefab");
            // obj = GameObject.Instantiate(Resources.Load<GameObject>("Panel/StartPanel"), canvasObj.transform);
            obj = GameObject.Instantiate(Resources.Load<GameObject>(ui.Path), canvasObj.transform);
#endif
            obj.name = ui.Name;
            dictUI.Add(ui.Path, obj);
   
            if ( obj != null)
                return obj;
            else
            {
                Debug.LogError($"名为{ui.Name}的面板无非初始化");
                return null;
            }
        }

        /// <summary>
        /// 移除UI
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="isDestroy">是否销毁，不销毁即隐藏</param>
        public void DestroyUI(UIType ui, bool isDestroy = false)
        {
            if (dictUI.ContainsKey(ui.Path))
            {
                if (isDestroy)
                {
                    GameObject.Destroy(dictUI[ui.Path]);
                    dictUI.Remove(ui.Path);
                    dictPanel.Remove(ui.Path);
                }
                else
                {
                    dictUI[ui.Path].transform.PanelAppearance(false);
                }
            }
        }

        /// <summary>
        /// 推入一个面板
        /// </summary>
        /// <param name="newPanel"></param>
        public void Push(BasePanel newPanel)
        {
            if (panelStack.Count > 0)
                panelStack.Peek().OnDisable();

            if (!dictPanel.ContainsKey(newPanel.UI.Path))
            {
                dictPanel.Add(newPanel.UI.Path, newPanel);
                GameObject obj = GetSingleUI(newPanel.UI);
                newPanel.ActivePanel = obj.transform;
                newPanel.Initializa(this);
            }
            else
            {
                BasePanel p = dictPanel[newPanel.UI.Path];
                p.OnChange(newPanel);
                newPanel = p;
            }

            newPanel.OnStart();
            if (panelStack.Count > 0)
            {
                //防止连续推送重复的面板
                if (panelStack.Peek().UI.Path != newPanel.UI.Path)
                    panelStack.Push(newPanel);
            }
            else
                panelStack.Push(newPanel);
        }

        /// <summary>
        /// 弹出一个面板
        /// </summary>
        public void Pop()
        {
            if (panelStack.Count > 0)
                panelStack.Pop().OnDestroy();
            if (panelStack.Count > 0)
                panelStack.Peek().OnEnable();
        }

        /// <summary>
        /// 弹出所有面板
        /// </summary>
        public void PopAll()
        {
            var values = new List<BasePanel>(dictPanel.Values);
            while (values.Count > 0)
            {
                values[0].OnDestroy(true);
                values.RemoveAt(0);
            }
        }
    }
}
