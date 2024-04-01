using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork;

/// <summary>
/// SampleScene场景类
/// </summary>
public class SampleSceneScene : SceneState
{
    /// <summary>
    /// 构造方法
    /// </summary>
    public SampleSceneScene()
    {
        sceneName = "SampleScene";
    }

    /// <summary>
    /// 进入场景时调用
    /// </summary>
    public override void OnEnter()
    {
        // panelManager.Push(new SampleScenePanel());
    }
}
