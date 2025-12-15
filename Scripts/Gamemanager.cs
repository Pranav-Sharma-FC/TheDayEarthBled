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
	[Signal] public delegate void TextEventHandler(string Txt);

	[Export] private Attack currentSpawns = Attack.ToPortal;
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
	private bool isNotText = true;
	
	public int scrapMetal = 0;
	public int scrapExplodey = 0;
	public int scrapElectic = 0;
	public override void _Ready()
	{
		GD.Print("Cool");
		string[] lines =
			{
			"USSF HC: You are the only spacecraft that remains, Commander",
			"Commander: Affirmative, what is our goal High Command",
			"USSF HC: Die Honorably.",
			"Commander: Understood.",
			"Tip: Use your Left JoyStick to move and angle your guns, press X To fire",
			"Tip: Press Menu to pause and Back to exit",
			""
			};
		isNotText = false;
		PlayDialogueAsync(lines);
		isNotText = true;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if ((_isReloadTime && (isNotText)) && (_enemies.GetChildCount() <= 2))
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
			string[] lines =
			{
			"USSF HC: Humanity is safe in the complex, Intrepid Commander. Commander?",
			};
			isNotText = false;
			PlayDialogueAsync(lines);
			EmitSignal(SignalName.Ending);
		}
		
		if (_score >= 4000)
		{
			GD.Print("YOU WERE SUPOSED TO END THE SITH< NOT HTOIN EM");
			currentSpawns = Attack.Boss;
		}
		else if (_score >= 2000)
		{
			GD.Print("e: " + _score);
			string[] lines =
			{
			"USSF HC: Radar is picking up enemy cruisers coming out from the portal...",
			"Intrepid Commander: Give me a report on them HC",
			"USSF HC: Four times more guns firing Intrepid",
			"Intrepid Commander: ****, we don't got the health for that HC",
			"USSF HC: Use the expiremental bullets",
			"Intrepid Commander: Those things drain sheilds",
			"USSF HC: You have your orders.",
			"Intrepid Commander: Those things drain sheilds",
			"Tip: Upgrade Weapons with A, B, and Y",
			"Tip: Collect resources from killing enemies to upgrade!",
			""
			};
			isNotText = false;
			PlayDialogueAsync(lines);
			isNotText = true;
			currentSpawns = Attack.Retreat;
		}
		GD.Print("State" + currentSpawns);
	}

		
	private async void PlayDialogueAsync(string[] lines)
	{
		foreach (string line in lines)
		{
			GD.Print(line);
			EmitSignal(nameof(Text), line);
			await ToSignal(GetTree().CreateTimer(5), "timeout");
		}
	}

	public int retScore()
	{
		return this._score;
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
