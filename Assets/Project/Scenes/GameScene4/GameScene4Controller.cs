using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene4Controller : Controller
{
    public const string GAMESCENE4_SCENE_NAME = "GameScene4";

    public override string SceneName()
    {
        return GAMESCENE4_SCENE_NAME;
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}