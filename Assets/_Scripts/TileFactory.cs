using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileFactory
{
    private GameObject tile;
    private GameController gameController;
    private static TileFactory m_instance = null;

    private TileFactory()
    {
        Start();
    }

    public static TileFactory Instance()
    {
        if (m_instance == null)
        {
            m_instance = new TileFactory();
        }

        return m_instance;
    }

    public GameObject CreateTile(Vector3 location = new Vector3())
    {
        var newTile = MonoBehaviour.Instantiate(tile, location, Quaternion.identity);
        newTile.transform.parent = gameController.transform;
        newTile.name = "Tile " + newTile.GetHashCode();
        return newTile;
    }

    void Start()
    {
        tile = Resources.Load("Prefabs/Tile") as GameObject;
        gameController = GameObject.FindObjectOfType<GameController>();
    }
}
