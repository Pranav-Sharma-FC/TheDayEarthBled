using Godot;
using System;

public partial class Gamemanager : Node2D
{
    [Export] private PackedScene _player;
    [Export] private Node2D _players;
    public override void _Ready()
    {
        for (int i = (Input.GetConnectedJoypads().Count); i >= 0; i--)
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
    }
    
    public override void _Input(InputEvent @event)
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
    
}
