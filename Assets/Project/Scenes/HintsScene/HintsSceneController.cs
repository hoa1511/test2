using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class HintsSceneController : Controller
{
    public const string HINTSSCENE_SCENE_NAME = "HintsScene";

    public override string SceneName()
    {
        return HINTSSCENE_SCENE_NAME;
    }

    public void ReturnToGame()
    {
        GameManager.Instance.ReloadLevel();
    }
}