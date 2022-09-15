using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : BaseButton
{
    protected override void OnClick()
    {
        EventManager.OnEvent(eEventType.LevelStart);
    }
}
