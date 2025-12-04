using UnityEngine;

public class Price : MonoBehaviour
{
    public PriceDatabase priceData;

    public void AddPrice(int price)
    {
        priceData.price += price;
    }
}
