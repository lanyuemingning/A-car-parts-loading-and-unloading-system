using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UIFrameWork
{
    /// <summary>
    /// �����Ļ�����Ϣ���������ں���
    /// </summary>
    public abstract class SceneState
    {
        // ��������
        public PanelManager panelManager;
        // ��������
        protected string sceneName = "";
        // ��������(�ⲿ�ɶ�)
        public string SceneName { get => sceneName;}
        
        // ��ȡ���ű���ʵ��
        protected UIRoot uiRoot { get => UIRoot.Instance; }

        public SceneState()
        {
            panelManager = new PanelManager();
        }

        /// <summary>
        /// ��������ʱ
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// ��������ʱ
        /// </summary>
        public virtual void OnUpdate()
        {

        }

        /// <summary>
        /// �����˳�
        /// </summary>
        public virtual void OnExit()
        {
            panelManager.PopAll();
        }
    }
}