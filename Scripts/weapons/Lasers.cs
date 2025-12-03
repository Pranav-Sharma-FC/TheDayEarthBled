using System;
using System.Collections.Generic;
using System.Threading;
using Godot; // Only needed if you actually want Godot Vector2; else replace with custom struct

public partial class Lasers : Node2D
{
	// class content
}

//Weapons for the aircraft
namespace WeaponDemo
{
	// Simple target class used by weapons
	public partial class Lasers
	{
		public Vector2 Position { get; set; }
		public float Health { get; set; } = 100;
		public bool IsAirborne { get; set; } = false;

		public Lasers(Vector2 pos, bool airborne = false)
		{
			Position = pos;
			IsAirborne = airborne;
		}
	}

	// Base weapon providing cooldown tracking
	public partial class Lasers
	{

	}
	public class WeaponBase : LAAA
	{
		public string Name { get; protected set; }
		public float Cooldown { get; protected set; }
		protected float timeUntilReady = 0f;
		public bool CanFire => timeUntilReady <= 0f;

	public async void getNode(Node2D Collision)
	{
		GD.Print("eee");
	}
	}
}
