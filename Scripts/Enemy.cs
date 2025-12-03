using Godot;
using System;
using Godot.Collections;

public partial class Enemy : Entity
{
    [Export] private RayCast2D rayCast;
    [Export] public CharacterBody2D player;
    public override void _PhysicsProcess(double delta)
    {
        if (Health <= 0)
        {
            Death();
        }
        GD.Print(Health + "HHHHHHHHHHHHHHHHHHHH");
        AimWeapons();
        MoveCharacter(delta);
    }

    protected override void MoveCharacter(double delta)
    {
        Vector2 direction = (player.GlobalPosition - this.GlobalPosition).Normalized();

        // Accelerate in that direction
        Velocity += direction * _acceleration * (float)delta;

        // Clamp to max speed
        if (Velocity.Length() > _maxSpeed)
            Velocity = Velocity.Normalized() * _maxSpeed;

        MoveAndSlide();
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    protected override void SpecialEffects()
    {
        GD.Print("throw new NotImplementedException();");
    }

    protected override void HealthReload()
    {
        GD.Print("throw new NotImplementedException();");
    }

    protected override void Fire()
    {
        GD.Print("throw new NotImplementedException();");
    }

    public override Dictionary GetStats()
    {
        GD.Print("throw new NotImplementedException();");
        Dictionary tempDict = new Dictionary();
        return tempDict;
    }

    public override void Death()
    {
        this.QueueFree();
    }
    
    private void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            GD.Print("I am just a fish");
            Enemy enemys = (Enemy)body;
            TakeDamage(enemys.Damage);
            enemys.TakeDamage(Damage);
        }
    }
    
    
    
    private void AimWeapons()
    {
        if (!GodotObject.IsInstanceValid(player))
        {
            rayCast.RotationDegrees = 180;
            GD.Print("eeeee");
        }
        else
        {
            Vector2 targetPos = player.GlobalPosition;
            rayCast.LookAt(targetPos);
            GD.Print("eadadfadfd");
            if (rayCast.IsColliding())
            {
                GD.Print("eadadfadfd");
                Player enemys = (Player)player;
                enemys.TakeDamage(Damage);
            }
        }
    }
}
