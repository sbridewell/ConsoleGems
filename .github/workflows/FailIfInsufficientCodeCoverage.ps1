param (
    [Parameter(Mandatory = $false)][string]$ModuleUnderTest
)

Write-Verbose "You are running in verbose mode";
Write-Verbose "ModuleUnderTest: $ModuleUnderTest";
$ErrorActionPreference = 'Stop';
$coverageFilenames = Get-ChildItem -Recurse -Filter "coverage.opencover.xml";
Write-Output "The following coverage files were found:";
Write-Output ($coverageFilenames | Format-Table | Out-String);

$coverageFilename = ($coverageFilenames | Select-Object -First 1).FullName;
Write-Verbose "Coverage filename: $coverageFilename";
$coverageFileContent = Get-Content $coverageFilename;

# Write-Verbose "Coverage file content:";
# Write-Verbose ($coverageFileContent | Out-String);

Write-Verbose "Cast content of $coverageFilename file to XML";
$coverageXml = [xml]$coverageFileContent;

Write-Verbose "Coverage XML content:";
Write-Verbose ($coverageXml | Out-String);

Write-Verbose "Get modules from coverage XML";
$modules = $coverageXml.GetElementsByTagName("Module");
Write-Verbose ($modules | Out-String);

Write-Verbose "Get module where ModuleName is $ModuleUnderTest";
$modules | ForEach-Object { Write-Verbose "ModuleName: $($_.ModuleName)" }
$moduleResult = $modules | Where-Object { $_.ModuleName -and $_.ModuleName -eq $ModuleUnderTest }
if ($null -eq $moduleResult) {
    throw "No module found called '$ModuleUnderTest'";
}

Write-Verbose "Get Method elements from module";
$methods = $moduleResult.GetElementsByTagName("Method");
# Write-Verbose $methods; # TODO: write something more meaningful than "METHOD METHOD METHOD"

$methodCount = ($methods | Measure-Object).Count;
Write-Verbose "Found $methodCount methods";
if ($methodCount -eq 0) {
    throw "No methods found in the coverage file $coverageFilename";
}

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
