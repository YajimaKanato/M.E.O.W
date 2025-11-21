using Interface;
using Item;
using UnityEngine;

public class Chocolate : BadFoodBase
{
    protected override void ItemActivate()
    {
        Debug.Log("Eat Chocolate");
    }
}
