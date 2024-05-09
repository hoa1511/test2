using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene2Controller : Controller
{
    public const string GAMESCENE2_SCENE_NAME = "GameScene2";

    public override string SceneName()
    {
        return GAMESCENE2_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}