using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float plateSpawnTime;
    [SerializeField] private int maxPlatesAmount;

    private float _spawnPlateTimer;
    private int _spawnedPlatesAmount; 

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if (KitchenGameManager.Instance.IsGamePlaying() && _spawnPlateTimer > plateSpawnTime)
        {
            _spawnPlateTimer = 0f;
            if (_spawnedPlatesAmount < maxPlatesAmount)
            {
                _spawnedPlatesAmount++;
                
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (_spawnedPlatesAmount > 0)
            {
                _spawnedPlatesAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
