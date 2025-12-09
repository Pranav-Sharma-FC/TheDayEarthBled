using Godot;
using Godot.Collections;
using System;
using Godot.NativeInterop;

public partial class UIPanel : Control
{
	[Export] public ShipPanel Player1Control;
	[Export] public ShipPanel Player2Control;
	[Export] public ShipPanel Player3Control;
	[Export] public ShipPanel Player4Control;
	private Dictionary _controlDict;
	[Export] public Player Player1;
	[Export] public Player Player2;
	[Export] public Player Player3;
	[Export] public Player Player4;
	public override void _Ready()
	{
		Dictionary tempControlDict = new Dictionary
		{
			{ Player1Control, Player1 },
			{ Player2Control, Player2 },
			{ Player3Control, Player3 },
			{ Player4Control, Player4},
		};
		_controlDict = tempControlDict;
	}
	
	public override void _Process(double delta)
	{
		foreach (var kvp in _controlDict)
		{
			GodotObject obj = kvp.Value.AsGodotObject();
			Player player = obj as Player;
			ShipPanel controls = kvp.Key.As<ShipPanel>();
			if (GodotObject.IsInstanceValid(player) || player != null)
			{
				Dictionary playerDict = player.GetStats();
				controls.GetValues(playerDict["Health"].As<double>(),
					playerDict["Sheild"].As<double>());
			}
		}
	}
	
}
	
