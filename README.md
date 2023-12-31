# Практикум. AI противников

## Задание

Собрать тестовую сцену “Зиккураты” для проверки ИИ ботов.
“Зиккураты” - это игровая карта, где постоянно создаются новые персонажи под управлением бота, затем персонажи вступают в бой друг с дружкой, погибают и цикл повторяется. Игрок выступает в роли наблюдателя, который может в режиме реального времени балансировать персонажей.

## Обязательные требования

Создать сцену, в которой:
- Расположено три Зиккурата разных цветов;
- Каждый Зиккурат имеет собственные настройки параметров создаваемых персонажей;
- Зиккураты создают юнитов, окрашенных в свой цвет, и отправляют их в центр карты, где те действуют по логике, описываемой далее.

Создаваемые юниты должны:
- Находить ближайшего не союзного юнита и двигаться к нему;
- При приближении, юнит атакует быстрой и слабой или медленной и сильной атакой. В случае победы - переключается на следующего противника.
- У юнитов есть следующие параметры: здоровье, скорость перемещения, урон от слабой и от медленной атаки, вероятность промаха, вероятность двукратного урона, процентное соотношение вероятности слабой и сильной атак.

У игрока имеется:
- Управление посредством WASD и мышки или аналогично управлению камеры в редакторе;
- Выделение юнита или Зиккурата по щелчку мыши;
- При выделении Зиккурата, игроку открывается панель с параметрами создаваемых юнитов, при изменении которых, следующие создаваемые юниты имеют измененные параметры.

## Ограничения

- Вся информация о юнитах и Зиккуратах должна выводиться в игровом UI при нажатии мышкой на соответствующий игровой объект. UI должен позволять изменять баланс во время игры.
- Все описанные в пункте 2.3 свойства юнитов должны влиять на игру, то есть обязательно реализовать логику, учитывающие перечисленные параметры.
- Все добавленные панели UI должны быть выдвижными, то есть по нажатию на шапку прятаться/извлекаться. Движение необходимо реализовать через Update() или корутины, во время анимации, кнопки не должны нажиматься.

## Дополнительные требования

Расширить возможности игрока, добавив дополнительную панель управления, на которой:
- Кнопка “Убить всех” - уничтожает всех живых юнитов;
- Кнопка “Отобразить здоровье” - открывает/скрывает шкалы здоровья над каждым юнитом.

Добавить логику блуждания для ботов, в случае, если в центре карты нет противников;

Добавить панель со статистикой по Зиккуратам:
- Количество живых юнитов;
- Количество убитых юнитов;
- Оставшееся время до создания нового юнита;
- Кнопка “Очистить” - сбрасывающая счетчик убитых юнитов.
