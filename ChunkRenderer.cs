using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer : MonoBehaviour {

    public static ChunkRenderer SINGLETON { get; private set; }

    public Mesh[] Meshes;

    public Material TerrainMaterial, PropMaterial;

    private void Awake () {
        SINGLETON = this;
    }

    private float getHeight(int x, int y) {
        return World.GetHeight (x, y);
    }

    private void Update () {
        foreach (Chunk c in ChunkManager.SINGLETON.Chunks.Values) {
            drawChunk (c);
        }
    }

    private void drawChunk(Chunk chunk){
        Graphics.DrawMesh (chunk.TerrainMesh, new Vector3 (chunk.Key.x, 0, chunk.Key.y) * Chunk.CHUNK_SIZE, Quaternion.identity, TerrainMaterial, 0);

        foreach (Chunk.Prop p in chunk.Props) {
            drawProp (p);
        }
    }

    private void drawProp(Chunk.Prop prop) {
        Graphics.DrawMesh (Meshes[prop.MeshIndex], prop.Position, prop.Rotation, PropMaterial, 0);
    } 


}
