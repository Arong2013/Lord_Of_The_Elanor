using UnityEngine.Tilemaps;

public class MapGrid : Grid<Tile>
{
    public MapGrid(int width, int height) : base(width, height)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                SetValue(x, y, default(Tile));
    }
    public void CreatTile()
    {

    }
}