using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour {

    public static ChunkManager SINGLETON { get; private set; }

    public Dictionary<Vector2Int, Chunk> Chunks;

    private void Awake () {
        SINGLETON = this;
        Chunks = new Dictionary<Vector2Int, Chunk> ();

        int size = 2;
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                SpawnChunk (new Vector2Int (x, y));
            }
        }
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

    public const int CHUNK_SIZE = 32;

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
    public Mesh TerrainMesh { get; private set; }

    private float[] heights;

    public Chunk (Vector2Int Key) {
        this.Key = Key;
        heights = new float[CHUNK_SIZE * CHUNK_SIZE];
        Props = new List<Prop> ();
        TerrainMesh = GenerateTerrain (Key);
    }

    private const int LENGTH = Chunk.CHUNK_SIZE, VERT_COUNT = 4, TRI_COUNT = 6;
    private static Mesh GenerateTerrain (Vector2Int Key) {
        Mesh mesh = new Mesh ();

        Vector3[] verts = new Vector3[CHUNK_SIZE * CHUNK_SIZE * VERT_COUNT];
        int[] tris = new int[CHUNK_SIZE * CHUNK_SIZE * TRI_COUNT];
        Vector3[] norms = new Vector3[verts.Length];

        int i = 0, cx = Key.x * CHUNK_SIZE, cy = Key.y * CHUNK_SIZE;
        for (int y = 0; y < CHUNK_SIZE; y++) {
            for (int x = 0; x < CHUNK_SIZE; x++) {

                verts[i * VERT_COUNT + 0] = new Vector3(x, World.GetHeight (x + cx, y + cy), y);
                verts[i * VERT_COUNT + 1] = new Vector3 (x + 1, World.GetHeight (x + cx + 1, y + cy), y);
                verts[i * VERT_COUNT + 2] = new Vector3 (x, World.GetHeight (x + cx, y + cy + 1), y + 1);
                verts[i * VERT_COUNT + 3] = new Vector3 (x + 1, World.GetHeight (x + cx + 1, y + 1 + cy), y + 1);

                Vector3 norm = Vector3.Cross (verts[i * VERT_COUNT + 2] - verts[i * VERT_COUNT],
                    verts[i * VERT_COUNT + 1] - verts[i * VERT_COUNT]);
                if (norm.y < 0)
                    norm *= -1;

                for (int n = 0; n < VERT_COUNT; n++) {
                    norms[i * VERT_COUNT + n] = norm;
                }

                tris[i * TRI_COUNT + 0] = i * VERT_COUNT + 0;
                tris[i * TRI_COUNT + 1] = i * VERT_COUNT + 3;
                tris[i * TRI_COUNT + 2] = i * VERT_COUNT + 1;

                tris[i * TRI_COUNT + 3] = i * VERT_COUNT + 0;
                tris[i * TRI_COUNT + 4] = i * VERT_COUNT + 2;
                tris[i * TRI_COUNT + 5] = i * VERT_COUNT + 3;

                i++;
            }
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.normals = norms;

        return mesh;
    }

}

