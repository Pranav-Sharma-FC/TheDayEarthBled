using Godot;
using Godot.Collections;
using System;
using Godot.NativeInterop;

public partial class UIPanel : Control
{
	//UI Code
	[Export] public ShipPanel Player1Control;
	private Dictionary _controlDict;
	[Export] public Player player;
	
	//Sends updates to ShipPanel sliders from players personal dictionary
	public override void _Process(double delta)
	{
		if (GodotObject.IsInstanceValid(player) || player != null)
		{
			Dictionary playerDict = player.GetStats();
			Player1Control.GetValues(playerDict["Health"].As<double>(),
				playerDict["Sheild"].As<double>());
		}
	}
	
}
	
