using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance => instance;
    public GameObject updatePricePanel;
    public TMP_Text priceText, currentPriceText;
    public TMP_InputField priceInputfield;
    private StockInfo activeStockInfo;
    public TMP_Text moneyText;
    public GameObject buyMenuScreen;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            OpenCloseBuyMenu();
        }
    }

    public void OpenUpdatePricePanel(StockInfo stockToUpdate)
    {
        updatePricePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        priceText.text = "$" + stockToUpdate.price.ToString("F2");
        currentPriceText.text = "$" + stockToUpdate.currentPrice.ToString("F2");

        activeStockInfo = stockToUpdate;

        priceInputfield.text = "";
    }

    public void CloseUpdatePricePanel()
    {
        updatePricePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ApplyPriceUpdate()
    {
        if (priceInputfield.text == "")
        {
            CloseUpdatePricePanel();
            return;
        }
        activeStockInfo.currentPrice = float.Parse(priceInputfield.text);

        currentPriceText.text = "$" + activeStockInfo.currentPrice.ToString("F2");

        List<ShelfSpaceController> shelves = new List<ShelfSpaceController>(FindObjectsByType<ShelfSpaceController>(FindObjectsSortMode.None));

        foreach (ShelfSpaceController shelf in shelves)
        {
            if (shelf.info != null && shelf.info.name == activeStockInfo.name)
            {
                shelf.UpdateDisplayPrice();
            }
        }

        CloseUpdatePricePanel();
    }

    public void UpdateMoney(float currentMoney)
    {
        moneyText.text = "$" + currentMoney.ToString("F2");
    }

    public void OpenCloseBuyMenu()
    {
        if (!buyMenuScreen.activeSelf)
        {
            buyMenuScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            buyMenuScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
