using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private Segment _tailTemplate;


    public List<Segment> Generate(int size)
    {
        List<Segment> tail = new List<Segment>();

        for (int i = 0; i < size; i++)
        {
            tail.Add(Instantiate(_tailTemplate, transform));

        }
        return tail;
    }
}
