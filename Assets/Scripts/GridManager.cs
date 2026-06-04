using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public int rows = 9;
    public int cols = 9;
    public float cellSize = 2.2f; // espacio entre casillas

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        PlatformType[,] layout = new PlatformType[,]
        {
        { PlatformType.Teleport, PlatformType.AbilityWhite, PlatformType.Normal, PlatformType.Normal, PlatformType.Spawn, PlatformType.Normal, PlatformType.Normal, PlatformType.AbilityWhite, PlatformType.Teleport },
        { PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal },
        { PlatformType.Elimination, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Elimination, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Elimination },
        { PlatformType.Elimination, PlatformType.Normal, PlatformType.Normal, PlatformType.Elimination, PlatformType.Elimination, PlatformType.Elimination, PlatformType.Normal, PlatformType.Normal, PlatformType.AbilityBlue },
        { PlatformType.Launch, PlatformType.AbilityWhite, PlatformType.Normal, PlatformType.Normal, PlatformType.AbilityPurple, PlatformType.Normal, PlatformType.Normal, PlatformType.AbilityWhite, PlatformType.Launch },
        { PlatformType.AbilityBlue, PlatformType.Normal, PlatformType.Normal, PlatformType.Elimination, PlatformType.Elimination, PlatformType.Elimination, PlatformType.Normal, PlatformType.Normal, PlatformType.Elimination },
        { PlatformType.Elimination, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Elimination, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Elimination },
        { PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal, PlatformType.Normal },
        { PlatformType.Teleport, PlatformType.AbilityWhite, PlatformType.Normal, PlatformType.Normal, PlatformType.Spawn, PlatformType.Normal, PlatformType.Normal, PlatformType.AbilityWhite, PlatformType.Teleport },
        };

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector3 pos = new Vector3(c * cellSize, 0, r * cellSize);
                GameObject obj = Instantiate(platformPrefab, pos, Quaternion.identity, transform);
                obj.GetComponent<Platform>().platformType = layout[r, c];
                obj.GetComponent<Platform>().Initialize();
                SetLaunchDirection(obj, r, c);
            }
        }
    }
    void SetLaunchDirection(GameObject obj, int row, int col)
    {
        Platform p = obj.GetComponent<Platform>();
        if (p.platformType != PlatformType.Launch) return;

        // Definí acá la dirección de cada plataforma de lanzamiento según su posición
        if (row == 4 && col == 0) p.launchDirection = Vector3.right;   // fila 4, col 0 → lanza a la derecha
        if (row == 4 && col == 8) p.launchDirection = Vector3.left;    // fila 4, col 8 → lanza a la izquierda
    }
}