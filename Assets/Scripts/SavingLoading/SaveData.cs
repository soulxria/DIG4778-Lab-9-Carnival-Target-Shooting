using UnityEngine;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

[System.Serializable]
public class SaveData
{
    public string saveID;
    public int score;
    public PlayerData playerData;
    public List<TargetData> targetsData;

    public SaveData()
    {
        targetsData = new List<TargetData>();
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;

    public PlayerData(Vector3 _position)
    {
        position = _position;
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}

[System.Serializable]
public class TargetData
{
    public Vector3 position;
    public int pointValue;
    public float speed;
    public Vector3 size;
    public Color color;
    public string saveID;

    public TargetData(Vector3 _position, int _pointValue, float _speed, Vector3 _size, Color _color, string id)
    {
        position = _position;
        pointValue = _pointValue;
        speed = _speed;
        size = _size;
        color = _color;
    }
    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector3 GetSize()
    {
        return size;
    }

    public Color GetColor()
    {
        return color;
    }
}

[System.Serializable]
public class SaveDataContainer
{
    public SaveData playerData;
    public List<SaveData> targetsData;

    public SaveDataContainer()
    {
        targetsData = new List<SaveData>();
    }
}

