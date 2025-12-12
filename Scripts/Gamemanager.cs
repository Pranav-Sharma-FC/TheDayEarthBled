using Godot;
using System;

public partial class Gamemanager : Node2D
{
	public enum Attack
	{
		ToPortal,
		Retreat,
		Boss
	}
	
	[Signal] public delegate void DeathEventHandler(); 
	[Signal] public delegate void EndingEventHandler();

	[Export] private Attack currentSpawns = Attack.Retreat;
	[Export] private  CharacterBody2D mainPlayer;
	[Export] private PackedScene _player;
	[Export] private Node2D _players;
	[Export] private Node2D _enemies;
	[Export] private Node2D _entities;
	[Export] private PackedScene[] _enemyDestroyerFleet { get; set; }
	[Export] private PackedScene[] _enemyCruiserFleet { get; set; }
	[Export] private PackedScene[] _enemyCarrierFleet { get; set; }
	
	private bool _isReloadTime = true;
	private int _reloadInt;
	private int _score;
	public override void _Ready()
	{
		GD.Print("Cool");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (_isReloadTime && _enemies.GetChildCount() <= 2)
		{
			switch (currentSpawns)
			{
				case Attack.ToPortal:
					spawnEnemy(_enemyDestroyerFleet, (float)1.0);
					break;

				case Attack.Retreat:
					spawnEnemy(_enemyCruiserFleet,(float)10.0);
					break;

				case Attack.Boss:
					spawnEnemy(_enemyCarrierFleet,(float)31536000.0);
					break;
			}
		}
		if (_players.GetChildCount() == 0)
		{
			EmitSignal(SignalName.Death);
		}

		if (_score >= 5000)
		{
			GD.Print("Score: " + _score);
			EmitSignal(SignalName.Ending);
		}
		
		if (_score >= 3000)
		{
			GD.Print("YOU WERE SUPOSED TO END THE SITH< NOT HTOIN EM");
			currentSpawns = Attack.Boss;
		}
		else if (_score >= 2000)
		{
			GD.Print("e: " + _score);
			currentSpawns = Attack.Retreat;
		}
		GD.Print("State" + currentSpawns);
	}

	public void SendText(String text)
	{
		
	}

	public void sendScore(int score)
	{
		this._score += score;
	}

	private async void spawnEnemy(PackedScene[] Fleet, float time)
	{
		foreach(PackedScene EnemyToSpawn in Fleet)
		{
			Enemy enemy = EnemyToSpawn.Instantiate<Enemy>();
			_enemies.AddChild(enemy);
			enemy.player = mainPlayer;
			enemy._players = _players;
			enemy.BulletTree = _entities;
			enemy.Position = GetRandomSpawnPosition();
			await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
		}
		await ToSignal(GetTree().CreateTimer(time), "timeout");
		_isReloadTime = true;
	}
	
	private Vector2 GetRandomSpawnPosition()
	{
		float x = (float)GD.RandRange(-1920, -1920-1920);
		float y = (float)GD.RandRange(1920, -1920-1920);

		return new Vector2(x, y);
	}

	
	/*public override void _Input(InputEvent @event)
	{
		// Send input to each player
		foreach (Node child in _players.GetChildren())
		{
			if (child is Player player)
			{
				player.HandleInput(@event);
			}
		}
	}
	*/
	
}
