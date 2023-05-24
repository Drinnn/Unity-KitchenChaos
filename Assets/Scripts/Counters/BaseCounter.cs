using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
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
    
    public Transform GetKitchenObjectPointTransform()
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
    
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
}
