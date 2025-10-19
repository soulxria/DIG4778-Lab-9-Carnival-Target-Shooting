using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerScript;
    [SerializeField] private TextMeshProUGUI score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GameManager.OnScoreUpdate += UpdateUI;
    }

    void UpdateUI()
    {
        score.text = gameManagerScript.GetScore().ToString();
    }
}
