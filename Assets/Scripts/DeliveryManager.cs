using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private int maxWaitingRecipes = 4;
    [SerializeField] private float spawnRecipeTimerMax = 4f;
    
    private List<RecipeSO> _waitingRecipesSOList;
    private float _spawnRecipeTimer;

    private void Awake()
    {
        Instance = this;
        
        _waitingRecipesSOList = new List<RecipeSO>();
    }
    
    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = spawnRecipeTimerMax;

            if (_waitingRecipesSOList.Count < maxWaitingRecipes)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                _waitingRecipesSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipesSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipesSOList[i];

            if (waitingRecipeSO.kitchenObjectList.Count == plateKitchenObject.KitchenObjectSOList.Count)
            {
                bool plateCountentsMatchesRecipe = true;
                foreach (var recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectList)
                {
                    bool ingredientFound = false;
                    foreach (var plateKitchenObjectSO in plateKitchenObject.KitchenObjectSOList)
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateCountentsMatchesRecipe = false;
                    }
                }

                if (plateCountentsMatchesRecipe)
                {
                    Debug.Log("Player delivered the correct recipe");
                    _waitingRecipesSOList.RemoveAt(i);
                    return;
                }
            }
        }
        
        Debug.Log("No recipe found!");
    }
}
