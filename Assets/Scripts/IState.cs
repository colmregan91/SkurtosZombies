public interface IState
{
    public void OnEnter();

    public void OnUpdate(float DeltaTime );

    public void OnExit();
}