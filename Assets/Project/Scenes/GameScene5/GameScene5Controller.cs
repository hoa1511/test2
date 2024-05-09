using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene5Controller : Controller
{
    public const string GAMESCENE5_SCENE_NAME = "GameScene5";

    public override string SceneName()
    {
        return GAMESCENE5_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}