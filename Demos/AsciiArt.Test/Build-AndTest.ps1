if (-not (dotnet tool list -g | Select-String 'dotnet-format')) { 
	dotnet tool install -g dotnet-format
}
#dotnet-format ../AsciiArt/AsciiArt.csproj
#dotnet-format AsciiArt.Test.csproj
dotnet build -v:m -t:Rebuild > bin/build_warnings.txt
dotnet test --no-build --verbosity normal --logger "console;verbosity=normal" --logger "trx;LogFileName=AsciiArt.Test.trx" --results-directory "TestResults"