using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrameWork
{
    /// <summary>
    /// UI����
    /// �洢UI�������Լ�·��
    /// </summary>
    public class UIType
    {
        private string name;
        private string path;
        private bool init;
        /// <summary>
        /// UI����
        /// </summary>
        public string Name { get => name; }
        /// <summary>
        /// UI·��
        /// </summary>
        public string Path { get => path; }
        /// <summary>
        /// �Ƿ��Ѿ���ʼ��
        /// </summary>
        public bool Init { get => init; set => init = value; }

        /// <summary>
        /// UI����
        /// �洢UI�������Լ�·��
        /// </summary>
        /// <param name="uiPath">UI·��</param>
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