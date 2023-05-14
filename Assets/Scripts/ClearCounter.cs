using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private KitchenObject _kitchenObject;

    public Transform TopPoint
    {
        get => topPoint;
    }
    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set => _kitchenObject = value;
    }

    public bool HasKitchenObject
    {
        get => _kitchenObject != null;
    }

    public void Interact()
    {
        if (_kitchenObject == null)
        {
            GameObject kitchenObject = Instantiate(kitchenObjectSO.prefab, topPoint);
            kitchenObject.transform.localPosition = Vector3.zero;
            kitchenObject.GetComponent<KitchenObject>().ClearCounter = this;
        }
    }
}
