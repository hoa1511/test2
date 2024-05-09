using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene11Controller : Controller
{
    public const string GAMESCENE11_SCENE_NAME = "GameScene11";

    public override string SceneName()
    {
        return GAMESCENE11_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}