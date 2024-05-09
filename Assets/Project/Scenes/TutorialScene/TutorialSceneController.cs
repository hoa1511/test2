using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class TutorialSceneController : Controller
{
    public const string TUTORIALSCENE_SCENE_NAME = "TutorialScene";

    public override string SceneName()
    {
        return TUTORIALSCENE_SCENE_NAME;
    }

    public void ReturnToGame()
    {
        Manager.Close();
    }
}