//Designed to be effective vs airborne targets, burst-fire with tracking
	public class LAAA : WeaponBase
	{
		public float BurstCount { get; private set; }
		public float DamagePerShot { get; private set; }
		public float Range { get; private set; }

		private int shotsFiredInBurst = 0;
		private float burstCooldown = 0.35f; time between shots in a burst
		private float burstTimer = 0f;

		public class LAAA()
		{
			Name = "LAAA";
			Cooldown = 2.5f; //time between bursts
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
				Console.WriteLine($"{Name} prefers airborne targets; it's less effective on ground targets.");
			}

			// Start a burst
			shotsFiredInBurst = 0;
			burstTimer = 0f;
			timeUntilReady = Cooldown; // set main cooldown now (can't start another burst)
			Console.WriteLine($"{Name} begins burst at target {target.Position}.");
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			// If a burst is in progress, shoot at burst intervals
			if (timeUntilReady < Cooldown - 0.0001f) // crude way to check if we started a burst earlier
			{
				// we want to fire internal shots if shotsFiredInBurst < BurstCount
				burstTimer -= deltaTime;
				if (burstTimer <= 0f && shotsFiredInBurst < BurstCount)
				{
					// simulate a shot
					shotsFiredInBurst++;
					burstTimer = burstCooldown;
					Console.WriteLine($"{Name} burst shot {shotsFiredInBurst}/{(int)BurstCount} deals {DamagePerShot} damage.");
					// In a real system you'd apply damage to the tracked target
				}
			}
		}
	}

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

			float tick = 0.1f; // 100 ms per update tick
			float simTime = 0f;
			float maxSim = 6.0f;

			Console.WriteLine("=== Weapon simulation start ===");

			while (simTime < maxSim)
			{
				Console.WriteLine($"\n--- t={simTime:F1}s ---");

				// Example firing logic:
				if (Math.Abs(simTime - 0.0f) < 0.001f)
				{
					weapons[0].Fire(groundTarget); // missile at ground
					weapons[1].Fire(groundTarget); // laser at ground
					weapons[2].Fire(airborneTarget); // LAAA at airborne target (burst)
				}

				// Attempt repeated fire to show cooldown behavior
				if (Math.Abs(simTime - 1.0f) < 0.001f)
				{
					weapons[0].Fire(airborneTarget); // missile again
				}

				// Update all weapons
				foreach (var w in weapons)
					w.Update(tick);

				Thread.Sleep((int)(tick * 1000)); // slow down console output so you can read it
				simTime += tick;
			}

			Console.WriteLine("\n=== Simulation end ===");
		}
	}
extends CollisionShape2D
