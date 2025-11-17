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

// Dummy Missile and Laser classes
public class Missile : WeaponBase
{
	public Missile() => Name = "Missile";

	public override void Fire(Target target) => Console.WriteLine($"{Name} fired at {target.Position}");

	public override void Update(float deltaTime) => base.Update(deltaTime);
}

public class Laser : WeaponBase
{
	public Laser() => Name = "Laser";

	public override void Fire(Target target) => Console.WriteLine($"{Name} fired at {target.Position}");

	public override void Update(float deltaTime) => base.Update(deltaTime);
}

// Simulation
class Program
{
	static void Main(string[] args)
	{
		var weapons = new List<IWeapon>
		{
			new Missile(),
			new Laser(),
			new LAAA()
		};

		var airborneTarget = new Target(new Vector2(100, 200), airborne: true);
		var groundTarget = new Target(new Vector2(50, 10), airborne: false);

		float tick = 0.1f; // 100 ms per update
		float simTime = 0f;
		float maxSim = 6.0f;

		Console.WriteLine("=== Weapon simulation start ===");

		while (simTime < maxSim)
		{
			Console.WriteLine($"\n--- t={simTime:F1}s ---");

			// Fire weapons at specific times
			if (Math.Abs(simTime - 0.0f) < 0.001f)
			{
				weapons[0].Fire(groundTarget);
				weapons[1].Fire(groundTarget);
				weapons[2].Fire(airborneTarget);
			}

			if (Math.Abs(simTime - 1.0f) < 0.001f)
			{
				weapons[0].Fire(airborneTarget);
			}

			// Update all weapons
			foreach (var w in weapons)
				w.Update(tick);

			Thread.Sleep((int)(tick * 1000));
			simTime += tick;
		}

		Console.WriteLine("\n=== Simulation end ===");
	}
}
