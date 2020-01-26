# League Unglitch

Fixes what Riot couldn't fix ;)

## Description
Windows C# application using ALREADY EXISTING functionality to fix existing UI/graphical bugs and issues that prevent players from joining champion select, selecting runes, selecting champions, banning champions, making players dodge champion select etc.

## Functionality
- Reload UI (fix bugs) - built-in function method

- Reload UI (fix bugs) - kill league process method

- Close UI while keeping League Client alive

- Restore UI from League Client after closing

"Reload UI (fix bugs) - built-in function method" allows us to quickly and easily fix issues - it WON'T interrupt what you are doing right now! You can restart UI in champion select and you WON'T dodge game. League Client is keeping everything alright, we are simply restarting interface. It will call built-in function called kill-and-restart-ux.

"Reload UI (fix bugs) - kill league process method" does the same as above but instead of calling built-in function we are forcing client to restart graphical interface by killing the interface process.

Close UI allows us to do just that, leaving only one LeagueClient.exe process alive, consuming only 100 MB of RAM and <0.1% CPU while in background. Why? Well...

Restore UI allows us to restore interface from the LeagueClient.exe that we left alive in the step above. The client will open ULTRAFAST, in about 5 to 10 seconds (depending on machine). You can quickly open it from the taskbar instead of desktop shortcut.

## Author
 - Maciej Gorczyca
 - maciej.dariusz.gorczyca@gmail.com
 
## Donation
 The software is provided to you for free and is open sourced but if you think it's great or saved you from dodging or losing promo, consider sending a donation: https://www.paypal.me/CoUsTme/1EUR
