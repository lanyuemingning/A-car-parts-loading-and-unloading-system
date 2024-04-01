using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIFrameWork
{
    /// <summary>
    /// ��ܵĸ��ű� 
    /// </summary>
    
    public class UIRoot : MonoBehaviour
    {
        // �������ű�
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

        // ����������
        private SceneController sceneController;

        // �������� 
        private PanelManager panelManager;

       /* // ������������
        [Header("���س���ʱ��ʾ�Ľ������������")]
        public string loadPanelName = "LoadContinuePanel";*/

        protected virtual void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            sceneController = new SceneController();

            // �����л���ʱ������ GameObject
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
        /// ���س���
        /// </summary>
        /// <param name="sceneState"></param>
        /// <param name="loadPanel"></param>
        /// <param name="reload"></param>
        public void LoadScene(SceneState sceneState, bool reload = true)
        {
            sceneController?.SetScene(sceneState, reload);
        }

        // ��ʼ����������
        public void Initialize(PanelManager manager)
        {
            panelManager = manager;
        }
    }
}
