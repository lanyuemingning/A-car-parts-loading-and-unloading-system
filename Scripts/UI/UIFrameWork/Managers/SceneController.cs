using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIFrameWork
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneController
    {
        // 场景状态
        SceneState sceneState;

        // 场景状态标识
        bool isReady;

        // 当前场景的名称
        string sceneName;

        /*       // 加载加载页面名称
               private string LoadPanelName { get => UIRoot.Instance.loadPanelName; }*/

        public SceneController()
        {
            isReady = false;
        }

        public void SetScene(SceneState sceneState, bool reload = true)
        {
            isReady = false;

            // 退出当前场景状态的逻辑
            sceneState?.OnExit();

            // 设置新的场景状态
            this.sceneState = sceneState; 
            
            // 设置场景名称
            sceneName = sceneState.SceneName; 

            if (reload) 
                // 加载场景
                LoadScene();
            else
                // 进入新场景状态的逻辑
                sceneState?.OnEnter(); 
        }
     
        public void OnSceneUpdate()
        {
            if (isReady)
            {
                sceneState?.OnUpdate();
            }
        }

        protected void LoadScene()
        { 
            // 同步加载场景
            SceneManager.LoadScene(sceneName); 
            // 注册场景加载完毕的回调
            SceneManager.sceneLoaded += SceneLoaded;
        }

        protected void SceneLoaded(Scene scene, LoadSceneMode mode)
        { 
            // 进入新场景状态的逻辑
            sceneState?.OnEnter();
            
            // 标志场景已准备好
            isReady = true; 
            
            // 取消注册场景加载完毕的回调
            SceneManager.sceneLoaded -= SceneLoaded; 
            
            // 输出场景加载完毕的日志
            Debug.Log($"{sceneName}场景加载完毕！"); 
        }
    }
}