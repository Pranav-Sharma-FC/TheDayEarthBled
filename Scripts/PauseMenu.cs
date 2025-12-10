using Godot;
using System;

public partial class PauseMenu : Control
{
	public override void _Ready()
	{
		this.ProcessMode = ProcessModeEnum.Always;
	}
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("pause"))
		{
			this.Visible = !this.Visible;
			GetTree().Paused = !GetTree().Paused;
		}
	}
}
