using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockBoxController : MonoBehaviour
{
    public StockInfo info;
    public List<Transform> bigDrinkPoints, cerealPoints, chipsTubePoints, fruitPoints, largeFruitPoints;
    public List<StockObject> stockInBox;
    public bool testFill;
    public Rigidbody rb;
    public Collider col;
    private bool isHeld;
    public float moveSpeed;
    public GameObject flap1, flap2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (testFill)
        {
            testFill = false;
            SetupBox(info);
        }

        if (isHeld)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, Time.deltaTime * moveSpeed);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * moveSpeed);
        }
    }

    public void SetupBox(StockInfo stockInfo)
    {
        info = stockInfo;

        List<Transform> activePoints = null;

        switch (info.typeOfStock)
        {
            case StockInfo.StockType.cereal:
                activePoints = cerealPoints;
                break;
            case StockInfo.StockType.bigDrink:
                activePoints = bigDrinkPoints;
                break;
            case StockInfo.StockType.chipsTube:
                activePoints = chipsTubePoints;
                break;
            case StockInfo.StockType.fruit:
                activePoints = fruitPoints;
                break;
            case StockInfo.StockType.fruitLarge:
                activePoints = largeFruitPoints;
                break;
        }

        if (stockInBox.Count == 0 && activePoints != null)
        {
            for (int i = 0; i < activePoints.Count; i++)
            {
                StockObject stock = Instantiate(info.prefab, activePoints[i]);

                stockInBox.Add(stock);

                stock.PlaceInBox();
            }
        }
    }

    public void Pickup()
    {
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;

        col.enabled = false;

        isHeld = true;
    }
    public void Release()
    {
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        col.enabled = true;

        isHeld = false;
    }

    public void OpenClose()
    {
        if (flap1.activeSelf)
        {
            flap1.SetActive(false);
            flap2.SetActive(false);
        }
        else
        {
            flap1.SetActive(true);
            flap2.SetActive(true);
        }
    }

    public void PlaceStockOnShelf(ShelfSpaceController shelf)
    {
        if (stockInBox.Count > 0)
        {
            shelf.PlaceStock(stockInBox[stockInBox.Count - 1]);

            if (stockInBox[stockInBox.Count - 1].isPlaced)
            {
                stockInBox.RemoveAt(stockInBox.Count - 1);
            }
        }

        if (flap1.activeSelf)
        {
            OpenClose();
        }
    }
}
