using Interface;
using Item;
using UnityEngine;

public class Alcohol : BadFoodBase
{
    protected override void ItemActivate()
    {
        Debug.Log("Drink Alcohol");
    }
}
