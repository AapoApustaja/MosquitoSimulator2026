using UnityEngine;
using System.Linq;
using NUnit.Framework;

using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataManager instance {  get; private set; }

    private void Awake()
    {

        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        // Don't load game automatically - wait for menu to call InitializeGame()
    }

    public void InitializeGame()
    {
        LoadGame();
    }

    public void InitializeDataPersistence()
    {
        // Initialize data persistence objects with current gameData (for new games)
        if (dataPersistenceObjects == null || dataPersistenceObjects.Count == 0)
        {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Data persistence objects initialized with new game data.");
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            NewGame();
        }

        // Make sure dataPersistenceObjects is initialized
        if (dataPersistenceObjects == null || dataPersistenceObjects.Count == 0)
        {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // Ensure gameData exists before saving
        if (gameData == null)
        {
            gameData = new GameData();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    // autosave jos sammuttaa peli
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IDataPersistence[] dataPersistenceObjects =
        FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>()
            .ToArray();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
