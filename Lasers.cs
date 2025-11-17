using Godot;
using Godot;
using System;

{public partial class Lasers : Node2D}
//Weapons for the aircraft

using System;
using System.Collections.Generic;
using System.Numerics; // for Vector2 (add System.Numerics reference if needed)
using System.Reflection.Metadata;
using System.Threading;

namespace WeaponDemo
{
	// Simple interface for weapons
	public interface IWeapon
	{
		string Name { get; }
		float Cooldown { get; }      // seconds between shots
		bool CanFire { get; }
		void Fire(Target target);
		void Update(float deltaTime); // called every simulation tick
	}

	// Simple target class used by weapons
	public class Target
	{
		public Vector2 Position { get; set; }
		public float Health { get; set; } = 100;
		public bool IsAirborne { get; set; } = false;

		public Target(Vector2 pos, bool airborne = false)
		{
			Position = pos;
			IsAirborne = airborne;
		}
	}

	// Base weapon providing cooldown tracking
	public abstract interface Lasers
	{

	}
	public class WeaponBase : IWeapon
	{
		public string Name { get; protected set; }
		public float Cooldown { get; protected set; }
		protected float timeUntilReady = 0f;
		public bool CanFire => timeUntilReady <= 0f;
		public virtual void Fire(Target target)
		{
			if (!CanFire)
			{
				Console.WriteLine($"{Name} is reloading ({timeUntilReady:F2}s left).");
				return;
			}

			timeUntilReady = Cooldown;
		}
		public virtual void Update(float deltaTime)
		{
			if (timeUntilReady > 0f)
				timeUntilReady -= deltaTime;
		}
	}
}
