using Godot;
using System;

public partial class InventorySlotPanel : Panel
{
	[Export] private Sprite2D _visual;
	[Export] private Label _amount;
	
	[Export] private InvSlot slot = null;
	
	public void update(InvSlot incSlot)
	{
		if (incSlot.Item == null)
		{
			_visual.Visible = false;
			_amount.Text = "";
			slot = null;
		}
		else
		{
			_visual.Visible = true;
			_visual.Texture = incSlot.Item.Icon;
			if(slot.Amount > 0)
			{
				_amount.Text = incSlot.Amount.ToString();  
			}
			slot = incSlot;
		}
	}
}
