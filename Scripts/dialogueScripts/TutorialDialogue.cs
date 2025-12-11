using Godot;
using System;

public partial class TutorialDialogue : Label
{
    public override void _Ready()
    {
        dialouge();
    }
    private async void dialouge()
    {
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        this.Text = 
            "Alyx-Protocol: Admiral, use your right joystick to cycle fleet views. Left stick to adjust formation bearing. Camera lock to sector Tango-Four";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        this.Text =
            "Rear Admiral Sign (PHOENIX-LEAD): Eagle-One, this is Phoenix. Our frigate screen is ready for rally. UNSF Roosevelt and Yamato report green lights on railgun capacitators.";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        this.Text =
            "Harper: Good. Signal 'Ready Room Bravo'. Weapons free on all hostiles. Confirm IFF before firing-aliens are spoofing from transponders.";
        await ToSignal(GetTree().CreateTimer(3), "timeout");
        this.Text =
            "[Combat Cue]: Contact left! bearing zero-seven-zero! Enermy corvettes breaking formation! Fox-3. Fox-3!";
    }
}
