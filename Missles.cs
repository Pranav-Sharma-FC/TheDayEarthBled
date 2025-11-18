using System;
using Godot;

// Target class already exists
// public class Target { public Vector2 Position { get; } public bool IsAirborne { get; } ... }

// Base weapon exists: WeaponBase : IWeapon

// Missile — homing projectile with splash damage
public class Missile : WeaponBase
{
	public float Damage { get; private set; } = 80f;
	public float Speed { get; private set; } = 50f;
	public float SplashRadius { get; private set; } = 3f;
	public override void Fire(Target target)
	{
		if (!CanFire)
		{
			Console.WriteLine($"{Name} cannot fire yet ({timeUntilReady:F2}s cooldown).");
			return;
		}

		base.Fire(target);
		Console.WriteLine($"{Name} launched toward target at {target.Position} (Homing)");

		SimulateHit(target);
	}

	private void SimulateHit(Target target)
	{
		Console.WriteLine($"Missile hits near {target.Position}, dealing {Damage} damage and {SplashRadius} splash.");
		// For demonstration, reduce Health if Target has Health property
		// target.Health -= Damage;
		// Console.WriteLine(target.Health <= 0 ? "Target destroyed!" : $"Target health: {target.Health:F1}");
	}

	private override void Update(float deltaTime)
	{
		base.Update(deltaTime);
		// Missile-specific logic could go here (homing, travel, etc.)
	}
}
