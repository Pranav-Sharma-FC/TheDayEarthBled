using Godot;
using System;
using Godot.Collections;

public partial class Enemy : Entity
{
	[Export] private RayCast2D rayCast;
	[Export] public CharacterBody2D player;
	[Export] public Node2D _players;
	public override void _PhysicsProcess(double delta)
	{
		if (Health <= 0)
		{
			Death();
		}
		GD.Print(Health + "HHHHHHHHHHHHHHHHHHHH");
		AimLaser();
		player = GetClosestPlayer();
		MoveCharacter(delta);
	}

	protected override void MoveCharacter(double delta)
	{
		Vector2 direction = (player.GlobalPosition - this.GlobalPosition).Normalized();

		// Accelerate in that direction
		Velocity += direction * _maxSpeed * (float)delta;

		// Clamp to max speed
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

	protected override void HealthReload()
	{
		GD.Print("throw new NotImplementedException();");
	}

	protected override void Fire()
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
		this.QueueFree();
	}
	
	public void OnBodyEntered(Node2D body)
	{
		GD.Print(body + "nahhh");
		if (body is Player)
		{
			Player enemys = (Player)body;
			TakeDamage(enemys.Damage);
			enemys.TakeDamage(Damage);
		}
		
	}
	
	
	
	private void AimLaser()
	{
		if (!GodotObject.IsInstanceValid(player))
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
	
	public CharacterBody2D GetClosestPlayer()
	{
		Vector2 fromPosition = this.GlobalPosition;
		CharacterBody2D closest = null;
		float closestDist = float.MaxValue;
		if (_players.GetChildCount() == 0)
			return null;
		foreach (CharacterBody2D child in _players.GetChildren())
		{
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
