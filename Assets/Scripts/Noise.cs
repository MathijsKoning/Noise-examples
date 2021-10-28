using System;
using System.Numerics;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Noise : MonoBehaviour
{
    // Dimensions.
    public int width = 256;
    public int height = 256;
    
    // Bigger scale means bigger coords,
    // Higher number is zooming-out.
    public float scale = 1f;
    
    // Offset on x-axis & y-axis to create a more random result.
    // No offset = same result each time.
    public float offsetX = 100f;
    public float offsetY = 100f;

    // Mesh Renderer component in Quad GameObject.
    private Renderer _renderer;

    private void Start()
    {
        // Make it Random by using an offset.
        offsetX = 0.05f;
        
        
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // offsetX += 5;
        offsetY += 0.1f;
        _renderer.material.mainTexture = Generate(true);
    }

    Texture2D Generate(bool offset)
    {
        Texture2D texture2D = new Texture2D(width, height);
        
        // MAKE SOME NOISE!
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                texture2D.SetPixel(x, y, Pixel(x, y, offset));
            }
        }

        // Don't forget.
        texture2D.Apply();
        
        return texture2D;
    }

    Color Pixel(int x, int y, bool offset)
    {
        float xCoord = (float) x / width * scale;
        float yCoord = (float) y / height * scale;

        // Offset makes the coordinates random, instead of always yielding the same result.
        if (offset)
        {
            xCoord += offsetX;
            yCoord += offsetY;
        }
        
        // The closer the value to 0.5, 'intenser' the noise. (black, compared to grey - white).
        float pixel = Mathf.PerlinNoise(xCoord, yCoord);

        if (pixel > 0.45 && pixel < 0.55)
        {
            return Color.white;
            
        }
        
        return new Color(0, 0, 255);

        // return new Color(pixel, pixel, pixel);
    }

    Texture2D RandomGenerate()
    {
        Texture2D texture2D = new Texture2D(width, height);
        
        // MAKE SOME NOISE!
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                texture2D.SetPixel(x, y, RandomPixel(x, y));
            }
        }

        // Don't forget.
        texture2D.Apply();
        
        return texture2D;
    }
    
    Color RandomPixel(int x, int y)
    {
        int random = Random.Range(0, 99999);

        return random % 2 == 0 ? Color.black : Color.white;
    }
}
