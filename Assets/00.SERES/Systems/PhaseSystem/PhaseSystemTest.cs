using SERES.Systems.PhaseSystem;

public class PhaseSystemTest : PhaseManager
{
    public override void OnFixedGamePlay()
    {
        base.OnFixedGamePlay();
        print("it`s on fixed game play");
    }

    public override void OnGamePlay()
    {
        base.OnGamePlay();
        print("it`s on game play");
    }

    public override void OnPause()
    {
        base.OnPause();
        print("it`s on pause");
    }
}