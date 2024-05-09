using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class TutorialScene2Controller : Controller
{
    public const string TUTORIALSCENE2_SCENE_NAME = "TutorialScene2";

    public override string SceneName()
    {
        return TUTORIALSCENE2_SCENE_NAME;
    }

    public void ReturnToGame()
    {
        Manager.Close();
    }
}