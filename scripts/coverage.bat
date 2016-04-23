..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe ^
-register:user ^
-target:"..\packages\xunit.runner.console.2.1.0\tools\xunit.console.exe" ^
-targetargs:"..\tests\SignalR.Client.Portable.Tests\bin\Release\SignalR.Client.Portable.Tests.dll -noshadow" ^
-filter:"+[SignalR.Client.Portable]*" ^
-output:xunit_opencovertests.xml ^
-showunvisited
