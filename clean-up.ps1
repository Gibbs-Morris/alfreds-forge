#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Runs code-style cleanup for the Alfred's Forge solution.
#>

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$cleanupScript = Join-Path $repoRoot 'eng\src\agent-scripts\clean-up-alfreds-forge-solution.ps1'

try {
    Write-Host '=== CLEANUP: ALFREDS-FORGE SOLUTION ===' -ForegroundColor Yellow
    & $cleanupScript
    if ($LASTEXITCODE -ne 0) {
        throw "Cleanup failed with exit code $LASTEXITCODE"
    }

    Write-Host '=== CLEANUP COMPLETED SUCCESSFULLY ===' -ForegroundColor Green
}
catch {
    Write-Error "=== CLEANUP FAILED === $_"
    exit 1
}
