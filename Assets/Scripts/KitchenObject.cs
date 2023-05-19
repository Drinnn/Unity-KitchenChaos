using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

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

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void DestroySelf()
    {
        _kitchenObjectParent.ClearKitchenObject();
        
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent kitchenObjectParent)
    {
        GameObject kitchenObjectGameObject = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = kitchenObjectGameObject.transform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
