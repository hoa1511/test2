using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene9Controller : Controller
{
    public const string GAMESCENE9_SCENE_NAME = "GameScene9";

    public override string SceneName()
    {
        return GAMESCENE9_SCENE_NAME;
    }
    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}