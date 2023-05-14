using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    
    public KitchenObjectSO KitchenObjectSO
    {
        get => _kitchenObjectSO;
    }

    private ClearCounter _clearCounter;
    public ClearCounter ClearCounter
    {
        get => _clearCounter;
        set
        {
            if (_clearCounter != null)
            {
                _clearCounter.KitchenObject = null;
            }
            
            _clearCounter = value;

            if (_clearCounter.HasKitchenObject)
            {
                Debug.LogError("Counter already has a KitchenObject!");
            }
            _clearCounter.KitchenObject = this;

            transform.parent = _clearCounter.TopPoint;
            transform.localPosition = Vector3.zero;
        }
    }
}
