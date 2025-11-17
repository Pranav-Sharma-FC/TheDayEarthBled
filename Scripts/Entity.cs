using Godot;
using System;

public abstract partial class Entity : CharacterBody2D
{
	[Export] public float _acceleration = 600f;   // How fast the ship accelerates
	[Export] public float _friction = 20f;       // Slows ship down when no input
	[Export] public float _maxSpeed = 600f;      // How much the ship can go too 
	[Export] public Vector2 _velocityDisplay = Vector2.Zero;
	
	[Export] public int Health { get; set; } = 100;
	[Export] public int MaxHealth { get; set; } = 100;
	[Export] public int Sheilds { get; set; } = 100;
	[Export] public int MaxSheilds { get; set; } = 100;
	[Export] public int SheildRegenSeconds { get; set; } = 1;
	
	[Export] public int Damage { get; set; } = 100;

	// Abstract method
	public abstract void MoveCharacter(double delta);
	public abstract void TakeDamage(Vector2 direction);
	public abstract void SpecialEffects(Vector2 direction);
	public abstract void HealthReload(Vector2 direction);
	
}
