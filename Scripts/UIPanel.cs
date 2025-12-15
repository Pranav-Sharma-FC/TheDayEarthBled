using Godot;
using Godot.Collections;
using System;
using Godot.NativeInterop;

public partial class UIPanel : Control
{
	[Export] public ShipPanel Player1Control;
	private Dictionary _controlDict;
	[Export] public Player player;
	[Export] private Label _label;
	
	[Export] private Label _explodey;
	
	[Export] private Label _electic;
	
	[Export] private Label _metal;
	
	[Export] private Label _scorees;
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
		root.Text += ChangeText;
	}
	
	private void ChangeText(string i)
	{
		GD.Print("FFFFFF" + i);
		if((!_end.Visible) && (!_death.Visible))
			_label.Text = i;
	}

	private async void EndingScreen()
	{
		_end.Visible = true;
		this.ProcessMode = ProcessModeEnum.Always;
		GetTree().Paused = true;
		_label.Text = "Intrepid Commander: The 7th Fleet has fallen, High Command. Prosperity to what remains of Huma$!(**)x..-.---- $  . ";
		await ToSignal(GetTree().CreateTimer(3), "timeout");
		GetTree().Quit();
		
	}
	private async void Death()
	{
		this.ProcessMode = ProcessModeEnum.Always;
		_death.Visible = true;
		GetTree().Paused = true;
		await ToSignal(GetTree().CreateTimer(3), "timeout");
		GetTree().Quit();
	}
	
	public override void _Process(double delta)
	{
		_metal.Text = ("Metal: " + root.scrapMetal);
		_explodey.Text = ("Gundpoweder: " + root.scrapExplodey);
		_electic.Text = ("Electric: " + root.scrapElectic);
		_scorees.Text = ("Score: " + root.retScore());
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
	
