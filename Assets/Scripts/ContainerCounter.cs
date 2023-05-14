using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        GameObject kitchenObject = Instantiate(kitchenObjectSO.prefab);
        kitchenObject.transform.localPosition = Vector3.zero;
        kitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
