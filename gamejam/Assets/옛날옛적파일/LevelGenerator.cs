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
    // 타일맵 생성코드 잘 나오는지에 대한 것은 도트 맵에 해당되는 컬러가 있을시에 그 위치를 찍어줌
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
