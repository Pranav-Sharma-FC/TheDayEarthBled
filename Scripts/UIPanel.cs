using Godot;
using Godot.Collections;
using System;
using Godot.NativeInterop;

public partial class UIPanel : Control
{
	[Export] public ShipPanel Player1Control;
	private Dictionary _controlDict;
	[Export] public Player player;
	private Gamemanager root;
	[Export] private Control _end;
	
	[Export] private Control _death;
	public override void _Ready()
	{
		Node currentScene = GetTree().CurrentScene;
		if(currentScene is Gamemanager)
			root = (Gamemanager)currentScene;
		root.Ending += EndingScreen;
		root.Death += Death;
	}

	private void EndingScreen()
	{
		_end.Visible = true;
		endText();
		GetTree().Paused = true;
	}
	private void Death()
	{
		_death.Visible = true;
		GetTree().Paused = true;
	}
	
	public override void _Process(double delta)
	{
		if (GodotObject.IsInstanceValid(player) || player != null)
		{
			Dictionary playerDict = player.GetStats();
			Player1Control.GetValues(playerDict["Health"].As<double>(),
				playerDict["Sheild"].As<double>());
		}
		
	}

	private void endText()
	{
		GD.Print("end");
	}
	
}
	
