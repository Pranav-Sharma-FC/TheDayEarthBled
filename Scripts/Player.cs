using Godot;
using Godot.Collections;
using System;



public partial class Player : Entity
{
	//Enum
	public enum BulletFireMode
	{
		Single,
		Burst,
		Auto
	}
	[Export] public BulletFireMode currentBullet = BulletFireMode.Single;
	[Export] public int DeviceId = 0;
	[Export] public bool Player2; //Temporary bool
	[Export] private CharacterBody2D _enemy;
	[Export] private Node2D _enemies;
	[Export] private RayCast2D _rayCast;
	[Export] private PackedScene _bullets;
	[Export] private Node2D _bulletSpawn;
	[Export] private Node2D _bulletTree;
	[Export] private Node2D _portBullets;
	[Export] private Node2D _sternBullets;
	[Export] private Node2D _starboardBullets;
	[Export] private Node2D _portAimers;
	[Export] private Node2D _sternAimers;
	[Export] private Node2D _starboardAimers;
	private Node2D _currentAimer;
	private bool _outOfCameraRange;
	private Vector2 _velocity = Vector2.Zero;
	private bool _activeThisFrame = true; // Only true if this player's device had input this frame
	private bool _isBulletReloadTime = true;
	private bool _isPortReloadTime = true;
	private bool _isStarboardReloadTime = true;
	private bool _isSternReloadTime = true;

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
		if (isShield)
		{
			RegenSheilds();
		}
		AimWeapons();
		if (Health <= 0)
		{
			Death();
		}
		if (!_outOfCameraRange)
		{
			//Only check Input.IsActionPressed if this player is active this frame
			MoveCharacter(delta);
			// Reset for next frame
			//_activeThisFrame = false;
			Vector2 input = Vector2.Zero;
			if (Input.IsActionPressed("thrust_left"))
				input.X -= 1;
			else if (Input.IsActionPressed("thrust_right") && Player2)
				input.X += 1;
			if (Input.IsActionPressed("thrust_up"))
				input.Y -= 1;
			else if (Input.IsActionPressed("thrust_down"))
				input.Y += 1;
			if (Input.IsActionPressed("shoot"))
				GD.Print(":laser");//Fire();
			if (Input.IsActionPressed("shoot_port") & _isPortReloadTime)
				FirePort();
			if (Input.IsActionPressed("shoot_stern") & _isSternReloadTime)
				FireStern();
			if (Input.IsActionPressed("shoot_starboard") & _isStarboardReloadTime)
				FireStarboard();
			

			Velocity = Vector2.Zero;
			input = input.Normalized();

			_velocity += input * _acceleration * (float)delta;

			if (input == Vector2.Zero)
				_velocity = _velocity.MoveToward(Vector2.Zero, _friction * (float)delta);

			if (_velocity.Length() > _maxSpeed)
				_velocity = _velocity.Normalized() * _maxSpeed;

			Velocity = new Vector2(_velocity.X, _velocity.Y);
			MoveAndSlide();
			_enemy = GetClosestPlayer();
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

	private async void FireStern()
	{
		_isSternReloadTime = false;
		foreach (Node2D stern in _sternBullets.GetChildren())
		{
			_bulletSpawn = stern;
			_currentAimer = _sternAimers;
			Fire();
			await ToSignal(GetTree().CreateTimer(0.15), "timeout");
		}
		await ToSignal(GetTree().CreateTimer(0.75), "timeout");
		_isSternReloadTime = true;
	}
	
	private async void FireStarboard()
	{
		_isStarboardReloadTime = false;
		foreach (Node2D sb in _starboardBullets.GetChildren())
		{
			_bulletSpawn = sb;
			_currentAimer = _starboardAimers;
			Fire();
			await ToSignal(GetTree().CreateTimer(0.15), "timeout");
		}
		await ToSignal(GetTree().CreateTimer(0.75), "timeout");
		_isStarboardReloadTime = true;
	}
	
	private async void FirePort()
	{
		_isPortReloadTime = false;
		foreach (Node2D port in _portBullets.GetChildren())
		{
			_bulletSpawn = port;
			_currentAimer = _portAimers;
			Fire();
			await ToSignal(GetTree().CreateTimer(0.15), "timeout");
		}
		await ToSignal(GetTree().CreateTimer(0.75), "timeout");
		_isPortReloadTime = true;
	}


	protected override void MoveCharacter(double delta)
	{
		//if (_activeThisFrame)
		//{
			
		//}
	}
	public override void TakeDamage(int enemysDamage)
	{
		Sheilds -= enemysDamage;
		if (Sheilds < 0)
		{
			Health += Sheilds;
			GD.Print("Health Debug" + Health);
			Sheilds = 0;
		}
	}
	protected override void SpecialEffects()
	{
		
	}

	protected override void Fire()
	{
		_isBulletReloadTime = false;
		Bullet bull = _bullets.Instantiate<Bullet>();
		Vector2 randomOffset = new Vector2(
			GD.RandRange(-50, 50),
			GD.RandRange(-50, 50)
		);
		bull.target = _currentAimer;
		bull.Position = _bulletSpawn.GlobalPosition + randomOffset;
		_bulletTree.AddChild(bull);
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
		if (!GodotObject.IsInstanceValid(_enemy))
		{
			_rayCast.RotationDegrees = 180;
			_enemy = GetClosestPlayer();
		}
		else
		{
			Vector2 targetPos = _enemy.GlobalPosition;
			_rayCast.LookAt(targetPos);
			if (_rayCast.IsColliding())
			{
				Node2D objectss = (Node2D) _rayCast.GetCollider();
				if (objectss.GetParent() is Enemy enemy) 
				{
					GD.Print("eadadfadfd");
					enemy.TakeDamage(Damage);
				}
			}
		}
	}
}
