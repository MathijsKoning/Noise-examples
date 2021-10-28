using UnityEngine;

public class Landmass : MonoBehaviour
{
    public int width = 256;
    public int height = 256; // length
    public int depth = 20; // height on y-axis

    public float scale = 1;
    public float offsetX = 100;
    public float offsetY = 100;

    public Terrain terrain;

    void Start()
    {
        // Make it random.
        // offsetX = Random.Range(0f, 9999f);
        // offsetY = Random.Range(0f, 9999f);
    }

    void Update()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrainData(terrain.terrainData);
        
        // Make it animate.
        offsetX += Time.deltaTime * 2.5f;
        offsetY += Time.deltaTime * 2.5f;
    }

    private TerrainData GenerateTerrainData(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        
        terrainData.size = new Vector3(width, depth, height);
        
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // MAKE SOME NOISE.
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
