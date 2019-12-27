# League Unglitch

Fixes what Riot couldn't fix ;)

## Description
Windows C# application using ALREADY EXISTING functionality to fix existing UI/graphical bugs and issues that prevent players from joining champion select, selecting runes, selecting champions, banning champions, making players dodge champion select etc.

## Functionality
- Kill and restart UI (graphical interface)

- Close UI while keeping League Client alive

- Restore UI from League Client after closing

Kill and restart allows us to quickly and easily fix issues - it WON'T interrupt what you are doing right now! You can restart UI in champion select and you WON'T dodge game. League Client is keeping everything alright, we are simply restarting interface.

Close UI allows us to do just that, leaving only one LeagueClient.exe process alive, consuming only 100 MB of RAM and <0.1% CPU while in background. Why? Well...

Restore UI allows us to restore interface from the LeagueClient.exe that we left alive in the step above. The client will open ULTRAFAST, in about 5 to 10 seconds (depending on machine). You can quickly open it from the taskbar instead of desktop shortcut.

## Author
 - Maciej Gorczyca
 - maciej.dariusz.gorczyca@gmail.com
 
## Donation
 The software is provided to you for free and is open sourced but if you think it's great or saved you few minutes (or hours if you are hardcore crafter), consider sending a donation: https://www.paypal.me/CoUsTme/1EUR
