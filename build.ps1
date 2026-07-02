#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Builds the Alfred's Forge solution.
#>

[CmdletBinding()]
param(
    [string]$Configuration = 'Release'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$buildScript = Join-Path $repoRoot 'eng\src\agent-scripts\build-alfreds-forge-solution.ps1'

try {
    Write-Host '=== STEP 1: BUILD ALFREDS-FORGE SOLUTION ===' -ForegroundColor Yellow
    & $buildScript -Configuration $Configuration
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed with exit code $LASTEXITCODE"
    }

    Write-Host '=== BUILD COMPLETED SUCCESSFULLY ===' -ForegroundColor Green
}
catch {
    Write-Error "=== BUILD FAILED === $_"
    exit 1
}
