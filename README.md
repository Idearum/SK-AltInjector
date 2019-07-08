# SK-TinyInjector

Work in progress alternative companion injector for Special K, where injection is performed either through a global hotkey (Alt+X if enabled) or through a manual list of windows in the traybar icon.

Requires [Special K](https://steamcommunity.com/groups/SpecialK_Mods/discussions/0/) installed.

**Note that as Special K is delay injected into the target process, certain features might not be available** (e.g. flip model presentation etc). It is recommended to install a wrapper DLL (hold CTRL+Shift when clicking on the window title) to enable such functionality.


# Instructions

1. Download and extract this tool from the [Releases](https://github.com/Idearum/SK-AltInjector/releases) section.

2. Launch **SK-TinyInjector.exe**

3. Right click on its tray icon (the pokeball 😃) and enable Settings > **Keyboard shortcut (Alt+X)**.
   * The setting will be saved between launches.
   
4. Use **Alt+X** to automatically inject SK into the active window.

By default Special K is only configured to fully initialize itself into Steam games. To allow it to also initalize in non-Steam games, right click on the SK-TinyInjector tray icon and select Settings > **Edit whitelist.ini**. Now within this file, specify a part of the path to the game(s) Special K will be manually loaded into.
   * For example specify "Games" on its own on a line to allow injection into all games that are installed in a location that containes "Games" somewhere within it.
   * Similarly, specify "WindowsApps" to allow injection into UWP/Microsoft Store based titles.
     * Note that support for UWP based games is minimal at the moment.


# Tips and tricks

* Special K's compatibility menu is still accessible if holding down Ctrl+Shift when clicking on a window in the list, or when clicking Alt+X (so Ctrl+Shift+Alt+X) to inject into the active window. This menu will either allow you to re-configure what API Special K will use, or act as a shortcut to install wrapper DLLs or reset the config for the injected game.

* Special K have been confirmed working (although with reduced functionality) for both Void Bastards and Prey on the Microsoft Store when injected in this capacity. A lot of other UWP based titles will not work.


# Future ideas and/or plans

* Automatically add relevant path to whitelist.ini when selecting a window in the list to inject into.

* Better handling of the window list in general (more informative, possibly better filtered, etc).


# Credits

* [Special K](https://gitlab.com/Kaldaien/SpecialK/) for doing the heavy lifting here. This tiny companion piece is basic as hell when compared to the versatility of Special K that makes this companion piece possible. 

* Kudos to Nefarius and their [Injector](https://github.com/nefarius/Injector) which showed me this was possible.

* [Pokeball icon](https://www.iconfinder.com/icons/1337537/game_go_play_pokeball_pokemon_icon) from [Roundicons.com](https://roundicons.com/).
