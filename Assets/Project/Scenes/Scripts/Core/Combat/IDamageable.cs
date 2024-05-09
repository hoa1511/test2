public interface IDamageable
{
    public void HandleDamage(int damage, ICanTakeDamage client);
}