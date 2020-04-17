echo Build and restore backend libraries...

call dotnet build .\Backend\BuildBackend.xml
if not %ERRORLEVEL% == 0 set ERRORS=1

echo Build and restore frontend libraries...
echo to do..

exit /b %ERRORS%
