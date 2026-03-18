using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShelfSpaceController : MonoBehaviour
{
    public StockInfo info;
    public int amountOnShelf;
    public List<StockObject> objectsOnShelf;
    private List<Transform> currentPoints;
    public List<Transform> bigDrinkPoints, cerealPoints, chipsTubePoints, fruitPoints, largeFruitPoints;
    public TMP_Text shelfLable;
    public void PlaceStock(StockObject objectToPlace)
    {
        //阻止放置标记
        bool preventPlacing = true;

        if (objectsOnShelf.Count == 0)
        {
            info = objectToPlace.info;
            preventPlacing = false;

            switch (info.typeOfStock)
            {
                case StockInfo.StockType.bigDrink:
                    currentPoints = bigDrinkPoints;
                    break;
                case StockInfo.StockType.cereal:
                    currentPoints = cerealPoints;
                    break;
                case StockInfo.StockType.chipsTube:
                    currentPoints = chipsTubePoints;
                    break;
                case StockInfo.StockType.fruit:
                    currentPoints = fruitPoints;
                    break;
                case StockInfo.StockType.fruitLarge:
                    currentPoints = largeFruitPoints;
                    break;
            }

            shelfLable.text = "$" + info.currentPrice.ToString("F2");
        }
        else
        {
            if (objectToPlace.info.name == info.name)
            {
                preventPlacing = false;
                if (objectsOnShelf.Count >= currentPoints.Count)
                {
                    preventPlacing = true;
                }
            }
        }

        if (!preventPlacing)
        {
            objectToPlace.transform.SetParent(currentPoints[objectsOnShelf.Count]);
            objectToPlace.MakePlaed();

            objectsOnShelf.Add(objectToPlace);
        }
    }

    public StockObject GetStockObject()
    {
        StockObject objectToReturn = null;
        if (objectsOnShelf.Count > 0)
        {
            objectToReturn = objectsOnShelf[objectsOnShelf.Count - 1];
            objectsOnShelf.RemoveAt(objectsOnShelf.Count - 1);
        }

        if (objectsOnShelf.Count == 0)
        {
            info = null;
            shelfLable.text = "";
        }

        return objectToReturn;
    }

    public void StartUpdatePrice()
    {
        if (objectsOnShelf.Count > 0)
        {
            UIController.Instance.OpenUpdatePricePanel(info);
        }
    }

    public void UpdateDisplayPrice()
    {
        shelfLable.text = "$" + info.currentPrice.ToString("F2");
    }
}
