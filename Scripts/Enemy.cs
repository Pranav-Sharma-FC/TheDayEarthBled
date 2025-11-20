using Godot;
using System;
using Godot.Collections;

public partial class Enemy : Entity
{
    public override void _PhysicsProcess(double delta)
    {
        if (Health <= 0)
        {
            Death();
        }
        GD.Print(Health + "HHHHHHHHHHHHHHHHHHHH");
    }

    protected override void MoveCharacter(double delta)
    {
        GD.Print("throw new NotImplementedException();");
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
}
