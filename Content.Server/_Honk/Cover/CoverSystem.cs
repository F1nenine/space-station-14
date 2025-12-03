using Content.Shared._Honk.Cover;
using Content.Shared.Projectiles;
using Robust.Shared.Physics.Events;
using Robust.Shared.Random;

namespace Content.Server._Honk.Cover;

public sealed class CoverSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CoverComponent, StartCollideEvent>(OnCoverStartCollide);
    }

    private void OnCoverStartCollide(EntityUid uid, CoverComponent cover, ref StartCollideEvent args)
    {
        var projectileUid = args.OtherEntity;

        // Только снаряды
        if (!HasComp<ProjectileComponent>(projectileUid))
            return;

        // Проверка дистанции
        var distance = (Transform(uid).WorldPosition - Transform(projectileUid).WorldPosition).Length();
        if (distance > 0.81f) return;

        // Шанс блокировки
        if (!_random.Prob(cover.BlockChance))
            return;

        // Удаление
        QueueDel(projectileUid);

        if (!TryComp<ProjectileComponent>(projectileUid, out var projectile))
            return;

        var ev = new CoverBlockedProjectileEvent(
            GetNetEntity(uid),
            GetNetEntity(projectileUid),
            projectile.Damage
        );

        RaiseLocalEvent(ev);
        RaiseNetworkEvent(ev);
    }
}
