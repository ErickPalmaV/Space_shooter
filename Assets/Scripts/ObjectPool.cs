using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize; 
    private List<GameObject> _gameObjectspool = new List<GameObject>();
    private GameController _gameController;

    public void Configure(GameController gameController)
    {
        _gameController = gameController;
    }

    public void StartGame()
    {
        AddGameObjectToPool(poolSize);
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private void AddGameObjectToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instantiate = Instantiate(prefab, this.transform, true); 
            _gameObjectspool.Add(instantiate);
            instantiate.SetActive(false);
            var shipMediator = instantiate.GetComponent<ShipMediator>();
            if (shipMediator != null)
            {
                instantiate.GetComponent<ShipMediator>().Configure(_gameController);
                Debug.Log("Creando Nave enemiga");
            }
        }
    }
    public GameObject RequestGameObject()
    {
        foreach (var gameObject in _gameObjectspool)
        {
            if ( !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                return gameObject;
            }
        }
        AddGameObjectToPool(1);
        return _gameObjectspool[^1];
    }

    public void DisableAll()
    {
        foreach (var gameObject in _gameObjectspool)
        {
            gameObject.SetActive(false);
        }
    }
}
