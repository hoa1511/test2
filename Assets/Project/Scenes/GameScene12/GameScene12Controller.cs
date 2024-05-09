using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene12Controller : Controller
{
    public const string GAMESCENE12_SCENE_NAME = "GameScene12";

    public override string SceneName()
    {
        return GAMESCENE12_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}