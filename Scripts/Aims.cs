using Godot;
using System;

public partial class Aims : CharacterBody2D
{
	//Makes the Aimers Inputs
	[Export] private String left;
	[Export] private String up;
	[Export] private String down;
	[Export] private String right;
	
	//Sets Speed and Velocity
	[Export] private int _speed;
	private Vector2 _velocity = Vector2.Zero;
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
	
	public void GetInput()
	{
		//Simple Movement Script
		Vector2 inputDirection = Input.GetVector(left, right, up, down);
		Velocity = inputDirection * _speed;
	}
}
