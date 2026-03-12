using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StockInfo
{
    public string name;
    public enum StockType
    {
        cereal, bigDrink, chipsTube, fruit, fruitLarge
    }
    public StockType typeOfStock;

    public float price, currentPrice;
    public StockObject prefab;
}
