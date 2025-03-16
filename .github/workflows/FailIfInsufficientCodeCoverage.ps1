$coverageFilename = Get-ChildItem -Path "..\..\ConsoleGems\ConsoleGems\TestResults" -Recurse -Filter "*.xml";
$coverageFileContent = [xml](Get-Content $coverageFilename);
$methods = $coverageFileContent.GetElementsByTagName("Method");
$failures = $methods | Where-Object {[int]$_.sequenceCoverage -lt 80 -or [int]$_.branchCoverage -lt 80};
if ($failures.Count -gt 0) {
    Write-Output "The following methods have insufficient code coverage";
    $failures | ForEach-Object {
        $_ | Select-Object -Property name,sequenceCoverage,branchCoverage
    } | Format-List
    throw "Insufficient code coverage";
}
