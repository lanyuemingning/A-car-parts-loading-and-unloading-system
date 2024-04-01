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
        // ����״̬
        SceneState sceneState;

        // ����״̬��ʶ
        bool isReady;

        // ��ǰ����������
        string sceneName;

        /*       // ���ؼ���ҳ������
               private string LoadPanelName { get => UIRoot.Instance.loadPanelName; }*/

        public SceneController()
        {
            isReady = false;
        }

        public void SetScene(SceneState sceneState, bool reload = true)
        {
            isReady = false;

            // �˳���ǰ����״̬���߼�
            sceneState?.OnExit();

            // �����µĳ���״̬
            this.sceneState = sceneState; 
            
            // ���ó�������
            sceneName = sceneState.SceneName; 

            if (reload) 
                // ���س���
                LoadScene();
            else
                // �����³���״̬���߼�
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
            // ͬ�����س���
            SceneManager.LoadScene(sceneName); 
            // ע�᳡��������ϵĻص�
            SceneManager.sceneLoaded += SceneLoaded;
        }

        protected void SceneLoaded(Scene scene, LoadSceneMode mode)
        { 
            // �����³���״̬���߼�
            sceneState?.OnEnter();
            
            // ��־������׼����
            isReady = true; 
            
            // ȡ��ע�᳡��������ϵĻص�
            SceneManager.sceneLoaded -= SceneLoaded; 
            
            // �������������ϵ���־
            Debug.Log($"{sceneName}����������ϣ�"); 
        }
    }
}