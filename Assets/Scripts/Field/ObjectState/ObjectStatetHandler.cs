public class ObjectStatetHandler
{
    ObjectState objectState;
    public void ChangeState(ObjectState newState)
    {
        objectState?.Exit();
        objectState = newState; 
        objectState?.Enter();    
    }
    public void Execute()
    {
        objectState?.Execute(); 
    }
}