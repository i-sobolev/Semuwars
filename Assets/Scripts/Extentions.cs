using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class Extentions
{
    public static Vector3 RotateByY(this Vector3 v, float angle)
    {
        angle *= Mathf.Deg2Rad;

        var a = new Vector2(v.x, v.z);

        var x = a.x * Mathf.Cos(angle) - a.y * Mathf.Sin(angle);
        var y = a.y * Mathf.Cos(angle) + a.x * Mathf.Sin(angle);

        return new Vector3(x, 0, y);
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).Single();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static T PickRandom<T>(this IEnumerable<T> objects, float[] chances)
    {
        if (objects.Count() != chances.Length)
            throw new System.ArgumentException("Sizes of both the arrays must be equal.");

        var sumChances = chances.Sum();
        var rnd = Random.Range(0, sumChances);

        float sum = 0;
        int i = 0;
        foreach (var item in objects)
        {
            sum += chances[i];
            if (rnd <= sum)
            {
                return item;
            }
            i++;
        }

        return objects.Last();
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
}