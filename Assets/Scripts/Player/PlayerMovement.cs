using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5.0f;
    public Vector2 move;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    
    void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }
}
