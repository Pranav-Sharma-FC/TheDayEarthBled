using Godot;
using System;
using Godot.Collections;

public partial class Bullet : Entity
{
	[Export] private bool _allied;
	public Vector2 Direction;
	public Entity target;
	public static int regularDamage;

	public override void _Ready()
	{
		regularDamage = Damage;
		if (_allied)
		{
			Vector2 mousePos = GetGlobalMousePosition();
			Direction = (mousePos - this.GlobalPosition).Normalized();
		}
		
		Velocity = Direction * _maxSpeed;
		Fire();
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveCharacter(delta);
		//if (Input.IsActionJustPressed("escape_debug"))
		// 	Death();
	}

	protected override void MoveCharacter(double delta)
	{
		if(target == null || !(GodotObject.IsInstanceValid(target)))
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, _friction * (float)delta);
		}
		else
		{
			Damage = regularDamage*2;
			Vector2 direction = (target.GlobalPosition - this.GlobalPosition).Normalized();

			// Accelerate in that direction
			Velocity += direction * _maxSpeed * (float)delta;

			// Clamp to max speed
			if (Velocity.Length() > _maxSpeed)
				Velocity = Velocity.Normalized() * _maxSpeed;

		}
		MoveAndSlide();	
	}

	public override void TakeDamage(int damage)
	{
		throw new NotImplementedException();
	}

	protected override void SpecialEffects()
	{
		throw new NotImplementedException();
	}

	protected async override void Fire()
	{
		await ToSignal(GetTree().CreateTimer(5.0), "timeout");
		Death();
	}

	public override Dictionary GetStats()
	{
		throw new NotImplementedException();
	}
	
	public void OnBodyEntered(Node2D body)
	{ 
		GD.Print("Collided with: ", body, " of type: ", body.GetType());
		if ((body is Enemy) && _allied)
		{
			GD.Print("Fish?: " + _allied);
			Enemy enemys = (Enemy)body;
			enemys.TakeDamage(this.Damage);
			this.Death();
		}
		else if ((body is Player) && !_allied)
		{
			GD.Print("Fisssssh?");
			Player players = (Player)body;
			players.TakeDamage(this.Damage);
			GD.Print(players);
			this.Death();
		}
	}

	public override void Death()
	{
		QueueFree();
	}
}
