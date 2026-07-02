#!/usr/bin/env pwsh

[CmdletBinding()]
param(
    [string]$Configuration = 'Release',
    [switch]$SkipCleanup
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$cleanUpScript = Join-Path $PSScriptRoot 'clean-up-alfreds-forge-solution.ps1'
$buildScript = Join-Path $PSScriptRoot 'build-alfreds-forge-solution.ps1'
$unitTestScript = Join-Path $PSScriptRoot 'unit-test-alfreds-forge-solution.ps1'
$finalBuildScript = Join-Path $PSScriptRoot 'final-build-solutions.ps1'

if (-not $SkipCleanup) {
    & $cleanUpScript
    if ($LASTEXITCODE -ne 0) {
        throw "Cleanup failed with exit code $LASTEXITCODE"
    }
}

& $buildScript -Configuration $Configuration
if ($LASTEXITCODE -ne 0) {
    throw "Build failed with exit code $LASTEXITCODE"
}

& $unitTestScript -Configuration $Configuration
if ($LASTEXITCODE -ne 0) {
    throw "Unit tests failed with exit code $LASTEXITCODE"
}

& $finalBuildScript -Configuration $Configuration
if ($LASTEXITCODE -ne 0) {
    throw "Final build failed with exit code $LASTEXITCODE"
}
