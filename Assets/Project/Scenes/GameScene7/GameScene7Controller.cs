using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene7Controller : Controller
{
    public const string GAMESCENE7_SCENE_NAME = "GameScene7";

    public override string SceneName()
    {
        return GAMESCENE7_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}