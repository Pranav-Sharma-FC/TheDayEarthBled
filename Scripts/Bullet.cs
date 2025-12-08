using Godot;
using System;
using Godot.Collections;

public partial class Bullet : Entity
{
	[Export] private bool _allied;
	public Vector2 Direction;

	public override void _Ready()
	{
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
		Velocity = Velocity.MoveToward(Vector2.Zero, _friction * (float)delta);
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

	protected override void HealthReload()
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
		if ((body is Enemy) && _allied)
		{
			GD.Print("Fish?");
			Enemy enemys = (Enemy)body;
			enemys.TakeDamage(this.Damage);
			this.Death();
		}
		else if ((body is Player) && !_allied)
		{
			GD.Print("Fish?");
			Player players = (Player)body;
			players.TakeDamage(this.Damage);
			this.Death();
		}
	}

	public override void Death()
	{
		QueueFree();
	}
}
