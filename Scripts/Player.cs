using Godot;
using System;

public partial class Player : Entity
{
	[Export] public int DeviceId = 0;
	[Export] private float _acceleration = 600f;
	[Export] private float _friction = 20f;
	[Export] private float _maxSpeed = 600f;

	private Vector2 _velocity = Vector2.Zero;
	private bool _activeThisFrame = true; // Only true if this player's device had input this frame

	// Called by GameManager
	public void _Input(InputEvent @event)
	{
		GD.Print(@event.Device);
		if (@event.Device != DeviceId)
			return;

		// If any input came from this device, mark active
		_activeThisFrame = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Only check Input.IsActionPressed if this player is active this frame
		MoveCharacter();
		// Reset for next frame
		//_activeThisFrame = false;
	}

	public override void MoveCharacter()
	{
		if (_activeThisFrame)
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

			GD.Print(Input.IsActionPressed("thrust_down"));

			input = input.Normalized();

			_velocity += input * _acceleration * (float)delta;

			if (input == Vector2.Zero)
				_velocity = _velocity.MoveToward(Vector2.Zero, _friction * (float)delta);

			if (_velocity.Length() > _maxSpeed)
				_velocity = _velocity.Normalized() * _maxSpeed;

			Velocity = new Vector2(_velocity.X, _velocity.Y);
			MoveAndSlide();
		}
	}
	public override void TakeDamage(Vector2 direction)
    {
        //
    }
	public override void SpecialEffects(Vector2 direction)
    {
        
    }
	public override void HealthReload(Vector2 direction)
    {
        
    }
}
