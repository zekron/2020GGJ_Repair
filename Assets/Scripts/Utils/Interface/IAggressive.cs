/// <summary>
/// Item can attack player
/// </summary>
public interface IAggressive
{
    public void Attack(Damageable damageableTemp);
    public bool CanAttack(GameItemState state, Damageable damageableTemp);
}
