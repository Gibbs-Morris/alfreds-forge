#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Performs a fast, strict build by invoking final-build-solutions.ps1.
#>

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
$finalBuildScript = Join-Path $scriptDir 'eng\src\agent-scripts\final-build-solutions.ps1'

Write-Host '=== QUICK BUILD MODE ===' -ForegroundColor Yellow
Write-Host 'Fast build with warnings as errors (bypasses tests and cleanup)'
Write-Host ''

try {
    & $finalBuildScript @args
    if ($LASTEXITCODE -ne 0) {
        throw "final-build-solutions.ps1 failed with exit code $LASTEXITCODE"
    }

    Write-Host '=== QUICK BUILD COMPLETED SUCCESSFULLY ===' -ForegroundColor Green
}
catch {
    Write-Error "=== QUICK BUILD FAILED === $_"
    exit 1
}
