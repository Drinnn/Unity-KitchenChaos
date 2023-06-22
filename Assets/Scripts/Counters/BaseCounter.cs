using System;
using Unity.Netcode;
using UnityEngine;

public abstract class BaseCounter : NetworkBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    
    [SerializeField] private Transform topPoint;

    private KitchenObject _kitchenObject;

    public virtual void Interact(Player player)
    {
        
    }

    public virtual void InteractAlternative(Player player)
    {
        
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;

        if (_kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
    
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
}
