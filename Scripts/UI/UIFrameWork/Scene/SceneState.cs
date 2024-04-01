using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UIFrameWork
{
    /// <summary>
    /// 场景的基本信息和三个周期函数
    /// </summary>
    public abstract class SceneState
    {
        // 面板管理器
        public PanelManager panelManager;
        // 场景名称
        protected string sceneName = "";
        // 场景名称(外部可读)
        public string SceneName { get => sceneName;}
        
        // 获取根脚本的实例
        protected UIRoot uiRoot { get => UIRoot.Instance; }

        public SceneState()
        {
            panelManager = new PanelManager();
        }

        /// <summary>
        /// 场景进入时
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// 场景持续时
        /// </summary>
        public virtual void OnUpdate()
        {

        }

        /// <summary>
        /// 场景退出
        /// </summary>
        public virtual void OnExit()
        {
            panelManager.PopAll();
        }
    }
}