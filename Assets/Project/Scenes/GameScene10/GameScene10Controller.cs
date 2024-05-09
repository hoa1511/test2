using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene10Controller : Controller
{
    public const string GAMESCENE10_SCENE_NAME = "GameScene10";

    public override string SceneName()
    {
        return GAMESCENE10_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}