using Godot;
using System;

public partial class MainMenu : Control
{
    private Node2D currentScene;

    public override void _Ready()
    {
        currentScene = (Node2D)GetTree().CurrentScene;
    }

    private string ScenePath { get; set; } = "res://Scenes/main.tscn";

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("start"))
            SwitchScenes();    
        
        if (Input.IsActionJustPressed("escape_debug"))
            GetTree().Quit();
    }

    private void SwitchScenes()
    {
        GetTree().ChangeSceneToFile(ScenePath);
    }
    
}
