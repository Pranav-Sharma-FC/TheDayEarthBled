using System;
using System.Collections.Generic;
using System.Threading;
using Godot; // Only needed if you actually want Godot Vector2; else replace with custom struct

// Simple Target class
public class Target
{
	public Vector2 Position { get; }
	public bool IsAirborne { get; }

	public Target(Vector2 position, bool airborne)
	{
		Position = position;
		IsAirborne = airborne;
	}
}

// Weapon interface
public interface IWeapon
{
	void Fire(Target target);
	void Update(float deltaTime);
	string Name { get; }
}

// Base weapon class
public class WeaponBase : IWeapon
{
	public string Name { get; protected set; } = "Unnamed";
	protected float timeUntilReady = 0f;
	protected float Cooldown = 1f;

	public virtual bool CanFire => timeUntilReady <= 0f;

	public virtual void Fire(Target target)
	{
		Console.WriteLine($"{Name} fired at {target.Position}");
		timeUntilReady = Cooldown;
	}

	public virtual void Update(float deltaTime)
	{
		if (timeUntilReady > 0f)
			timeUntilReady -= deltaTime;
	}
}

// LAAA — Laser Anti-Aircraft Artillery
public class LAAA : WeaponBase
{
	public float BurstCount { get; private set; }
	public float DamagePerShot { get; private set; }
	public float Range { get; private set; }

	private int shotsFiredInBurst = 0;
	private float burstCooldown = 0.35f;
	private float burstTimer = 0f;

	public LAAA()
	{
		Name = "LAAA";
		BurstCount = 3;
		DamagePerShot = 25;
		Range = 120f;
	}

	public override void Fire(Target target)
	{
		if (!CanFire)
		{
			Console.WriteLine($"{Name} cannot start burst yet ({timeUntilReady:F2}s).");
			return;
		}

		if (!target.IsAirborne)
		{
			Console.WriteLine($"{Name} prefers airborne targets; less effective on ground.");
		}

		shotsFiredInBurst = 0;
		burstTimer = 0f;
		timeUntilReady = Cooldown;
		Console.WriteLine($"{Name} begins burst at target {target.Position}");
	}

	public override void Update(float deltaTime)
	{
		base.Update(deltaTime);

		// Handle burst shots
		if (timeUntilReady < Cooldown - 0.0001f)
		{
			burstTimer -= deltaTime;
			if (burstTimer <= 0f && shotsFiredInBurst < BurstCount)
			{
				shotsFiredInBurst++;
				burstTimer = burstCooldown;
				Console.WriteLine($"{Name} burst shot {shotsFiredInBurst}/{(int)BurstCount} deals {DamagePerShot} damage");
			}
		}
	}
}

x