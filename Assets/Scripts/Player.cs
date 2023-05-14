using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerRadius = .7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask countersLayerMask;

    [SerializeField] private GameInput gameInput;

    public bool IsWalking { get; private set; }

    private Vector3 _lastInteractionDir;

    private void Start()
    {
        gameInput.OnInteraction += HandleInteraction;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleInteraction(object sender, System.EventArgs e)
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
                clearCounter.Interact();                   
            }
        }
    }

    private Vector3 GetMovementDirection()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        return new Vector3(inputVector.x, 0f, inputVector.y);
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
    
}
