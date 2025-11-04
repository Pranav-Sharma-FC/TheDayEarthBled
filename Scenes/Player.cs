using Godot;
using System;

public partial class Player : Node2D
{
	[Export] private String control;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 input = Vector2.Zero;
		if (isPlayer)
		{
			String thurst_left = thurst
			if (Input.IsActionPressed(thurst_left))
				input.X -= 10;
			if (Input.IsActionPressed("thrust_right"))
				input.X += 10;
			if (Input.IsActionPressed("thrust_up"))
				input.Y -= 10;
			if (Input.IsActionPressed("thrust_down"))
				input.Y += 10;
			if (Input.IsActionPressed("shoot"))
				Shoot();
		}
}
