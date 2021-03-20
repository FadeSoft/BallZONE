using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.UI;

public class IAPController : MonoBehaviour,IStoreListener
{

    IStoreController controller;
    public string[] product;
    public Text cointext;

    public bool delete = true;


    private void Start()
    {
        if (delete)
            PlayerPrefs.DeleteAll();


        IAPStart();
    }

    private void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        
        foreach(string item in product)
        {
            builder.AddProduct(item,ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this, builder);

    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Error" + error.ToString());
    }
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Error while buying" + p.ToString());

    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        // satın alma gerçekleştiği zaman aktif olacak fonksiyon

        if (string.Equals(e.purchasedProduct.definition.id,product[0],StringComparison.Ordinal))
        {
            //element 0 ı aldıgın zaman
            AddCoin(100);
            return PurchaseProcessingResult.Complete;

        }
        else if (string.Equals(e.purchasedProduct.definition.id, product[1], StringComparison.Ordinal))
        {
            //element 1 ı aldıgın zaman
            AddCoin(200);
            return PurchaseProcessingResult.Complete;

        }
        else if (string.Equals(e.purchasedProduct.definition.id, product[2], StringComparison.Ordinal))
        {
            //element 2 ı aldıgın zaman
            AddCoin(300);

            return PurchaseProcessingResult.Complete;
        }
        else if (string.Equals(e.purchasedProduct.definition.id, product[3], StringComparison.Ordinal))
        {
            //element 3 ı aldıgın zaman
            AddCoin(400);

            return PurchaseProcessingResult.Complete;
        }
        else if (string.Equals(e.purchasedProduct.definition.id, product[4], StringComparison.Ordinal))
        {
            //element 4 ı aldıgın zaman
            AddCoin(500);

            return PurchaseProcessingResult.Complete;
        }
        else if (string.Equals(e.purchasedProduct.definition.id, product[5], StringComparison.Ordinal))
        {
            //element 5 ı aldıgın zaman
            AddCoin(600);

            return PurchaseProcessingResult.Complete;
        }
        else
        {
            return PurchaseProcessingResult.Pending;

        }
    }
    private void AddCoin(int coin)
    {
        cointext.text = coin.ToString() + " altın satın aldı";
    }
    
    public void IAPButton(string id )
    {
        Product proc = controller.products.WithID(id);
        if(proc != null && proc.availableToPurchase)
        {
            Debug.Log("Buying...");
            controller.InitiatePurchase(proc);

        }
        else
        {
            Debug.Log("Not ");
        }
    }
}
