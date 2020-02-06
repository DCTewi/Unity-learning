public abstract class State<T>
{
    public virtual void Enter(T e) { }
    public abstract void Execute(T e);
    public virtual void Exit(T e) { }
}
