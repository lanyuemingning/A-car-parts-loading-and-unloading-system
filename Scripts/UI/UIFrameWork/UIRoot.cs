using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIFrameWork
{
    /// <summary>
    /// 框架的根脚本 
    /// </summary>
    
    public class UIRoot : MonoBehaviour
    {
        // 单例根脚本
        private static UIRoot instance;

        public static UIRoot Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<UIRoot>();
                return instance;
            }
        }

        // 场景管理器
        private SceneController sceneController;

        // 面板管理器 
        private PanelManager panelManager;

       /* // 加载面板的名称
        [Header("加载场景时显示的进度条面板名称")]
        public string loadPanelName = "LoadContinuePanel";*/

        protected virtual void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            sceneController = new SceneController();

            // 场景切换的时候保留该 GameObject
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Start()
        {
            /*GameObject loadPanel = GameObject.Find(loadPanelName);
            loadPanel?.transform.PanelAppearance(false);*/
            // LoadScene(new StartScene(), false);
           

        }

        private void Update()
        {
            sceneController?.OnSceneUpdate();
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneState"></param>
        /// <param name="loadPanel"></param>
        /// <param name="reload"></param>
        public void LoadScene(SceneState sceneState, bool reload = true)
        {
            sceneController?.SetScene(sceneState, reload);
        }

        // 初始化面板管理器
        public void Initialize(PanelManager manager)
        {
            panelManager = manager;
        }
    }
}
