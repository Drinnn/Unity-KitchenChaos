using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private IKitchenObjectParent _kitchenObjectParent;

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (_kitchenObjectParent != null)
        {
            _kitchenObjectParent.ClearKitchenObject();
        }

        _kitchenObjectParent = kitchenObjectParent;

        if (_kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject!");
        }

        _kitchenObjectParent.SetKitchenObject(this);

        transform.parent = _kitchenObjectParent.GetKitchenObjectPointTransform();
        transform.localPosition = Vector3.zero;
    }
}
