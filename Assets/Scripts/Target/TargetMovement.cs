using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    private TargetBuilder targetBuilder;

    void Awake()
    {
        targetBuilder = gameObject.GetComponent<TargetBuilder>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.right * targetBuilder.Speed * Time.deltaTime;
        transform.position += movement;
    }
}
