Write-Verbose "You are running in verbose mode";
$ErrorActionPreference = 'Stop';
$coverageFilenames = Get-ChildItem -Recurse -Filter "coverage.opencover.xml";
Write-Verbose ($coverageFilenames | Format-List | Out-String);
$coverageFilename = $coverageFilenames[0].FullName;
$coverageFileContent = Get-Content $coverageFilename;
$coverageXml = [xml]$coverageFileContent;
$nonTestAssemblies = @($coverageXml.GetElementsByTagName("Module") | Where-Object {$_.ModulePath -notlike "*.Test.dll"})
$methods = $nonTestAssemblies.GetElementsByTagName("Method");
$methodCount = ($methods | Measure-Object).Count;
Write-Verbose "Found $methodCount methods";
Write-Verbose ($methods | Format-List -Property name,sequenceCoverage,branchCoverage | Out-String);

# Explicitly wrap the result in an array, otherwise if there's only one object found then it's returned
# as a XmlElement rather than as an array with a single element.
$failures = @($methods | Where-Object {[int]$_.sequenceCoverage -lt 80 -or [int]$_.branchCoverage -lt 80});
if ($failures.Count -gt 0) {
    Write-Output "The following methods have insufficient code coverage";
    $failures | Format-Table sequenceCoverage, branchCoverage, name
    $failureCount = $failures.Count;
    throw "Insufficient code coverage - $failureCount failures.";
} else {
    Write-Output "Congratulations, your code coverage looks good :-)";
}
