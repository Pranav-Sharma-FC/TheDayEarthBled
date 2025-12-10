using Godot;
using System;
using Godot.Collections;

public partial class Bullet : Entity
{
	[Export] private bool _allied;
	public Vector2 Direction = Vector2.Zero;
	public Node2D target;
	
	public override void _Ready()
	{
		if (target != null || GodotObject.IsInstanceValid(target))
			Direction = (target.GlobalPosition - this.GlobalPosition).Normalized();
			GD.Print("Direction");
		Velocity = Direction * _maxSpeed;
		GD.Print(Velocity);
		Fire();
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveCharacter(delta);
		//if (Input.IsActionJustPressed("escape_debug"))
		// 	Death();
		MoveAndSlide();
	}

	protected override void MoveCharacter(double delta)
	{ 
		Velocity = Velocity.MoveToward(Direction, _friction * (float)delta);
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
