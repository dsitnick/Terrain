using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    public static Biome[] Biomes;



    public static float GetHeight(float x, float y) {
        float f = Mathf.Pow (10, WorldManager.SINGLETON.Frequency);
        return Mathf.PerlinNoise (x * f, y * f) * WorldManager.SINGLETON.Amplitude;
    }

}

public class Biome {

}
