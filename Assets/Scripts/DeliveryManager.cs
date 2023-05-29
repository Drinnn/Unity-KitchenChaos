using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    public List<RecipeSO> WaitingRecipesSOList => _waitingRecipesSOList;
    public int SuccessfulRecipesAmount => _successfulRecipesAmount;
    
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private int maxWaitingRecipes = 4;
    [SerializeField] private float spawnRecipeTimerMax = 4f;
    
    private List<RecipeSO> _waitingRecipesSOList;
    private float _spawnRecipeTimer;
    private int _successfulRecipesAmount;

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

            if (KitchenGameManager.Instance.IsGamePlaying() && _waitingRecipesSOList.Count < maxWaitingRecipes)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                _waitingRecipesSOList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
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
                bool plateContentsMatchesRecipe = true;
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
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    _successfulRecipesAmount++;
                    
                    _waitingRecipesSOList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
}
