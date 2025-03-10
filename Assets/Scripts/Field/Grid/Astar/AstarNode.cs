[System.Serializable]
public class AstarNode
{
    public AstarNode(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public AstarNode ParentNode;


    public int x, y, G, H;
    public int F { get { return G + H; } }
}