using System.Collections.Generic;
using UnityEngine;

public class ShelfSpaceController : MonoBehaviour
{
    public StockInfo info;
    public int amountOnShelf;
    public List<StockObject> objectsOnShelf;

    public void PlaceStock(StockObject objectToPlace)
    {
        //阻止放置标记
        bool preventPlacing = true;

        if (objectsOnShelf.Count == 0)
        {
            info = objectToPlace.info;
            preventPlacing = false;
        }
        else
        {
            if (objectToPlace.info.name == info.name)
            {
                preventPlacing = false;
            }
        }

        if (!preventPlacing)
        {
            objectToPlace.transform.SetParent(transform);
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

        return objectToReturn;
    }
}
