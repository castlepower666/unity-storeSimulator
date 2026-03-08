using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockInfoController : MonoBehaviour
{
    private static StockInfoController instance;
    public static StockInfoController Instance => instance;

    public List<StockInfo> fruitStock, produceStock;
    private List<StockInfo> allStock = new List<StockInfo>();

    void Awake()
    {
        instance = this;

        allStock.AddRange(fruitStock);
        allStock.AddRange(produceStock);
    }

    public StockInfo GetInfo(string name)
    {
        for (int i = 0; i < allStock.Count; i++)
        {
            if (allStock[i].name == name)
            {
                return allStock[i];
            }
        }

        return null;
    }
}
