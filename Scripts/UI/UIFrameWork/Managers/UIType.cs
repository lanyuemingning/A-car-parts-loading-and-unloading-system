using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{
    /// <summary>
    /// UI类型
    /// 存储UI的名称以及路径
    /// </summary>
    public class UIType
    {
        private string name;
        private string path;
        private bool init;
        /// <summary>
        /// UI名称
        /// </summary>
        public string Name { get => name; }
        /// <summary>
        /// UI路径
        /// </summary>
        public string Path { get => path; }
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        public bool Init { get => init; set => init = value; }

        /// <summary>
        /// UI类型
        /// 存储UI的名称以及路径
        /// </summary>
        /// <param name="uiPath">UI路径</param>
        public UIType(string uiPath)
        {
            init = false;
            path = uiPath;
            name = path.Substring(path.LastIndexOf('/') + 1);
        }

        public override string ToString()
        {
            return $"name : {name} , path : {path}";
        }
    }
}