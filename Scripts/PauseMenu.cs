using Godot;
using System;

//Pauses game
public partial class PauseMenu : Control
{
	//Makes sure node itself doesnt get paused
	public override void _Ready()
	{
		this.ProcessMode = ProcessModeEnum.Always;
	}
	
	//Pauses game with a (unfinished) pause screen
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("pause"))
		{
			this.Visible = !this.Visible;
			GetTree().Paused = !GetTree().Paused;
		}
	}
}
