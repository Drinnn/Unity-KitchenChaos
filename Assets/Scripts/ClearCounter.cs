using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private KitchenObject _kitchenObject;

    public void Interact(Player player)
    {
        if (_kitchenObject == null)
        {
            GameObject kitchenObject = Instantiate(kitchenObjectSO.prefab, topPoint);
            kitchenObject.transform.localPosition = Vector3.zero;
            kitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            _kitchenObject.SetKitchenObjectParent(player);
        }
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
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    bool IKitchenObjectParent.HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
