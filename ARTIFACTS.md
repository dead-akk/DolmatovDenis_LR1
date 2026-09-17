# Перечень артефактов и протоколов проекта «Calculator»

## 1. Введение

Документ фиксирует состав артефактов проекта «Calculator» (WPF,
.NET Framework 4.7.2) после реорганизации структуры (ЛР №1) и
построения диаграммы компонентов (ЛР №2), а также описывает протоколы
взаимодействия между выделенными модулями: `App`, `Views`, `Services`,
`Models`, `Infrastructure`. Документ предназначен для участников
разработки и служит опорной точкой при дальнейшей интеграции модулей,
код-ревью и сопровождении проекта.

## 2. Перечень артефактов

| Имя                                   | Тип          | Категория     | Назначение                                             | В Git | Расположение         |
|----------------------------------------|--------------|---------------|---------------------------------------------------------|-------|-----------------------|
| Calculator.sln                         | конфигурация | конфигурация  | описание решения Visual Studio                          | да    | корень                |
| Calculator.csproj                      | конфигурация | конфигурация  | описание проекта, ссылки, список файлов сборки          | да    | Calculator/           |
| Calculator.csproj.user                 | конфигурация | конфигурация  | персональные настройки IDE для проекта                  | нет   | Calculator/           |
| App.config                             | конфигурация | конфигурация  | конфигурация .NET-приложения (runtime-настройки)        | да    | Calculator/           |
| App.xaml                               | разметка     | исходный код  | точка входа, декларация ресурсов приложения             | да    | Calculator/           |
| App.xaml.cs                            | исходный код | исходный код  | код точки входа, facade `ChangeTheme`, `BaseTheme`/`EngTheme` | да | Calculator/       |
| Views/MainWindow.xaml(.cs)             | разметка/код | исходный код  | окно обычного калькулятора                              | да    | Calculator/Views/     |
| Views/Window1.xaml(.cs)                | разметка/код | исходный код  | окно инженерного калькулятора                           | да    | Calculator/Views/     |
| Models/CalculatorHistory.cs            | исходный код | исходный код  | хранение истории вычислений (`IHistoryStore`)           | да    | Calculator/Models/    |
| Services/CalculatorLogic.cs            | исходный код | исходный код  | бизнес-логика вычислений (`ICalculatorOps`)             | да    | Calculator/Services/  |
| Services/ICalculatorLogger.cs          | исходный код | исходный код  | интерфейс сервиса логирования                           | да    | Calculator/Services/  |
| Services/CalculatorLogger.cs           | исходный код | исходный код  | facade + файловая реализация логирования                | да    | Calculator/Services/  |
| Infrastructure/ThemeManager.cs         | исходный код | исходный код  | применение цветовых тем (`IThemeProvider`)              | да    | Calculator/Infrastructure/ |
| Properties/AssemblyInfo.cs             | исходный код | исходный код  | метаданные сборки                                        | да    | Calculator/Properties/|
| Properties/Resources.resx / .Designer.cs | ресурс/производный | исходный код | строковые и графические ресурсы приложения        | да    | Calculator/Properties/|
| Properties/Settings.settings / .Designer.cs | конфигурация/производный | конфигурация | пользовательские настройки приложения       | да    | Calculator/Properties/|
| .gitignore                             | конфигурация | конфигурация  | исключение служебных и производных файлов из Git        | да    | корень                |
| README.md                              | документация | проектный     | описание назначения, структуры и запуска проекта        | да    | корень                |
| docs/component-diagram.png             | документация | проектный     | диаграмма компонентов (результат ЛР №2)                 | да    | docs/                 |
| ARTIFACTS.md                           | документация | проектный     | настоящий документ                                       | да    | корень                |
| bin/Debug/Calculator.exe, .pdb, .exe.config | производный | сборка   | результат компиляции                                     | нет   | Calculator/bin/Debug/ |
| obj/Debug/*                            | производный  | сборка        | промежуточные файлы компиляции                           | нет   | Calculator/obj/Debug/ |
| log.txt                                | производный  | эксплуатация  | журнал действий пользователя, создаётся при запуске      | нет   | рядом с .exe          |
| .vs/                                   | вспомогательный | вспомогательный | служебные файлы Visual Studio (кэш, индексы)        | нет   | корень                |

Тестовые и CI/CD-артефакты в проекте на данный момент отсутствуют.

## 3. Протоколы взаимодействия

**Протокол: ICalculatorOps**
Участники: `Views` → `Services`.
Назначение: обработка событий ввода и выполнение вычислений.
Интерфейс: `Num_Click(TextBox, object)`, `Op_Click(TextBox, object)`, `Clear_Click(TextBox)`, `Back_Click(TextBox)`, `Sign_Click(TextBox)`, `Eq_Click(TextBox)`, `Math_Click(TextBox, object)`.
Формат данных: текст дисплея как `string` (`TextBox.Text`); подпись нажатой кнопки передаётся через `object sender`.
Соглашения: имя метода = `<Действие>_Click`, совпадает с именем обработчика события в XAML.

**Протокол: ICalculatorLogger**
Участники: `Services` (`CalculatorLogic`) → `Services` (`CalculatorLogger` / `FileCalculatorLogger`).
Назначение: логирование действий пользователя.
Интерфейс: `void Log(string action)`, `void Clear()`.
Формат данных: текстовая строка вида `[yyyy-MM-dd HH:mm:ss] <действие>`, дописывается в файл `log.txt`.
Соглашения: запись – одна строка на действие; ошибки записи в файл подавляются (`try/catch`), чтобы не нарушать работу калькулятора.

**Протокол: IHistoryStore**
Участники: `Services` → `Models` (запись); `Views` → `Models` (чтение по кнопке «Hist»).
Назначение: хранение истории вычислений за время работы приложения.
Интерфейс: `List<string> Entries`, `static void Add(string expression, string result)`.
Формат данных: строка `"<выражение> = <результат>"`; список хранится только в памяти процесса.
Соглашения: данные не персистентны – очищаются при перезапуске приложения.

**Протокол: IThemeProvider**
Участники: `App` (`Infrastructure`, через facade) → `Infrastructure` (`ThemeManager`).
Назначение: применение выбранной цветовой темы к ресурсам окна.
Интерфейс: `static void ChangeTheme(ResourceDictionary res, string theme)`.
Формат данных: `ResourceDictionary` с парами `"ИмяКлюча" → SolidColorBrush`; тема задаётся строкой (`"Тёмная"`, `"Светлая"`, `"Серая"`, `"Неоновая"`).
Соглашения: ключи ресурсов именуются по шаблону `<Назначение>Brush` (например, `MainBackgroundBrush`).

**Протокол: App-facade (ChangeTheme / BaseTheme / EngTheme)**
Участники: `Views` → `App`.
Назначение: единая точка доступа `Views` к смене темы без прямой зависимости от `Infrastructure`.
Интерфейс: `static void ChangeTheme(ResourceDictionary, string)`, `static string BaseTheme`, `static string EngTheme`.
Формат данных: как в протоколе `IThemeProvider`.
Соглашения: `Views` никогда не обращается к `ThemeManager` напрямую – только через `App`.

Явных пользовательских событий (delegate/event) между модулями не
публикуется: взаимодействие построено на прямых статических вызовах и
стандартных событиях элементов управления WPF (`Click`), обрабатываемых
в code-behind `Views`.

## 4. Соглашения об именовании и версионировании

- Классы и статические фасады – `PascalCase` (`CalculatorLogic`, `ThemeManager`).
- Интерфейсы – префикс `I` (`ICalculatorLogger`).
- Методы-обработчики – `<Действие>_Click`, методы сервисов – глагол + существительное (`Log`, `Add`, `ChangeTheme`).
- Пространства имён соответствуют каталогам: `Calculator.Views`, `Calculator.Models`, `Calculator.Services`, `Calculator.Infrastructure`.
- Ветки Git: на данном этапе используется единственная ветка `main`; при расширении команды рекомендуется схема `feature/*`, `fix/*`.
- Сообщения коммитов – по модели Conventional Commits (`type: описание`), например `Initial commit: project structure reorganized`, `docs: add component diagram (LR2)`, `docs: add ARTIFACTS.md with project artifacts and protocols`.
- Версионирование – семантическое (SemVer, `MAJOR.MINOR.PATCH`) рекомендовано для будущих релизов через теги Git (`git tag -a vX.Y.Z`); на текущем учебном этапе релизы и теги не создавались.

## 5. Заключение

Проведена инвентаризация всех артефактов проекта «Calculator»,
артефакты классифицированы по шести категориям, определены и описаны
пять протоколов взаимодействия между модулями. Выявлен и
зафиксирован пробел структуры – отсутствие тестовых и CI/CD-артефактов,
что является направлением для дальнейшего развития проекта.
