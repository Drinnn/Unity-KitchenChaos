using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectPointTransform();
    public KitchenObject GetKitchenObject();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}
