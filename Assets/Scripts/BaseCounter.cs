using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform topPoint;

    private KitchenObject _kitchenObject;
    
    public abstract void Interact(Player player);

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
        this._kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
