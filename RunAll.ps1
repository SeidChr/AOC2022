Set-Location '/workspaces/AOC2022'; Get-ChildItem "Day*" 
| ForEach-Object { Push-Location $_.FullName; Write-Host $_.Name;  dotnet run; Pop-Location }