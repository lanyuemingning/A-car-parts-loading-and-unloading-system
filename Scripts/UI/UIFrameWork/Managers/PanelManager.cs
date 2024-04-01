using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UIFrameWork.Extent;
namespace UIFrameWork
{
    /// <summary>
    /// ��������
    /// </summary>
    public class PanelManager
    {
        // �洢���е� UI �Լ� UI ��Ӧ�� GameObject ,key Ϊ·��
        private Dictionary<string, GameObject> dictUI;

        // �洢���е� UI �Լ� UI ��Ӧ�� BasePanel ,key Ϊ·��
        private Dictionary<string, BasePanel> dictPanel;

        // ��������ջ
        private Stack<BasePanel> panelStack;

        // ��������
        private string canvasName;

        // ��������
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

        // �õ� UI 
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
                    Debug.LogError($"��Ϊ{canvasName}�Ļ���������");
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
                Debug.LogError($"��Ϊ{ui.Name}������޷ǳ�ʼ��");
                return null;
            }
        }

        /// <summary>
        /// �Ƴ�UI
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="isDestroy">�Ƿ����٣������ټ�����</param>
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
        /// ����һ�����
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
                //��ֹ���������ظ������
                if (panelStack.Peek().UI.Path != newPanel.UI.Path)
                    panelStack.Push(newPanel);
            }
            else
                panelStack.Push(newPanel);
        }

        /// <summary>
        /// ����һ�����
        /// </summary>
        public void Pop()
        {
            if (panelStack.Count > 0)
                panelStack.Pop().OnDestroy();
            if (panelStack.Count > 0)
                panelStack.Peek().OnEnable();
        }

        /// <summary>
        /// �����������
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
