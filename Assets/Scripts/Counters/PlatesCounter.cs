using System;
using Unity.Netcode;
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
        if (!IsServer)
        {
            return;
        }
        
        _spawnPlateTimer += Time.deltaTime;
        if (_spawnPlateTimer > plateSpawnTime)
        {
            _spawnPlateTimer = 0f;
            if (KitchenGameManager.Instance.IsGamePlaying() && _spawnedPlatesAmount < maxPlatesAmount)
            {
                SpawnPlateServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlateServerRpc()
    {
        SpawnPlateClientRpc();
    }

    [ClientRpc]
    private void SpawnPlateClientRpc()
    {
        _spawnedPlatesAmount++;
                
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (_spawnedPlatesAmount > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                InteractLogicServerRpc();
            }
        }
    }

    [ServerRpc]
    private void InteractLogicServerRpc()
    {
        InteractLogicClientRpc();
    }

    [ClientRpc]
    private void InteractLogicClientRpc()
    {
        _spawnedPlatesAmount--;
        
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }
}
