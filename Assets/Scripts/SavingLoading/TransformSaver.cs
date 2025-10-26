using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    [SerializeField] private string _saveID;
    public bool isTarget = false;
    
    private TargetBuilder targetBuilder;
    
    public string SaveID 
    { 
        get { return _saveID; } 
        set { _saveID = value; }
    }
    
    void Awake()
    {
        if (string.IsNullOrEmpty(_saveID))
        {
            _saveID = gameObject.name + "_" + System.Guid.NewGuid().ToString();
        }
        
        if (isTarget)
        {
            targetBuilder = GetComponent<TargetBuilder>();
        }
    }
    
    public SaveData SavedData
    {
        get
        {
            SaveData data = new SaveData();
            data.saveID = _saveID;
            
            if (isTarget && targetBuilder != null)
            {
                TargetData targetData = new TargetData(
                    transform.position,
                    targetBuilder.PointValue,
                    targetBuilder.Speed,
                    targetBuilder.Size,
                    targetBuilder.Color,
                    _saveID

                );
                data.targetsData.Add(targetData);
            }
            else
            {
                data.playerData = new PlayerData(transform.position);
            }
            
            return data;
        }
    }
    
    public void LoadFromData(SaveData data)
    {
        if (data.playerData != null && !isTarget)
        {
            transform.position = data.playerData.GetPosition();
            Debug.Log($"Loaded player position: {transform.position}");
        }
        else if (data.targetsData.Count > 0 && isTarget && targetBuilder != null)
        {
            TargetData targetData = data.targetsData[0];
            transform.position = targetData.GetPosition();
            
            Debug.Log($"Loaded target - Position: {targetData.GetPosition()}, " +
                     $"Points: {targetData.pointValue}, Speed: {targetData.speed}, " +
                     $"Size: {targetData.GetSize()}, Color: {targetData.GetColor()}");
        }
    }
    
    public bool IsTarget()
    {
        return isTarget;
    }
}
