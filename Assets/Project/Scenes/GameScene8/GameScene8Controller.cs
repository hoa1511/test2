using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene8Controller : Controller
{
    public const string GAMESCENE8_SCENE_NAME = "GameScene8";

    public override string SceneName()
    {
        return GAMESCENE8_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}