using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class PauseSceneController : Controller
{
    public const string PAUSESCENE_SCENE_NAME = "PauseScene";

    public override string SceneName()
    {
        return PAUSESCENE_SCENE_NAME;
    }

    public void ReloadLevel()
    {
        GameManager.Instance.ReloadLevel();
    }

    public void Backhome()
    {
        GameManager.Instance.BackToHomeScene();
    }
}