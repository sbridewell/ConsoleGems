$coverageFilenames = Get-ChildItem -Path "..\..\ConsoleGems\ConsoleGems\TestResults" -Recurse -Filter "*.xml";
$coverageFilenames | Format-List
$coverageFileContent = [xml](Get-Content $coverageFilenames[0].FullName);
$coverageFileContent
$methods = $coverageFileContent.GetElementsByTagName("Method");
$methods | Format-List
$failures = $methods | Where-Object {[int]$_.sequenceCoverage -lt 80 -or [int]$_.branchCoverage -lt 80};
if ($failures.Count -gt 0) {
    Write-Output "The following methods have insufficient code coverage";
    $failures | ForEach-Object {
        $_ | Select-Object -Property name,sequenceCoverage,branchCoverage
    } | Format-List
    throw "Insufficient code coverage";
}
