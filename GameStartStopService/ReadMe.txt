Install with. then Run:
New-Service -Name "YourServiceName" -BinaryPathName <yourproject>.exe
Remove with:
Remove-Service -Name "YourServiceName"
And:
sc.exe delete "YourServiceName"
Start with: 
Start-Service -Name "eventlog"
Stop with:
Stop-Service -Name "iisadmin" -Force -Confirm
