using Content.Client.Popups;
using Content.Shared._Honk.Cover;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Player;
using Robust.Shared.Random;

namespace Content.Client._Honk.Cover;

public sealed class CoverClientEffectsSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly PopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeNetworkEvent<CoverBlockedProjectileEvent>(OnCoverBlockedClient);
    }

    private void OnCoverBlockedClient(CoverBlockedProjectileEvent ev)
    {
        var cover = GetEntity(ev.Cover);
        if (!Exists(cover)) return;

        var ricochetSounds = new[]
        {
            "/Audio/Weapons/Guns/Hits/ric1.ogg",
            "/Audio/Weapons/Guns/Hits/ric2.ogg",
            "/Audio/Weapons/Guns/Hits/ric3.ogg",
            "/Audio/Weapons/Guns/Hits/ric4.ogg",
            "/Audio/Weapons/Guns/Hits/ric5.ogg"
        };

        var randomSound = _random.Pick(ricochetSounds);
        _audio.PlayEntity(randomSound, Filter.Local(), cover, true);

        // ПОПАП
        _popup.PopupEntity("Блок!", cover);

        // УДАЛЕНИЕ ПУЛИ
        var projectile = GetEntity(ev.Projectile);
        if (Exists(projectile))
            QueueDel(projectile);
    }
}
