using Godot;
using System;
using Godot.Collections;

public abstract partial class Entity : CharacterBody2D
{
	[Export] protected float _acceleration = 600f;   // How fast the ship accelerates
	[Export] protected float _friction = 20f;       // Slows ship down when no input
	[Export] protected float _maxSpeed = 600f;      // How much the ship can go too 
	[Export] protected Vector2 _velocityDisplay = Vector2.Zero;
	
	[Export] protected int Health { get; set; } = 100;
	[Export] protected int MaxHealth { get; set; } = 100;
	[Export] protected int Sheilds { get; set; } = 100;
	[Export] protected int MaxSheilds { get; set; } = 100;
	[Export] protected int SheildRegenSeconds { get; set; } = 1;
	
	[Export] public int Damage { get; set; } = 100;

	// Abstract method
	protected abstract void MoveCharacter(double delta);
	public abstract void TakeDamage(int damage);
	protected abstract void SpecialEffects();
	protected abstract void HealthReload();
	protected abstract void Fire();
	
	public abstract Dictionary GetStats();

	public abstract void Death();

}
