using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class HomeSceneController : Controller
{
    public const string HOMESCENE_SCENE_NAME = "HomeScene";

    public override string SceneName()
    {
        return HOMESCENE_SCENE_NAME;
    }
}