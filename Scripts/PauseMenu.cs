using Godot;
using System;

public partial class PauseMenu : Control
{
	private Gamemanager root;
	public override void _Ready()
	{
		Node currentScene = GetTree().CurrentScene;
		if(currentScene is Gamemanager)
			root = (Gamemanager)currentScene;
		root.Ending +=Death;
		root.Death +=Death;
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

	private void Death()
	{
		this.ProcessMode = ProcessModeEnum.Inherit;
	}
}
