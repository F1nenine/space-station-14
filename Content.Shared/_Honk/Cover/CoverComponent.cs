using Robust.Shared.GameStates;

namespace Content.Shared._Honk.Cover;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class CoverComponent : Component
{
    /// <summary>
    /// Шанс блокировки урона (от 0 до 1)
    /// </summary>
    [DataField("blockChance"), ViewVariables(VVAccess.ReadWrite), AutoNetworkedField]
    public float BlockChance = 0.5f;
}
