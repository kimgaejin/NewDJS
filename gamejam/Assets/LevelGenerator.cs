using UnityEngine;


public class LevelGenerator : MonoBehaviour {
    public Texture2D map;
    public ColorToPrefeb[] colorMappings;
	// Use this for initialization
	void Start () {
        GenerateLevel();
	}
    void GenerateLevel() {
        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                GenerateTile(x, y);
            }
        }

    }
    void GenerateTile(int x, int y) {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }
        foreach (ColorToPrefeb colorMapping in colorMappings)
        {
            if (colorMapping.color == pixelColor) {
                
                if (pixelColor == Color.green) {
                   // Debug.Log(x + "   " + y + "   ");
                   // Debug.Log(pixelColor);
                }
                if (pixelColor == Color.blue)
                {
                    Debug.Log(x + "   " + y + "   ");
                   // Debug.Log(pixelColor);
                }
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
