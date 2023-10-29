namespace Enums
{
    public enum UnitState: byte
    {
        // Дефолтное поведение, без цели
        Idle = 0,
        // Блуждает в сторону базовой цели (центр карты)
        Wandering,
        // Преследует конкретного юнита
        Seeking,
        // Сражается с конкретным юнитом
        Fighting
    }
}