using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject _selectedHeroObject, _tileObject, _tileUnitObject, _tileWalk, _tileObjective, _maxObjective, _maxWalk;
    [SerializeField] public GameObject canva;
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        canva.gameObject.GetComponent<Canvas>();
        _maxObjective.GetComponent<Text>().text = UnitManager.Instance.Objective.Length.ToString();
        _maxWalk.GetComponent<Text>().text = UnitManager.Instance.maxWalk.ToString();
    }
    private void Update()
    {
        showObjective();
        showWalk();
    }
    public void showObjective()
    {
        _tileObjective.GetComponentInChildren<Text>().text = UnitManager.Instance.walkcount.ToString();
        _tileObjective.SetActive(true);
    }
    public void showWalk()
    {
        _tileWalk.GetComponentInChildren<Text>().text = UnitManager.Instance._data.Count.ToString();
        _tileWalk.SetActive(true);
    }
    public void ShowWin()
    {
        //if(tile._done == true)
        //{
            canva.SetActive(true);
        //}
    }
    public void ShowTileInfo(Tile tile)
    {

        if (tile == null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        //_tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        //_tileObject.SetActive(true);

        if (tile.OccupiedUnit)
        {
            //_tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            //_tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero)
    {
        if (hero == null)
        {
            _selectedHeroObject.SetActive(false);
            return;
        }
        //_selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        //_selectedHeroObject.SetActive(true);
    }
}