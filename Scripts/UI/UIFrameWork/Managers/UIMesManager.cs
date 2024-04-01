using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace UIFrameWork
{
    /// <summary>
    /// UI ��Ϣ�ֿ�
    /// ͨ���ֵ�����Ϣ�����ȥ��ֵ�õ���ص���Ϣ
    /// </summary>
    public class UIMesManager :MonoBehaviour
    {
        private static UIMesManager instance;

        // �ֵ����ڴ����Ϣ������ֻ�Ǿ���
        private Dictionary<string, string> infoDictionary = new Dictionary<string, string>();

        /// <summary>
        /// �õ��ֵ�����
        /// </summary>
        /// <param name="key">��Ϣ�� key </param>
        /// <returns></returns>
        public string GetMessage(string key)
        {
            string info = string.Empty;
            if (infoDictionary.ContainsKey(key))
            {
                info = infoDictionary[key];
            }
            return info;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="key">��Ϣ�� key </param>
        /// <param name="value">��Ϣ������</param>
        public void SetMessage(string key, string value)
        {
            if (infoDictionary.ContainsKey(key))
            {
                infoDictionary[key] = value;
            }
            else
            {
                infoDictionary.Add(key, value);
            }
        }

        // ���� UIManager
        public static UIMesManager Instance
        {
            get
            {
                // ���ʵ�������ڣ������ڳ������ҵ����е� UIManager
                if (instance == null)
                {
                    instance = FindObjectOfType<UIMesManager>();

                    // ����ڳ������Ҳ��� UIManager���򴴽�һ���µ� GameObject ����� UIManager ���
                    if (instance == null)
                    {
                        GameObject singleton = new GameObject("UIManager");
                        instance = singleton.AddComponent<UIMesManager>();
                    }

                    // ���� UIManager �ڳ����л�ʱ��������
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }

        private void Awake()
        {
            // �����������ͬ���� UIManager ʵ���������ٵ�ǰʵ����ȷ������ģʽ
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

}