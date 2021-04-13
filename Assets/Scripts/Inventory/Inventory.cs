using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.BaseballBat, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Nail, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.RubberBand, amount = 1 });
        
    }

    private void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
