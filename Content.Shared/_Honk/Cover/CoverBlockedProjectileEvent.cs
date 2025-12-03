using Content.Shared.Damage;
using Robust.Shared.Serialization;

namespace Content.Shared._Honk.Cover;

[Serializable, NetSerializable]
public sealed class CoverBlockedProjectileEvent : EntityEventArgs
{
    public NetEntity Cover;
    public NetEntity Projectile;
    public DamageSpecifier Damage;

    public CoverBlockedProjectileEvent(NetEntity cover, NetEntity projectile, DamageSpecifier damage)
    {
        Cover = cover;
        Projectile = projectile;
        Damage = damage;
    }
}
