using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameScene3Controller : Controller
{
    public const string GAMESCENE3_SCENE_NAME = "GameScene3";

    public override string SceneName()
    {
        return GAMESCENE3_SCENE_NAME;
    }
    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}