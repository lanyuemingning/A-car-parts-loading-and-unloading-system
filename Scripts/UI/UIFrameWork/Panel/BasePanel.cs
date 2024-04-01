using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UIFrameWork.Extent;

namespace UIFrameWork
{
    /// <summary>
    /// �������ĸ���
    /// �����������Ļ�����Ϣ
    /// </summary>
    public abstract class BasePanel
    {
        // ���λ��
        protected Transform activePanel;

        // ��������
        protected PanelManager panelManager;

        protected UIType ui;

        // ��ܸ��ű�
        protected UIRoot Game { get => UIRoot.Instance; }
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public Transform ActivePanel { get => activePanel; set => activePanel = value; }
        public UIType UI { get => ui; }

        public BasePanel(UIType ui)
        {
            this.ui = ui;
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="manager"></param>
        public void Initializa(PanelManager manager)
        {
            panelManager = manager;
        }

        /// <summary>
        /// ����һ�����
        /// </summary>
        /// <param name="newPanel"></param>
        public void Push(BasePanel newPanel)
        {
            panelManager?.Push(newPanel);
        }

        /// <summary>
        /// ����һ�����
        /// </summary>
        public void Pop()
        {
            panelManager?.Pop();
        }

        /// <summary>
        /// ���ڳ�ʼ��
        /// ��UI����֮ǰֻ��ִ��һ��
        /// </summary>
        protected virtual void InitEvent()
        {

        }

        /// <summary>
        /// ÿ���Ƴ�һ�����ִ�еĲ���
        /// </summary>
        public virtual void OnStart()
        {
            activePanel.PanelAppearance(true);
            activePanel.SetSiblingIndex(activePanel.parent.childCount - 1);
            if (!ui.Init)
            {
                InitEvent();
                ui.Init = true;
            }
        }

        /// <summary>
        /// ÿ�μ���ʱִ�еĲ���
        /// </summary>
        public virtual void OnEnable()
        {
            activePanel.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
        }

        /// <summary>
        /// ÿ��ʧȥ����ʱִ�еĲ���
        /// �������Ƴ���һ����壬��ǰ���ͻ�ִ�д˷���
        /// </summary>
        public virtual void OnDisable()
        {
            activePanel.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        }

        /// <summary>
        /// �˳�ʱִ�еĲ���
        /// isDestroyĬ��ֵΪfalse����ʾ�����ٶ���
        /// Ƶ�����ٺ������ǱȽ��������ܵģ�����Ǳ�Ҫ�������ʹisDestroyΪfalse
        /// </summary>
        /// <param name="isDestroy">�Ƿ�����</param>
        public virtual void OnDestroy(bool isDestroy = false)
        {
            panelManager?.DestroyUI(ui, isDestroy);
        }

        /// <summary>
        /// �ı�����ı���
        /// �����ִ��OnDestroyʱisDestroyֵΪfalse
        /// ������ЩPanel����Ҫ��new��ʱ�򴫵ݲ���
        /// ��ô����Ҫʹ�ô˷�������new����Ĳ������ݸ���ǰ����
        /// �����֪�����ʹ����ο�WarningPanel��(�������û�б�ɾ���Ļ�)
        /// </summary>
        /// <param name="newPanel">�µ����</param>
        public virtual void OnChange(BasePanel newPanel)
        {

        }
    }
}
