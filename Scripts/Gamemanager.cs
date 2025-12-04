using Godot;
using System;

public partial class Gamemanager : Node2D
{
	[Export] private  CharacterBody2D mainPlayer;
	[Export] private PackedScene _player;
	[Export] private Node2D _players;
	[Export] private Node2D _enemies;
	[Export] private PackedScene _enemyScene;
	private bool _isReloadTime = true;
	private int _reloadInt;
	public override void _Ready()
	{
		for (int i = (Input.GetConnectedJoypads().Count); i > 0; i--)
		{
			Player player = _player.Instantiate() as Player;
			_players.AddChild(player);
			player.DeviceId = i;
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Input.IsActionJustPressed("escape_debug"))
			GetTree().Quit();
		if (_isReloadTime)
		{
			_isReloadTime = false;
			spawnEnemy();
		}
	}

	private async void spawnEnemy()
	{
		Enemy enemy = _enemyScene.Instantiate<Enemy>();
		_enemies.AddChild(enemy);
		enemy.player = mainPlayer;
		enemy._players = _players;
		enemy.Position = GetRandomSpawnPosition();
		await ToSignal(GetTree().CreateTimer(1.0), "timeout");
		_isReloadTime = true;
	}
	
	private Vector2 GetRandomSpawnPosition()
	{
		float x = (float)GD.RandRange(1920, -1920-1920);
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
