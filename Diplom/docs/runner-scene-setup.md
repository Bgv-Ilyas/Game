# RunnerScene Setup (Unity)

## 1) Создай сцену
- Создай сцену `RunnerScene`.
- Добавь `Main Camera` (Orthographic) и выстави pixel-perfect параметры.
- Добавь `Global Light 2D` (если URP 2D).

## 2) Иерархия объектов
- `GameRoot` (Empty):
  - `RunnerBootstrap` (script: `RunnerBootstrap`)
  - `TrackGenerator` (script: `TrackGenerator`)
- `Runner` (Sprite + Rigidbody2D + CapsuleCollider2D):
  - `RunnerController` (script: `RunnerController`)
  - `RunnerCollisionRelay` (script: `RunnerCollisionRelay`)
  - `GroundCheck` (Empty child, под ногами персонажа)
- `Ground` (Tilemap/спрайт-полоса) + `Collider2D`, слой `Ground`.
- `Obstacles` (parent):
  - Несколько префабов/объектов с `Collider2D (isTrigger=true)` + script `Obstacle`
  - Тег для препятствий: `Obstacle`
- `UI`:
  - Canvas (Screen Space - Overlay)
  - `Text` для `Score`, `Best`, `Distance`, `Speed`, `State`
  - `HudController` на отдельном объекте `HUD`.
  - `GameOverPanel` (Panel) с `Text` и кнопкой `Restart`.

## 3) Связывание ссылок в инспекторе
- На `RunnerBootstrap`:
  - `Track Generator` -> объект `TrackGenerator`
  - `Runner Transform` -> `Runner`
  - `Runner Controller` -> компонент на `Runner`
  - Параметры старта: `baseSpeed=5`, `maxSpeed=14`, `scorePerMeter=1`
- На `RunnerCollisionRelay`:
  - `Runner Bootstrap` -> объект `RunnerBootstrap`
- На `RunnerController`:
  - `Ground Mask` -> слой `Ground`
  - `Ground Check` -> child `GroundCheck`
  - `Jump Force` ~ `10`, `Gravity Scale` ~ `3`
- На `HUD/HudController`:
  - `Runner Bootstrap` -> `RunnerBootstrap`
  - привяжи поля `Text` (score/best/distance/speed/state)
  - `Game Over Panel` -> `GameOverPanel`
  - `Game Over Text` -> текст внутри панели
  - `Restart Button` -> кнопка внутри панели
- На `TrackGenerator`:
  - Добавь 3-5 `TrackSegmentConfig` в `segmentConfigs`
  - Для первых: `DifficultyWeight=0.2..0.4`
  - Для поздних: `DifficultyWeight=0.7..1.0`

## 3.1) Теги (обязательно)
- `Runner` должен иметь тег `Player`.
- Все объекты-препятствия должны иметь тег `Obstacle`.

## 4) Горячие клавиши прототипа
- Прыжок: `Space` / `Up`
- Подкат: `Down` / `S`
- Рывок (Ability): `LeftShift` / `D`
- Second Wind (Ability): `Q`
- Shield (PowerUp): `1`
- Score Boost (PowerUp): `2`
- Restart после поражения: `R` (на мобилке также tap по экрану)

## 5) Мобильный запуск
- Переключи платформу на Android/iOS в Build Settings.
- В прототипе уже активируется `MobileInputSource`.
- Для production добавь кнопки UI и/или жесты через `Input System`.

## 6) Минимальный тест-чеклист
- Персонаж движется автоматически вперед.
- Прыжок работает с земли.
- После `Q` доступен один air-jump.
- `Dash` визуально увеличивает темп.
- `1` делает неуязвимость, `2` увеличивает темп очков.
- Сегменты трассы продолжают спавниться перед игроком.
- При столкновении с `Obstacle` без щита статус меняется на `Defeated`.
- При активном щите столкновение не завершает забег.
- HUD обновляет `Score/Distance/Speed/State` в реальном времени.
- После поражения появляется `GameOverPanel`.
- Кнопка `Restart`, клавиша `R` и tap (mobile) перезапускают сцену.
- `Best Score` сохраняется между запусками.
