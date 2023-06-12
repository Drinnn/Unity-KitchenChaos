using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : NetworkBehaviour
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
        _spawnRecipeTimer = 4f;
    }
    
    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = spawnRecipeTimerMax;

            if (KitchenGameManager.Instance.IsGamePlaying() && _waitingRecipesSOList.Count < maxWaitingRecipes)
            {
                int waitingRecipeSOIndex = Random.Range(0, recipeListSO.recipeSOList.Count);

                SpawnNewWaitingRecipeClientRpc(waitingRecipeSOIndex);
            }
        }
    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeSOIndex)
    {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[waitingRecipeSOIndex];
        
        _waitingRecipesSOList.Add(waitingRecipeSO);
                
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
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
                    DeliverCorrectRecipeServerRpc(i);
                    return;
                }
            }
        }
        
        DeliverIncorrectRecipeServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int matchingRecipeSOIndex)
    {
        DeliverCorrectRecipeClientRpc(matchingRecipeSOIndex);
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int matchingRecipeSOIndex)
    {
        _successfulRecipesAmount++;
                    
        _waitingRecipesSOList.RemoveAt(matchingRecipeSOIndex);
                    
        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void DeliverIncorrectRecipeServerRpc()
    {
        DeliverIncorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc()
    {
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
}
