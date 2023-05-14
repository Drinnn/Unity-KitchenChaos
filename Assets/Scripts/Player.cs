using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private float playerRadius = .7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    [SerializeField] private GameInput gameInput;

    public bool IsWalking { get; private set; }
    
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter SelectedCounter;
    }

    private Vector3 _lastInteractionDir;
    private ClearCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Player detected!");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteraction += GameInput_OnInteraction;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    
    private Vector3 GetMovementDirection()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        return new Vector3(inputVector.x, 0f, inputVector.y);
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (_selectedCounter)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void HandleMovement()
    {
        Vector3 moveDir = GetMovementDirection();
        float moveDistance = moveSpeed * Time.deltaTime;        
        
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

        IsWalking = moveDir != Vector3.zero;
    }

    private void HandleInteractions()
    {
        Vector3 moveDir = GetMovementDirection();
        if (moveDir != Vector3.zero)
        {
            _lastInteractionDir = moveDir;
        }

        if (Physics.Raycast(transform.position, _lastInteractionDir, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != _selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = _selectedCounter
        });
    }

    public Transform GetKitchenObjectPointTransform()
    {
        return kitchenObjectHoldPoint;
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
