mkdir M01
cd M01
mkdir ArrayHelper
mkdir RectangleHelper
mkdir ConsoleApp
dotnet new sln
cd ArrayHelper
dotnet new classlib
cd..
cd RectangleHelper
dotnet new classlib
cd..
cd ConsoleApp
dotnet new console
cd..
dotnet sln add ArrayHelper/ArrayHelper.csproj RectangleHelper/RectangleHelper.csproj ConsoleApp/ConsoleApp.csproj
dotnet add ConsoleApp/ConsoleApp.csproj reference ArrayHelper/ArrayHelper.csproj
dotnet add ConsoleApp/ConsoleApp.csproj reference RectangleHelper/RectangleHelper.csproj
dotnet build
cls
dotnet run --project ./ConsoleApp
dotnet publish -r win-x64 --self-contained
dotnet publish -r win-arm --self-contained