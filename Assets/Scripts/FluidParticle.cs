using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidParticle : MonoBehaviour
{

    public Vector2 worldPosition;
    public Vector2 spawnPosition;

    public void Move()
    {
        Vector2 toMove = Fluid.singleton.VectorField(this) * Time.deltaTime;
        worldPosition += toMove;
        this.transform.Translate(toMove);
    }

    public void Reset()
    {
        transform.localPosition = Vector2.zero;
        worldPosition = spawnPosition;
    }

}
