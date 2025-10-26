The saving/loading system has 4 main scripts, SaveData.cs, ISaveable.cs, TransformSaver.cs, and GameManager.cs.

SaveData.cs
- Contains the serializable classes that structure the saved data
- SaveData is the main container for holding the score and object data
- PlayerData stores the player's position
- TargetData stores the target position, point value, speed, size, and color
- SaveDataContainer is a wrapper that holds the player object and multiple targets for serialization
- This script is used by TransformerSaver.cs to package the data and by GameManager for file operations

ISaveable.cs
- Defines the interface that all saveable objects must implement
- SaveIS is the unique ID for each object
- LoadFromData() is a method that is used to restore the object from the saved data
- SavedData is the property that returns the object's current state

TransformSaver.cs
- Allows the attached game object to be saved and loaded
- Called by GameManager.cs during saving and loading operations
- Provides the object-specific data to the GameManager
- Inherits and implements the ISaveable interface

GameManager.cs
- Manages the saving and loading process
- SaveGame() is called by pressing the P key and calls SaveScoreBinary() and SavePositionsJSON()
- LoadGame() is called by pressing the L key and calls LoadScoreBinary() and LoadPositionsJSON()
- RecreateTarget() rebuilds the targets using TargetBuilder.cs