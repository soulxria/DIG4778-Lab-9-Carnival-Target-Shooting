using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameManager gameManagerScript;
    TargetBuilder targetBuilder;
    private int pointValue;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        targetBuilder = gameObject.GetComponent<TargetBuilder>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            gameManagerScript.UpdateScore(targetBuilder.PointValue);
        }
        Destroy(this.gameObject);
    }
}
