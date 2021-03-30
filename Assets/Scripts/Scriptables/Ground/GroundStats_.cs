using Sirenix.OdinInspector;

public class GroundStats_ : TileComponent_
{
    [ShowInInspector]
    public Biome biome;

    public override void Build(TileOwner_ tileOwner)
    {
        base.Build(tileOwner);
        biome = new Biome(((Ground_)tileOwner).biomeType);
    }
}