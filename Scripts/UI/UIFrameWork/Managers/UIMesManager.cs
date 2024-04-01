using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace UIFrameWork
{
    /// <summary>
    /// UI 信息仓库
    /// 通过字典存放信息，如何去键值得到相关的信息
    /// </summary>
    public class UIMesManager :MonoBehaviour
    {
        private static UIMesManager instance;

        // 字典用于存放信息，这里只是举例
        private Dictionary<string, string> infoDictionary = new Dictionary<string, string>();

        /// <summary>
        /// 得到字典数据
        /// </summary>
        /// <param name="key">信息的 key </param>
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
        /// 设置数据
        /// </summary>
        /// <param name="key">信息的 key </param>
        /// <param name="value">信息的内容</param>
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

        // 单例 UIManager
        public static UIMesManager Instance
        {
            get
            {
                // 如果实例不存在，则尝试在场景中找到现有的 UIManager
                if (instance == null)
                {
                    instance = FindObjectOfType<UIMesManager>();

                    // 如果在场景中找不到 UIManager，则创建一个新的 GameObject 并添加 UIManager 组件
                    if (instance == null)
                    {
                        GameObject singleton = new GameObject("UIManager");
                        instance = singleton.AddComponent<UIMesManager>();
                    }

                    // 保留 UIManager 在场景切换时不被销毁
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }

        private void Awake()
        {
            // 如果存在其他同名的 UIManager 实例，则销毁当前实例以确保单例模式
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

}