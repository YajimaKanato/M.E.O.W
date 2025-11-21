using Interface;
using Item;
using UnityEngine;

public class RottenMeat : BadFoodBase
{
    protected override void ItemActivate()
    {
        Debug.Log("Eat RottenMeat");
    }
}
