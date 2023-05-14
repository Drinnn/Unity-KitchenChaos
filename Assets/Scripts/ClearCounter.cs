using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public void Interact()
    {
        Debug.Log("Interact");
        
        GameObject kitchenObject = Instantiate(kitchenObjectSO.prefab, topPoint);
        kitchenObject.transform.localPosition = Vector3.zero;
        
        Debug.Log(kitchenObject.GetComponent<KitchenObject>().KitchenObjectSO.objectName);
    }
}
