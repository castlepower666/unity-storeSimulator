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

        for (int i = 0; i < allStock.Count; i++)
        {
            if (allStock[i].currentPrice == 0)
            {
                allStock[i].currentPrice = allStock[i].price;
            }
        }
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

    public void UpdatePrice(StockInfo stocrName, float newPrice)
    {
        for (int i = 0; i < allStock.Count; i++)
        {
            if (allStock[i].name == name)
            {
                allStock[i].currentPrice = newPrice;
                return;
            }
        }
    }
}
