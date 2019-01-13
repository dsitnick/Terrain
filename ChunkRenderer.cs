using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer : MonoBehaviour {

    public static ChunkRenderer SINGLETON { get; private set; }

    public Mesh[] Meshes;



    public Material TerrainMaterial, PropMaterial;

    public Mesh terrainMesh;

    private const int vert_count = VERT_LENGTH * VERT_LENGTH,
        quad_count = QUAD_LENGTH * QUAD_LENGTH;
    private const int VERT_LENGTH = Chunk.CHUNK_SIZE + 1, QUAD_LENGTH = Chunk.CHUNK_SIZE;

    private void Awake () {
        SINGLETON = this;

        terrainMesh = new Mesh ();

        Vector3[] verts = new Vector3[vert_count];
        int[] tris = new int[quad_count * 6];
        Vector3[] norms = new Vector3[vert_count];

        for (int z = 0; z < VERT_LENGTH; z++) {
            for (int x = 0; x < VERT_LENGTH; x++) {
                Vector3 v = Vector3.zero;
                v.x = x;
                v.z = z;
                v.y = x + z;

                verts[x + (z * VERT_LENGTH)] = v;
                norms[x + (z * VERT_LENGTH)] = Vector3.up;
            }
        }

        int t = 0;
        for (int y = 0; y < QUAD_LENGTH; y++) {
            for (int x = 0; x < QUAD_LENGTH; x++) {
                tris[t] = x + (y * VERT_LENGTH);
                tris[t + 1] = x + ((y + 1) * VERT_LENGTH) + 1;
                tris[t + 2] = x + (y * VERT_LENGTH) + 1;

                tris[t + 3] = x + (y * VERT_LENGTH);
                tris[t + 4] = x + ((y + 1) * VERT_LENGTH);
                tris[t + 5] = x + ((y + 1) * VERT_LENGTH) + 1;

                t += 6;
            }
        }

        for (int i = 0; i < quad_count; i++) {
            //i
            //i + 1
            //i + SIZE
            //i + SIZE + 1

            //23
            //01


            
        }

        terrainMesh.vertices = verts;
        terrainMesh.triangles = tris;
        terrainMesh.normals = norms;

    }



    private void Update () {
        foreach (Chunk c in ChunkManager.SINGLETON.Chunks.Values) {
            drawChunk (c);
        }
    }

    private void drawChunk(Chunk chunk){

        Graphics.DrawMesh (terrainMesh, new Vector3 (chunk.Key.x, 0, chunk.Key.y) * Chunk.CHUNK_SIZE, Quaternion.identity, TerrainMaterial, 0);

        foreach (Chunk.Prop p in chunk.Props) {
            drawProp (p);
        }
    }


    private void drawProp(Chunk.Prop prop) {
        //Graphics.DrawMeshNow (Meshes[prop.MeshIndex], prop.Position, prop.Rotation);
        Graphics.DrawMesh (Meshes[prop.MeshIndex], prop.Position, prop.Rotation, PropMaterial, 0);
    } 


}
