public class Cooldown
{
    private float _cooldownTime = 1f;

    public bool Enabled { get; private set; } = false;

    public Cooldown(float cooldownTime)
    {
        _cooldownTime = cooldownTime;
    }

    public async void Start()
    {
        Enabled = true;

        await System.Threading.Tasks.Task.Delay((int)(_cooldownTime * 1000));
        
        Enabled = false;
    }
}
