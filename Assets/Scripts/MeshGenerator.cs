using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;
    
    private Vector3[] vertices;
    private int[] triangles;

    // The modifier on the Perlin Noise method.
    public float depth = 20f;
    
    // Vertex count = (xSize + 1) * (zSize + 1)
    public int xSize = 20;
    public int zSize = 20;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        // CreateShape();
        // UpdateMesh();
    }

    private void Update()
    {
        // This is not optimised at all!
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        
        // vertices = new Vector3[]
        // {
        //     new Vector3(0, 0, 0),
        //     new Vector3(0, 0, 1),
        //     new Vector3(1, 0, 0),
        //     new Vector3(1, 0, 1)
        // };

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        triangles = new int[xSize * zSize * 6];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                // y = value between 0 and 1 (noise)
                // often the same, noise isn't about generating random numbers, but pseudo random.
                float y = Mathf.PerlinNoise(x * 0.5f, z * 0.5f) * depth;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
            
        }
        
        int vertice = 0;
        int triangle = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                
                // This janky code is to go over all the vertices one-by-one.
                // However, the triangles have vertices in multiple rows in the 2d array,
                // So we sometimes need xSize to make that little hop.
                triangles[triangle + 0] = vertice + 0;
                triangles[triangle + 1] = vertice + xSize + 1;
                triangles[triangle + 2] = vertice +1;
                triangles[triangle + 3] = vertice + 1;
                triangles[triangle + 4] = vertice + xSize + 1;
                triangles[triangle + 5] = vertice + xSize + 2;
        
                vertice++;
                triangle += 6;
            }
        
            vertice++;
        }
        
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
