:: docfx metadata ../Hybrid.sln
docfx metadata ../src/**/**.csproj
apicleaner.exe _api
docfx docfx.json
docfx serve _site