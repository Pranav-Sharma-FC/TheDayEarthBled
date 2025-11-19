using Godot;
using System;
using Godot.Collections;

public partial class Enemy : Entity
{
    protected override void MoveCharacter(double delta)
    {
        GD.Print("throw new NotImplementedException();");
    }

    public override void TakeDamage(Vector2 direction)
    {
        GD.Print("throw new NotImplementedException();");
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
}
