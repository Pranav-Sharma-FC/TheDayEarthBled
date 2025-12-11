using Godot;
using System;

public partial class FinalLevelDialogue : Label
{
    [Export] private Label _referencetolabel;

    private async void dialogue2()
    {
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Alyx: Earth comms are silent. Fleet strength below nine percent";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Harper: Transmit to all ships: Hold the line at Earth horizon";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Harper: We will not win today. We will buy time for others to live";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Singh: Ammo dry. Switching to kinetic runs. See you on the other side";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Alien: We will unmake your Sun and your named with it.";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Harper: Eagle-One to all stations-fire everything. Now.";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Alyx: Tip-Ramming is the last resort. Save the carriers.";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Kodiak covers evac. phoenix protexts the Gate. Move!";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Alyx: Final objective: Hold for the evac timer, then retrest by wing";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        _referencetolabel.Text =
            "Harper: UNSHC to all units...you did well. Earth bled, but never bowed.";
    }
}
