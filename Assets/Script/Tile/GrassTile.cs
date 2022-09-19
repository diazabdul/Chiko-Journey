using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;

    public override void Init(int x, int y)
    {
        var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
}
