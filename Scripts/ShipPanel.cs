using Godot;
using System;

public partial class ShipPanel : Control
{ 
    [Export] public HSlider Health;
    [Export] public HSlider Sheilds;
    [Export] public double HealthValue;
    [Export] public double SheildsValue;

    public override void _PhysicsProcess(double delta)
    {
        Health.Value = HealthValue;
        Sheilds.Value = SheildsValue;
    }
}
