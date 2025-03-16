$ErrorActionPreference = 'Stop';
$coverageFilenames = Get-ChildItem -Path "..\..\ConsoleGems\ConsoleGems\TestResults" -Recurse -Filter "*.xml";
Write-Verbose ($coverageFilenames | Format-List | Out-String);
$coverageFilename = $coverageFilenames[0].FullName;
$coverageFileContent = Get-Content $coverageFilename;
$coverageXml = [xml]$coverageFileContent;
$methods = $coverageXml.GetElementsByTagName("Method");
Write-Output "Found $methods.Count methods";
Write-Verbose ($methods | Format-List -Property name,sequenceCoverage,branchCoverage | Out-String);
$failures = $methods | Where-Object {[int]$_.sequenceCoverage -lt 80 -or [int]$_.branchCoverage -lt 80};
if ($failures.Count -gt 0) {
    Write-Output "The following methods have insufficient code coverage";
    $failures | ForEach-Object {
        $_ | Select-Object -Property name,sequenceCoverage,branchCoverage
    } | Format-List
    throw "Insufficient code coverage";
} else {
    Write-Output "Congratulations, your code coverage looks good :-)";
}
