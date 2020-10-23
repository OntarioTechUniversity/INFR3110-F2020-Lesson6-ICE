using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameController : MonoBehaviour
{
    [Header("Tile Layout")]
    public int MaxTiles;
    public float Threshold;
    public List<GameObject> Tiles;
    public GameObject farthestTile;

    [Header("Player Related")]
    public GameObject player;
    public Transform playerSpawnPoint;

    [Header("Enemy Related")] 
    public GameObject enemy;
    public Transform enemySpawnPoint;

        [Header("UI Elements")]
    public TMP_Text TileCount;
    public Image MiniMap;

    private Dictionary<string, bool> m_tilePosition = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        Tiles = new List<GameObject>();
        _Reset();
    }

    public bool AddTile(Vector3 new_location)
    {
        var key = new_location.x + "" + new_location.y + "" + new_location.z;
        if (!m_tilePosition.ContainsKey(key))
        {
            if (Tiles.Count <= MaxTiles - 1)
            {
                var newTile = TileFactory.Instance().CreateTile(new_location);
                Tiles.Add(newTile);
                m_tilePosition[key] = true;

                return true;
            }
        }

        return false;
    }

    public int TileSize()
    {
        return Tiles.Count;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _Reset();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            _Quit();
        }

        if (Input.GetKeyDown(KeyCode.M) || Input.GetAxis("Submit") > 0.1)
        {
            _ToggleMiniMap();
        }

        TileCount.text = "Tile Count: " + Tiles.Count.ToString();
    }

    IEnumerator ThresholdCheck()
    {
        yield return new WaitForSeconds(5.0f);
        if ((float)Tiles.Count < ((float)MaxTiles * Threshold))
        {
            _Reset();
        }

        _FindFarthestTile();
    }

    private void _Reset()
    {
        if (Tiles.Count > 0)
        {
            Tiles.Clear();
            m_tilePosition.Clear();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        player.gameObject.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerSpawnPoint.position;
        player.transform.rotation = playerSpawnPoint.rotation;
        player.gameObject.GetComponent<CharacterController>().enabled = true;
        AddTile(Vector3.zero);

        StartCoroutine(ThresholdCheck());
    }

    private void _Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    private void _FindFarthestTile()
    {
        float maxDistance = 0.0f;
        foreach (var tile in Tiles)
        {
            var distance = Vector3.Distance(player.transform.position, tile.transform.position);
            maxDistance = Mathf.Max(distance, maxDistance);
            if (distance == maxDistance)
            {
                farthestTile = tile;
            }
        }

        enemySpawnPoint.position = new Vector3(farthestTile.transform.position.x, 100.0f, farthestTile.transform.position.z);
        enemy.transform.position = enemySpawnPoint.transform.position;
    }

    private void _ToggleMiniMap()
    {
        MiniMap.enabled = !MiniMap.enabled;
    }

}
