using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;

    [SerializeField] private GameInput gameInput;

    public bool IsWalking { get; private set; }
    
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

        IsWalking = moveDir != Vector3.zero;
    }
}
