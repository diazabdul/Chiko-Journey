using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] public GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    public bool _done = false;
    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    public string[] tiled;
    public string nameTile;
    public bool check = false;
    int count = 0;
    public bool way = true;

    public enum state
    {
        moving,
        freeze
    }
    state curState;

    private void Start()
    {
        setState(state.moving); 
    }    
    private void Update()
    {
        
    }
    public virtual void Init(int x, int y)
    {
        
    }
    public void highlight(bool activator)
    {
        if (curState == state.moving)
        {
            _highlight.SetActive(activator);
            MenuManager.Instance.ShowTileInfo(this);
            UnitManager.Instance.getPosition(this);
        }        
        
    }
    
    public void setState(state _state)
    {
        curState = _state;
    }

    public bool checkDone()
    {
        if(_done== true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    
    private void OnMouseEnter()
    {
        if (UnitManager.Instance.getState() == UnitManager.state.freeze)
        {
            return;
        }
        if (OccupiedUnit != null)
        {
            if(OccupiedUnit.Faction == Faction.Obstacle)
            {
                highlight(false);
                return;
            }
            else if (OccupiedUnit.Faction == Faction.Finish && UnitManager.Instance._data.Count >= UnitManager.Instance.moveSuggest-1)
                {
                    highlight(true);
                    UnitManager.Instance.setState(UnitManager.state.freeze);
                    Debug.Log("State Freeze");
                }
            else
            {
                
                highlight(true);
            }
        }
        else
        {
            highlight(true);
        }
        
        
    }
    private void OnMouseExit()
    {
        if (UnitManager.Instance.getNeighbour() == false)
        {
            UnitManager.Instance.setNeighbour(true);
            highlight(false);
        }
        if (UnitManager.Instance.isFinish()==true)
        {
            if (UnitManager.Instance.getState() == UnitManager.state.freeze)
            {
                if (count == 0)
                {
                    UnitManager.Instance.MoveHeroes();
                    
                    //Debug.Log("Exe");
                    count = 1;
                }
            }
        }
        //else
        //{
        //    UnitManager.Instance.setState(UnitManager.state.second);
        //    return;
        //}
        
     
        
    }

    //private void OnMouseOver()
    //{
        

    //    //if (GameManager.Instance.GameState != GameState.HeroesTurn) return;
    //    //if (OccupiedUnit != null)
    //    //{
    //    //    //if (curState == state.freeze)
    //    //    //{
    //    //    //    return;
    //    //    //}
    //    //    //highlight();

    //    //    //if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
    //    //    if (OccupiedUnit.Faction == Faction.Finish)
    //    //    {
    //    //        if (UnitManager.Instance.SelectedHero != null)
    //    //        {
    //    //            var enemy = (BaseFinish)OccupiedUnit;
    //    //            Destroy(enemy.gameObject);
    //    //            Debug.Log("Finish");
    //    //            //UnitManager.Instance.SetSelectedHero(null);
    //    //            SetUnit(UnitManager.Instance.SelectedHero);
    //    //            //var hero = (BaseHero)OccupiedUnit;
    //    //            //UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
    //    //            //UnitManager.Instance.SetSelectedHero(null);
    //    //            //_done = true;
    //    //            //SceneManager.LoadScene("Win");
    //    //            // MenuManager.Instance.ShowWin(this);
    //    //            setState(state.freeze);
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        if (UnitManager.Instance.SelectedHero != null)
    //    //        {
    //    //            SetUnit(UnitManager.Instance.SelectedHero);
    //    //            UnitManager.Instance.SetSelectedHero(null);
    //    //        }
    //    //    }
    //    //}





    //        //if (GameManager.Instance.GameState != GameState.HeroesTurn) return;
    //        //if (OccupiedUnit != null)
    //        //{
    //        //    if (curState == state.freeze)
    //        //    {
    //        //        return;
    //        //    }
    //        //    //highlight();
    //        //    if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
    //        //    //if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
    //        //    else if (OccupiedUnit.Faction == Faction.Finish)
    //        //    {
    //        //        if (UnitManager.Instance.SelectedHero != null)
    //        //        {
    //        //            var enemy = (BaseFinish)OccupiedUnit;
    //        //            Destroy(enemy.gameObject);
    //        //            Debug.Log("Finish");
    //        //            //UnitManager.Instance.SetSelectedHero(null);
    //        //            SetUnit(UnitManager.Instance.SelectedHero);
    //        //            var hero = (BaseHero)OccupiedUnit;
    //        //            UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
    //        //            UnitManager.Instance.SetSelectedHero(null);
    //        //            _done = true;
    //        //            //SceneManager.LoadScene("Win");
    //        //            // MenuManager.Instance.ShowWin(this);
    //        //            setState(state.freeze);
    //        //        }
    //        //    }
    //        //    else if (OccupiedUnit.Faction == Faction.Objective)
    //        //    {
    //        //        if (UnitManager.Instance.SelectedHero != null)
    //        //        {
    //        //            var objective = (BaseObjective)OccupiedUnit;
    //        //            Destroy(objective.gameObject);
    //        //            Debug.Log("Objective Terambil");
    //        //            //UnitManager.Instance.SetSelectedHero(null);
    //        //            SetUnit(UnitManager.Instance.SelectedHero);
    //        //            var hero = (BaseHero)OccupiedUnit;
    //        //            UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
    //        //            UnitManager.Instance.SetSelectedHero(null);

    //        //        }

    //        //    }
    //        //    else if (OccupiedUnit.Faction == Faction.Obstacle)
    //        //    {
    //        //        if (UnitManager.Instance.SelectedHero != null)
    //        //        {
    //        //            //_highlight.SetActive(false);
    //        //            MenuManager.Instance.ShowTileInfo(null);
    //        //        }

    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    if (UnitManager.Instance.SelectedHero != null)
    //        //    {
    //        //        SetUnit(UnitManager.Instance.SelectedHero);
    //        //        UnitManager.Instance.SetSelectedHero(null);
    //        //    }
    //        //}
      
    //}

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
    public void DeleteUnit()
    {
        if (OccupiedUnit.Faction == Faction.Hero)
        {            
            var hero = (BaseHero)OccupiedUnit;
                Destroy(hero.gameObject);            
        }

    }
    public void DeleteObjective()
    {
        var objective = (BaseObjective)OccupiedUnit;
        Destroy(objective.gameObject);
    }

}