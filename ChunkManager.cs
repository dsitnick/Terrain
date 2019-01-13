using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour {

    public static ChunkManager SINGLETON { get; private set; }

    public Dictionary<Vector2Int, Chunk> Chunks;

    private void Awake () {
        SINGLETON = this;
        Chunks = new Dictionary<Vector2Int, Chunk> ();

        SpawnChunk (Vector2Int.zero);
    }

    public void SpawnChunk (Vector2Int key) {
        Chunk chunk = new Chunk (key);

        Chunks.Add (key, chunk);
    }

    public void DestroyChunk (Vector2Int key) {
        Chunks.Remove (key);
    }
}

public struct Chunk {

    public const int CHUNK_SIZE = 8;

    public struct Prop {

        /*public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }*/
        public Vector3 Position { get { return new Vector3 (Coordinate.x, 0, Coordinate.y); } }
        public Quaternion Rotation { get { return Quaternion.identity; } }

        public Vector2Int Coordinate { get; private set; }
        public int MeshIndex { get; private set; }

        public Prop (int MeshIndex, Vector2Int Coordinate) {
            this.MeshIndex = MeshIndex;
            this.Coordinate = Coordinate;
        }

    }

    public List<Prop> Props { get; private set; }
    public Vector2Int Key { get; private set; }

    private float[] heights;

    public Chunk (Vector2Int Key) {
        this.Key = Key;
        heights = new float[CHUNK_SIZE * CHUNK_SIZE];
        Props = new List<Prop> ();
    }

}

