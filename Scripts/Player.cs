using Godot;
using Godot.Collections;
using System;

public partial class Player : Entity
{
	[Export] public int DeviceId = 0;
	[Export] public bool player2; //Temporary bool
	[Export] private CharacterBody2D enemy;
	[Export] private Node2D _enemies;
	[Export] private RayCast2D rayCast;
	public bool OutOfCameraRange;
	private Vector2 _velocity = Vector2.Zero;
	private bool _activeThisFrame = true; // Only true if this player's device had input this frame

	// Called by GameManager
	/*public override void _Input(InputEvent @event)
	{
		GD.Print(@event.Device);
		if (@event.Device != DeviceId)
			return;

		// If any input came from this device, mark active
		_activeThisFrame = true;
	}
	*/

	public override void _PhysicsProcess(double delta)
	{
		AimWeapons();
		if (Health <= 0)
		{
			Death();
		}
		if (!OutOfCameraRange)
		{
			//Only check Input.IsActionPressed if this player is active this frame
			MoveCharacter(delta);
			// Reset for next frame
			//_activeThisFrame = false;
			Vector2 input = Vector2.Zero;
			if (Input.IsActionPressed("thrust_left"))
				input.X -= 1;
			if (Input.IsActionPressed("thrust_right") && player2)
				input.X += 1;
			if (Input.IsActionPressed("thrust_up"))
				input.Y -= 1;
			if (Input.IsActionPressed("thrust_down"))
				input.Y += 1;

			Velocity = Vector2.Zero;
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
	
	public CharacterBody2D GetClosestPlayer()
	{
		Vector2 fromPosition = this.GlobalPosition;
		CharacterBody2D closest = null;
		float closestDist = float.MaxValue;
		if (_enemies.GetChildCount() == 0)
			return null;
		foreach (CharacterBody2D child in _enemies.GetChildren())
		{
			if (child is Enemy enemys)
			{
				float dist = fromPosition.DistanceTo(enemys.GlobalPosition);
				if (dist < closestDist)
				{
					closestDist = dist;
					closest = enemys;
				}
			}
		}

		return closest;
	}


	protected override void MoveCharacter(double delta)
	{
		//if (_activeThisFrame)
		//{
			
		//}
	}
	public override void TakeDamage(int enemysDamage)
	{
		Health -= enemysDamage;
	}
	protected override void SpecialEffects()
	{
		
	}
	protected override void HealthReload()
	{
		
	}

	protected override void Fire()
	{
		
	}

	public override Dictionary GetStats()
	{
		Dictionary itemsDict = new Dictionary
		{
			{"Health", Health},
			{"Sheild", Sheilds},
		};
		return itemsDict;
	}

	public override void Death()
	{
		this.QueueFree();
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Enemy)
		{
			GD.Print("I am just a fish");
			Enemy enemys = (Enemy)body;
			TakeDamage(enemys.Damage);
			enemys.TakeDamage(Damage);
		}
	}

	private void AimWeapons()
	{
		if (!GodotObject.IsInstanceValid(enemy))
		{
			rayCast.RotationDegrees = 180;
			enemy = GetClosestPlayer();
		}
		else
		{
			Vector2 targetPos = enemy.GlobalPosition;
			rayCast.LookAt(targetPos);
			if (rayCast.IsColliding())
			{
				Enemy enemys = (Enemy)enemy;
				enemys.TakeDamage(Damage);
			}
		}
	}
}

