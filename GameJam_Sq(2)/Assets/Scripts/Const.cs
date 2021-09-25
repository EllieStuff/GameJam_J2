using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const
{
    public const KeyCode MOUSE_LEFT_BUTTON = KeyCode.Mouse0;
    public const KeyCode MOUSE_RIGHT_BUTTON = KeyCode.Mouse1;

    public enum CameraPositions { TABLE, FURNANCE };
    public enum GameState { INIT, MAIN_MENU, PLAYING, PAUSED, DELIVERING, END_GAME };

}
