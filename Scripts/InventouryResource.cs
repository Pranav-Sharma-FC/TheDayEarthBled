using Godot;
using System;

[Tool]
public partial class InventouryResource : Resource
{
	[Signal]
	public delegate void UpdateEventHandler();
	[Export]
	public Godot.Collections.Array<InvSlot> Slots {get; set; } = new();
	
	public void insert(InvItem item)
	{
		Godot.Collections.Array<InvSlot> itemSlots = new Godot.Collections.Array<InvSlot>();

		foreach (InvSlot slot in Slots)
		{
		if (slot.Item == item)
		{
			itemSlots.Add(slot);
		}

		else
		{
			var emptySlots= Slots.ToList().Where(slot => slot.Item == null).ToList();
			if (!(emptySlots.IsEmpty()))
			{
				emptySlots[0].Item = item;
				emptySlots[0].Amount = 1;
			}
		}
	EmitSignal(SignalName.Update);
	}
	
}
