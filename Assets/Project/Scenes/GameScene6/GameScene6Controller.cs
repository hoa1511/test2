using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene6Controller : Controller
{
    public const string GAMESCENE6_SCENE_NAME = "GameScene6";

    public override string SceneName()
    {
        return GAMESCENE6_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}