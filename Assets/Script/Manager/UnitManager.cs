using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public int _countObjective;

    public BaseUnit OccupiedUnit;

    public BaseHero SelectedHero;

    public bool _done = false;
    public bool dest = false;

    public List<Vector2> _data = new List<Vector2>();
    public List<Tile> _tiles = new List<Tile>();
    public Tile randomSpawnTile;
    private string temp;

    string[] movement;
    int counting = 0;
    public enum state
    {
        first,
        second,
        third,
        freeze
    }
    state curState;
    public void setState(state _state)
    {
        curState = _state;
    }
    public state getState()
    {
        return curState;
    }
    private void Start()
    {
        setState(state.first);
        
        
    }
    private void Update()
    {
        
    }

    void Awake()
    {
        Instance = this;
        
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();        
    }

    public void SpawnHeroes()
    {
        
            var heroCount = 1;
            for (int i = 0; i < heroCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
                var spawnedHero = Instantiate(randomPrefab);
                randomSpawnTile = GridManager.Instance.GetHeroRespawnTile();

                randomSpawnTile.SetUnit(spawnedHero);
            }

            GameManager.Instance.ChangeState(GameState.SpawnFlag);            
        
        
    }
    public void MoveHeroes()
    {
        randomSpawnTile.DeleteUnit();
        //SpawnHeroes();
        
        StartCoroutine(wait(1));
    }
    IEnumerator wait(float time)
    {
        for (int i = 0; i < _data.Count; i++)
        {          
            
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroMoveTile(_data[i].x, _data[i].y);

            randomSpawnTile.SetUnit(spawnedHero);
            //Destroy(_tiles[i].OccupiedUnit.gameObject);
            yield return new WaitForSeconds(time);
            
            var destroy = GridManager.Instance.GetHeroMoveTile(_data[i].x, _data[i].y);
            destroy.DeleteUnit();

        }
    }


    public void SpawnFlag()
    {
        var enemyCount = 1;
        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseFinish>(Faction.Finish);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemyRespawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.SpawnObjective);
    }
    public void SpawnObjective()
    {
        //var enemyCount = 2;
        for (int i = 0; i < _countObjective; i++)
        {
            var randomPrefab = GetRandomUnit<BaseObjective>(Faction.Objective);
            var spawnedObjective = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetObjectiveRespawnTile();

            randomSpawnTile.SetUnit(spawnedObjective);
        }
        GameManager.Instance.ChangeState(GameState.SpawnObstacle);
    }
    public void SpawnObstacle()
    {
        var enemyCount = 1;
        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseObstacle>(Faction.Obstacle);
            var spawnedObstacle = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetObstacleRespawnTile();

            randomSpawnTile.SetUnit(spawnedObstacle);
        }
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }
    public void getPosition(Tile tiles)
    {
        //temp adalah local variabel string UnitManager
        temp = tiles.name;
        //if (curState == state.first)
        //{
        //    addMovement(getVector2(temp));
        //    setState(state.second);
        //}
        //addMovement(getVector2(temp));
        if(curState == state.first)
        {
            validation(temp, tiles);
            showList();
        }

    }
  
    public void validation(string strz, Tile tlz)
    {
        if (_data.Count < 2)
        {
            addMovement(getVector2(strz));
            _tiles.Add(tlz);
            return;
            
        }
        if(_data.Count >= 2)
        {
            if (_data[_data.Count - 2] == getVector2(strz))
            {
                _data.RemoveAt(_data.Count - 1);
                _tiles.RemoveAt(_tiles.Count - 1);
                return;
            }
            else if (_data[_data.Count-1] != getVector2(strz))
            {
                addMovement(getVector2(strz));
                _tiles.Add(tlz);
                return;
            }
        } 
    }
    public void addMovement(Vector2 tempz)
    {
        _data.Add(tempz);
    }
    public void showTiles()
    {
        for(int i = 0; i< _tiles.Count; i++)
        {
            Debug.Log(_tiles[i].ToString());

        }
    }
    public void showList()
    {
        
        for(int i = 0; i < _data.Count; i++)
        {
            Debug.Log(_data[i].ToString());
        }
        
    }
    public Vector2 getVector2(string rString)
    {
        string[] temp = rString.Split(' ');
        float x = System.Convert.ToSingle(temp[1]);
        float y = System.Convert.ToSingle(temp[2]);
        Vector2 rValue = new Vector2(x, y);
        return rValue;
    }
    




}
