using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public int _countObjective;

    public BaseHero SelectedHero;
    void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeroes()
    {
        var heroCount = 1;
        for(int i = 0; i< heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroRespawnTile();

            randomSpawnTile.SetUnit(spawnedHero);
        }

        GameManager.Instance.ChangeState(GameState.SpawnFlag);
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
}
