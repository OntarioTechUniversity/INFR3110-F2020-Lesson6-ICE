using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Duplicate : MonoBehaviour
{
    [SerializeField]
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        StartCoroutine(_generateRandomTiles());
    }

    IEnumerator _generateRandomTiles()
    {
        yield return null;
        var numRandomTiles = Random.Range(1, 4);
        int[] tilePosition = {0, 0, 0, 0, 0, 0};

        for (var count = 0; count < numRandomTiles; count++)
        {
            var randomDirection = 0;
            var randomDirectionProb = Random.Range(1, 101);
            randomDirection = (randomDirectionProb < 99) ? Random.Range(0, 4) : Random.Range(4, 6);
            
            if (tilePosition[randomDirection] == 0)
            {
                tilePosition[randomDirection] = 1;
            }
            else
            {
                continue;
            }

            Vector3 newTileLocation;

            switch ((Direction) randomDirection)
            {
                case Direction.BACK:
                    newTileLocation = transform.position + Vector3.back * transform.localScale.z;
                    break;
                case Direction.FORWARD:
                    newTileLocation = transform.position + Vector3.forward * transform.localScale.z;
                    break;
                case Direction.LEFT:
                    newTileLocation = transform.position + Vector3.left * transform.localScale.x;
                    break;
                case Direction.RIGHT:
                    newTileLocation = transform.position + Vector3.right * transform.localScale.x;
                    break;
                case Direction.UP:
                    newTileLocation = transform.position + Vector3.up * transform.localScale.y;
                    break;
                case Direction.DOWN:
                    newTileLocation = transform.position + Vector3.down * transform.localScale.y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            gameController.AddTile(newTileLocation);

        }
    }


}
