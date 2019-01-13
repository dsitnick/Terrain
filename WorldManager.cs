using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    public static WorldManager SINGLETON { get; private set; }

    public Biome[] Biomes;

    [Range (0, 10)]
    public float Frequency = 1;

    [Range (0, 10)]
    public float Amplitude = 1;

    private void Awake () {
        SINGLETON = this;
        World.Biomes = Biomes;
    }

}
