public interface IDamageable<T>
{
    void Damage(T damageTaken, DamageTypes types);
    
} 

public enum DamageTypes
{
    Normal,
    Lighting,
    Ice,
    Fire,
    Poison,
}