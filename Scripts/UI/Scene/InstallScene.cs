using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;

/// <summary>
/// InstallScene场景类
/// </summary>
public class InstallScene : SceneState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    public InstallScene()
    {
        sceneName = "InstallScene";
    }

    /// <summary>
    /// 进入场景时调用
    /// </summary>
    public override void OnEnter()
    {
        // panelManager.Push(new InstallScenePanel());
    }
}
