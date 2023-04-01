# DRRP-Launcher

### launcher.json structure

#### v0

```ts
interface iLauncherJson {
    version: number = 0; // Версия спецификации файла

    engines: Array<iEngine>;
    engine_version: number; // Выбранная версия GZDoom для запуска

    drrp_version: string; // "0.3.x"
    drrp_repository: string; // URL репозитория для клонирования

    api_engines: string; // API для получения списка GZDoom'ов со ссылками для скачивания

    update_on_load: boolean = true; // Обновлять ли список версий DRRP и GZDoom при запуске лаунчера
};

interface iEngine {
    version: number; // Версия GZDoom (3.5.1 -> 351, 3.5 -> 350)
    binary: string; // Путь к .exe файлу относительно главной папки
};
```

#### v? *(Поля, которые планируется добавить позже)*

```ts
interface iLauncherJson {
    language: "ru" | "en";
    engine: number;
};
