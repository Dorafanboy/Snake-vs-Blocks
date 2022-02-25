using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;
    private int _destroyPrice;
    private int _filling;
    public event UnityAction<int> FillingUpdated;
    public int LeftFilling => _destroyPrice - _filling;

    private void Start()
    {
        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);
        FillingUpdated?.Invoke(LeftFilling);
    }

    public void Fill()
    {
        FillingUpdated?.Invoke(LeftFilling);
        _filling++;

        if (_filling == _destroyPrice)
            Destroy(gameObject);
    }
}   
