using Godot;
using Godot.Collections;
using System;
using Godot.NativeInterop;

public partial class UIPanel : Control
{
    [Export] public Control Player1;
    [Export] public Control Player2;
    [Export] public Control Player3;
    [Export] public Control Player4;
    public Dictionary ControlDict;
    public override void _Ready()
    {
        Dictionary tempDict = new Dictionary
        {
            { Player1, 1 },
            { Player2, 2 },
            { Player3, 3 },
            { Player4, 4 },
        };
        ControlDict = tempDict;
    }
}
    