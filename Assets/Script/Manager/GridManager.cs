using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] public int _width, _height;
    [SerializeField] private Tile _grassTile, _mountainTile;
    //[SerializeField] public int[] _x,_y;
    [SerializeField] public int _min, _max;
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    void Awake()
    {
        Instance = this;
    }
     

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        var mountaint = _mountainTile;
        var grass = _grassTile;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                           
                var spawnedGrass = Instantiate(grass, new Vector3(x, y), Quaternion.identity);
                spawnedGrass.name = $"Tile {x} {y}";
                spawnedGrass.Init(x, y);
                _tiles[new Vector2(x, y)] = spawnedGrass;
                
            }
        }   
     

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public Tile GetHeroRespawnTile()
    {        
        return _tiles.Where(t => t.Key.x == 0 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetHeroMoveTile(float x, float y)
    {
        return _tiles.Where(t => t.Key.x == x && t.Key.y == y).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemyRespawnTile()
    {
        //return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
        return _tiles.Where(t => t.Key.x == _width - 1 && t.Key.y == _height / 2).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetObjectiveRespawnTile()
    {
        return _tiles.Where(t => t.Key.x >= _min && t.Key.x <= _max && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetObstacleRespawnTile()
    {
        return _tiles.Where(t => t.Key.x == 1 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
   
}