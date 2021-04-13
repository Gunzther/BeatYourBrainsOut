using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item
{
    public enum ItemType
    {
        BaseballBat,
        Nail,
        RubberBand,
    }

    public ItemType itemType;
    public int amount;
}