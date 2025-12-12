using Godot;
using System;
using Godot.Collections;

public partial class Enemy : Entity
{
	
	[Export] private RayCast2D rayCast;
	[Export] public CharacterBody2D player;
	[Export] public Node2D _players;
	private bool _isReloadTime = true;
	[Export] private PackedScene _bullets;
	[Export] private Node2D _bulletSpawn;
	public Node2D BulletTree;

	public override void _PhysicsProcess(double delta)
	{
		//Gets New Player (If there is One) (Probably Not)
		if (!GodotObject.IsInstanceValid(player) || player == null)
		{
			player = GetClosestPlayer();
			return;
		}
		//Dies
		if (Health <= 0)
		{
			Death();
		}
		GD.Print(Health + "HHHHHHHHHHHHHHHHHHHH");
		AimLaser();
		//Fires more bullets
		if (_isReloadTime)
			Fire();
		player = GetClosestPlayer();
		//Moves character
		MoveCharacter(delta);
	}

	protected override void MoveCharacter(double delta)
	{
		//Moves character towards player
		Vector2 direction = (player.GlobalPosition - this.GlobalPosition).Normalized();
		direction *= new Vector2(1, 0.2f);
		// Accelerate in that direction
		Velocity += direction * _maxSpeed * (float)delta;

		// Clamps to max speed
		if (Velocity.Length() > _maxSpeed)
			Velocity = Velocity.Normalized() * _maxSpeed;

		MoveAndSlide();
	}

	public override void TakeDamage(int damage)
	{
		Health -= damage;
	}

	protected override void SpecialEffects()
	{
		GD.Print("throw new NotImplementedException();");
	}

	public override Dictionary GetStats()
	{
		GD.Print("throw new NotImplementedException();");
		Dictionary tempDict = new Dictionary();
		return tempDict;
	}

	public override void Death()
	{
		//Despawns enemy
		this.QueueFree();
	}
	
	public void OnBodyEntered(Node2D body)
	{
		//If the body is player then takes damage from it.
		GD.Print(body + "nahhh");
		if (body is Player)
		{
			Player enemys = (Player)body;
			enemys.TakeDamage(Damage);
		}
		
	}
	
	//Fires bullet as well as gives it direction
	protected async override void Fire()
	{
		_isReloadTime = false;
		Bullet bull = _bullets.Instantiate<Bullet>();
		//Random offset
		Vector2 randomOffset = new Vector2(
			GD.RandRange(-50, 50),
			GD.RandRange(-50, 50)
		);
		bull.Position = _bulletSpawn.GlobalPosition + randomOffset;
		Vector2 mousePos = player.GlobalPosition;
		bull.Direction = (mousePos - this.GlobalPosition).Normalized();
		BulletTree.AddChild(bull);
		//Reloads
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		_isReloadTime = true;
	}
	
	
	//Old Script used for Aiming Lasers, didnt get time to implement
	private void AimLaser()
	{
		if (!GodotObject.IsInstanceValid(player) || player == null)
		{
			rayCast.RotationDegrees = 180;
			player = GetClosestPlayer();
		}
		else
		{
			Vector2 targetPos = player.GlobalPosition;
			rayCast.LookAt(targetPos);
			GD.Print("eadadfadfd");
			if (rayCast.IsColliding())
			{
				Node2D objectss = (Node2D) rayCast.GetCollider();
				if (objectss.GetParent() is Player player) 
				{
					GD.Print("eadadfadfd");
					player.TakeDamage(Damage);
				}
			}
		}
	}
	
	//Gets closest player to enemy to make sure its firing stragetically.
	public CharacterBody2D GetClosestPlayer()
	{
		Vector2 fromPosition = this.GlobalPosition;
		CharacterBody2D closest = null;
		float closestDist = float.MaxValue;
		//Returns if no players exist
		if (_players.GetChildCount() == 0)
			return null;
		foreach (CharacterBody2D child in _players.GetChildren())
		{
			//Compares players
			if (child is Player players)
			{
				float dist = fromPosition.DistanceTo(players.GlobalPosition);
				if (dist < closestDist)
				{
					closestDist = dist;
					closest = players;
				}
			}
		}

		return closest;
	}
}
