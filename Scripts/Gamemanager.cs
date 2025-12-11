using Godot;
using System;

public partial class Gamemanager : Node2D
{
	[Signal] public delegate void DeathEventHandler(); 
	[Signal] public delegate void EndingEventHandler();
	[Export] private  CharacterBody2D mainPlayer;
	[Export] private PackedScene _player;
	[Export] private Node2D _players;
	[Export] private Node2D _enemies;
	[Export] private Node2D _entities;
	[Export] private PackedScene _enemyScene;
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
		if (Input.IsActionJustPressed("escape_debug"))
			GetTree().Quit();
		if (_isReloadTime && _enemies.GetChildCount() <= 20)
		{
			_isReloadTime = false;
			spawnEnemy();
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
	}

	public void SendText(String text)
	{
		
	}

	public void sendScore(int score)
	{
		this._score += score;
	}

	private async void spawnEnemy()
	{
		Enemy enemy = _enemyScene.Instantiate<Enemy>();
		_enemies.AddChild(enemy);
		enemy.player = mainPlayer;
		enemy._players = _players;
		enemy.BulletTree = _entities;
		enemy.Position = GetRandomSpawnPosition();
		await ToSignal(GetTree().CreateTimer(1.0), "timeout");
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
