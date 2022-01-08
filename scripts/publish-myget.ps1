Set-Location ..\aspnet-core\src\AbpDz\
$version = "4.2.102"
((Get-Content -path common.props -Raw) -replace '0.0.1' , $version) | Set-Content -Path common.props
dotnet pack -c release 
# ((Get-Content -path common.props -Raw) -replace $version , '0.0.1') | Set-Content -Path common.props
Set-Location ..\..\

((Get-Content -path common.props -Raw) -replace '0.0.1' , $version) | Set-Content -Path common.props
dotnet pack -c release 
# ((Get-Content -path common.props -Raw) -replace $version , '0.0.1') | Set-Content -Path common.props
 
$destination = "https://www.myget.org/F/badre429/api/v3/index.json"
# $packages = .\nuget.exe list -AllVersions -Source   $source

$packages = Get-ChildItem -Recurse *$version.nupkg | Sort-Object LastWriteTime  -Descending
nuget.exe setapikey "9dddf85c-6065-4e04-a9ee-085c33f82a3b" -Source $destination 

$packages | % {
   
    # $path = [IO.Path]::Combine($source, $id, $version, $nupkg)
    $path = $_.FullName
    Write-Host ".\nuget.exe push  -SkipDuplicate -Source $destination ""$path"""
    nuget.exe push  -SkipDuplicate  -Source   $destination $path 
}
Get-ChildItem *.nupkg -Recurse |Remove-Item

Set-Location ../scripts