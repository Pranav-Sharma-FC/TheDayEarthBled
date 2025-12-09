using Godot;
using System;

public partial class ShipPanel : Control
{ 
	[Export] private Godot.HSlider _health;
	[Export] private Godot.HSlider _sheilds;
	[Export] private double _healthValue;
	[Export] private double _sheildsValue;

	public override void _PhysicsProcess(double delta)
	{
		_health.Value = _healthValue;
		_sheilds.Value = _sheildsValue;
	}

	public void GetValues(double healthValue, double sheildsValue)
	{
		_healthValue = healthValue;
		_sheildsValue = sheildsValue;
	}
}
