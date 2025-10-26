using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public static event Action OnScoreUpdate;

    // Singleton pattern for easy access
    public static GameManager Instance { get; private set; }

    public void OnSave(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SaveGame();
        }
    }

    public void OnLoad(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LoadGame();
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int addedPoints)
    {
        score += addedPoints;
        OnScoreUpdate?.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        OnScoreUpdate?.Invoke();
    }

    private void SaveGame()
    {
        SaveScoreBinary();

        SavePositionsJSON();

        Debug.Log("Game saved successfully");
    }

    private void LoadGame()
    {
        LoadScoreBinary();

        LoadPositionsJSON();

        Debug.Log("Game loaded successfully");
    }

    private void SaveScoreBinary()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "score.dat");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);

        formatter.Serialize(stream, score);
        stream.Close();

        Debug.Log($"Score {score} saved to binary file");
    }

    private void LoadScoreBinary()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "score.dat");

        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            int loadedScore = (int)formatter.Deserialize(stream);
            SetScore(loadedScore);

            stream.Close();
            Debug.Log($"Score {loadedScore} loaded from binary file");
        }
        else
        {
            Debug.LogWarning("No saved score found!");
        }
    }

    private void SavePositionsJSON()
    {
        SaveDataContainer container = new SaveDataContainer();

        TransformSaver[] allSavers = FindObjectsOfType<TransformSaver>();

        foreach (TransformSaver saver in allSavers)
        {
            SaveData data = saver.SavedData;

            if (saver.IsTarget())
            {
                container.targetsData.Add(data);
            }
            else
            {
                container.playerData = data;
            }
        }

        string jsonData = JsonUtility.ToJson(container, true);
        string filePath = Path.Combine(Application.persistentDataPath, "positions.json");
        File.WriteAllText(filePath, jsonData);

        Debug.Log($"Saved {allSavers.Length} objects ({container.targetsData.Count} targets, 1 player)");
    }

    private void LoadPositionsJSON()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "positions.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            SaveDataContainer container = JsonUtility.FromJson<SaveDataContainer>(jsonData);

            // Load player
            if (container.playerData != null)
            {
                TransformSaver playerSaver = FindPlayerSaver();
                if (playerSaver != null)
                {
                    playerSaver.LoadFromData(container.playerData);
                }
                else
                {
                    Debug.Log("playerSaver null");
                }
            }
            else
            {
                Debug.Log("container.playerData null");
            }

            HandleTargetLoading(container.targetsData);

            Debug.Log($"Loaded player and {container.targetsData.Count} targets from JSON");
        }
        else
        {
            Debug.LogWarning("No saved positions found!");
        }
    }

    private void HandleTargetLoading(List<SaveData> targetsData)
    {
        TargetBuilder[] existingTargets = FindObjectsOfType<TargetBuilder>();
        foreach (TargetBuilder target in existingTargets)
        {
            if (target.gameObject.scene.IsValid())
            {
                Destroy(target.gameObject);
            }
        }

        // Recreate targets from saved data
        foreach (SaveData targetData in targetsData)
        {
            if (targetData.targetsData.Count > 0)
            {
                RecreateTarget(targetData.targetsData[0]);
            }
        }

        Debug.Log($"Recreated {targetsData.Count} targets from save data");
    }

    private void RecreateTarget(TargetData targetData)
    {
        TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
        if (spawner == null || spawner.targetPrefab == null)
        {
            return;
        }

        TargetBuilder recreatedTarget = new TargetBuilder.Builder(spawner.targetPrefab)
            .WithSpeed(targetData.speed)
            .WithPointValue(targetData.pointValue)
            .WithSize(targetData.GetSize())
            .WithColor(targetData.GetColor())
            .Build();

        recreatedTarget.transform.position = targetData.GetPosition();

        Debug.Log($"Recreated target at {targetData.GetPosition()} " +
                  $"(Speed: {targetData.speed}, Points: {targetData.pointValue}, " +
                  $"Size: {targetData.GetSize()}, Color: {targetData.GetColor()})");
    }

    private TransformSaver FindPlayerSaver()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.GetComponent<TransformSaver>();
        }
        return null;
    }
}
