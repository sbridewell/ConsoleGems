$coverageFilenames = Get-ChildItem -Path "..\..\ConsoleGems\ConsoleGems\TestResults" -Recurse -Filter "*.xml";
$coverageFilenames | Format-List -Verbose;
$coverageFilename = $coverageFilenames[0].FullName;
$coverageFileContent = Get-Content $coverageFilename;
$coverageXml = [xml]$coverageFileContent;
$methods = $coverageXml.GetElementsByTagName("Method");
$methods | Format-List -Verbose -Property name,sequenceCoverage,branchCoverage
$failures = $methods | Where-Object {[int]$_.sequenceCoverage -lt 80 -or [int]$_.branchCoverage -lt 80};
if ($failures.Count -gt 0) {
    Write-Output "The following methods have insufficient code coverage";
    $failures | ForEach-Object {
        $_ | Select-Object -Property name,sequenceCoverage,branchCoverage
    } | Format-List
    throw "Insufficient code coverage";
}
