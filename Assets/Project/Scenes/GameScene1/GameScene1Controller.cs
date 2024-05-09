using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene1Controller : Controller
{
    public const string GAMESCENE1_SCENE_NAME = "GameScene1";

    public override string SceneName()
    {
        return GAMESCENE1_SCENE_NAME;
    }
    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}