using System.Collections.Generic;
using UnityEngine;

public abstract class FieldOBJ : MonoBehaviour, IGridObject
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private ObjectStatetHandler objectStatetHandler = new ObjectStatetHandler();

    public Vector3Int Pos { get; protected set; }
    public List<Vector2Int> SizeList { get; protected set; } = new List<Vector2Int>();

    private IMovementGrid movementGrid;
    public virtual void Init(IMovementGrid movementGrid)
    {
        this.movementGrid = movementGrid;
        rb = GetComponent<Rigidbody2D>();
        Pos = Utils.ToVector3Int(transform.position);

        SizeList.Add(Vector2Int.zero); // 기본적으로 1칸 크기

        movementGrid.UpdateGridPos(this, false);
    }

    private void FixedUpdate()
    {
        objectStatetHandler.Execute();
    }

    public void ChangeState(ObjectState newState) => objectStatetHandler.ChangeState(newState);

    public bool CanMove(Vector3Int newPos) => movementGrid.CanMove(this, newPos) && Vector3.Distance(transform.position, Pos) <= 0.01f;


    private void UpdatePos(Vector3Int newPos)
    {
        movementGrid.UpdateGridPos(this, true);
        Pos = newPos;
        movementGrid.UpdateGridPos(this, false);
    }

    public void Move(Vector3Int newPos)
    {
        if (Pos != newPos)
            UpdatePos(newPos);
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }
}
