using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export] private CharacterBody2D player1;
	[Export] private CharacterBody2D player2;
	[Export] public CharacterBody2D player3;
	[Export] public CharacterBody2D player4;

	public override void _PhysicsProcess(double delta)
	{
		float distantplayer1 = (Math.Max(this.GlobalPosition.X - player1.GlobalPosition.X, this.GlobalPosition.Y - player1.GlobalPosition.Y));
		float distantplayer2 = (Math.Max(this.GlobalPosition.X - player2.GlobalPosition.X, this.GlobalPosition.Y - player2.GlobalPosition.Y));
		float distantplayer3 = (Math.Max(this.GlobalPosition.X - player3.GlobalPosition.X, this.GlobalPosition.Y - player3.GlobalPosition.Y));
		float distantplayer4 = (Math.Max(this.GlobalPosition.X - player4.GlobalPosition.X, this.GlobalPosition.Y - player4.GlobalPosition.Y));
		this.GlobalPosition = (player1.GlobalPosition + player2.GlobalPosition) * (float)0.5;
		GD.Print("Does this godot wakatime work please work i coding rn");
		GD.Print("Does it work does it work");
	}
}
