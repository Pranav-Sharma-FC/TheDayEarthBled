using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] private float _acceleration = 600f;   // How fast the ship accelerates
	[Export] private float _friction = 20f;       // Slows ship down when no input
	[Export] private float _maxSpeed = 600f;      // How much the ship can go too 
	[Export] private Vector2 _velocityDisplay = Vector2.Zero;
	[Export] private int _id = 0;
	private Vector2 _velocity = Vector2.Zero;
	
	public override void _PhysicsProcess(double delta)
	{
		if (true)//_id == @event.Device)
		{
			Vector2 input = Vector2.Zero;
			if (Input.IsActionPressed("thrust_left"))
				input.X -= 1;
			if (Input.IsActionPressed("thrust_right"))
				input.X += 1;
			if (Input.IsActionPressed("thrust_up"))
				input.Y -= 1;
			if (Input.IsActionPressed("thrust_down"))
				input.Y += 1;

			input = input.Normalized();

			_velocity += input * _acceleration * (float)delta;

			if (input == Vector2.Zero)
				_velocity = _velocity.MoveToward(Vector2.Zero, _friction * (float)delta);

			if (_velocity.Length() > _maxSpeed)
				_velocity = _velocity.Normalized() * _maxSpeed;

			Velocity = _velocity;
			MoveAndSlide();
		}
	}
	
	public void SetControllerId(int id)
	{
		_id = id;
	}
}
