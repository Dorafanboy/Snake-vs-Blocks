using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpinginess;
    [SerializeField] private int _tailSize;

    private SnakeInput _input;
    private TailGenerator _tailGenerator;
    private List<Segment> _tail;
    public event UnityAction<int> SizeUpdated;

    private void Awake()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _tail = _tailGenerator.Generate(_tailSize);

        _input = GetComponent<SnakeInput>();

        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position +_head.transform.up * _speed * Time.fixedDeltaTime);
        _head.transform.up = _input.GetDirectionOnClick(_head.transform.position);
    }

    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = _head.transform.position; 

        foreach (var segment in _tail)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSpinginess);
            previousPosition = tempPosition;
        }

        _head.Move(nextPosition);
    }

    private void OnBlockCollided()
    {
        Segment deletedSegment = _tail[_tail.Count - 1];
        Destroy(deletedSegment.gameObject);
        _tail.Remove(deletedSegment);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusCollected(int count)
    {
        _tail.AddRange(_tailGenerator.Generate(count));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
