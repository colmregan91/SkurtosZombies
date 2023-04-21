using Fusion;

public class TimeUTils : NetworkObject
{
    public static TimeUTils instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public float getRunnerDelta()
    {
        return Runner.DeltaTime;
    }
}
