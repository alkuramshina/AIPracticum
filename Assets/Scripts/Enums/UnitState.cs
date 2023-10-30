namespace Enums
{
    public enum UnitState: byte
    {
        // Дефолтное поведение, двигается в сторону базовой цели (центр карты)
        Seeking = 0,
        // Преследует конкретного юнита
        Pursuing,
        // Сражается с конкретным юнитом
        Fighting,
        // Блуждает без цели
        Wandering,
        // Мертв
        Dead
    }
}