# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/Reloaded.Universal.DInputPleaseCooperate/*" -Force -Recurse
dotnet publish "./Reloaded.Universal.DInputPleaseCooperate.csproj" -c Release -o "$env:RELOADEDIIMODS/Reloaded.Universal.DInputPleaseCooperate" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location