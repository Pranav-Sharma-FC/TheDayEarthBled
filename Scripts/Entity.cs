using Godot;
using System;

public abstract partial class Entity : CharacterBody2D
{
    [Export] private float _acceleration = 600f;   // How fast the ship accelerates
    [Export] private float _friction = 20f;       // Slows ship down when no input
    [Export] private float _maxSpeed = 600f;      // How much the ship can go too 
    [Export] private Vector2 _velocityDisplay = Vector2.Zero;
    
    [Export] public int Health { get; set; } = 100;
    [Export] public int MaxHealth { get; set; } = 100;
    
    [Export] public int Damage { get; set; } = 100;
    
    // Abstract method (must be implemented in derived classes)
    public abstract void MoveCharacter(Vector2 direction);
    
}
