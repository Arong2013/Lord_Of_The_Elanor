public abstract class ObjectState
{
    protected FieldOBJ fieldOBJ;

    public ObjectState(FieldOBJ fieldOBJ)
    {
        this.fieldOBJ = fieldOBJ;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}