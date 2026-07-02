#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Runs the orchestrate-solutions.ps1 script for this repository.
#>

param(
    [switch]$SkipCleanup,
    [string]$Configuration = 'Release'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$orchestrateScript = Join-Path $repoRoot 'eng\src\agent-scripts\orchestrate-solutions.ps1'

Write-Host '=== STARTING MAIN PIPELINE ORCHESTRATION ===' -ForegroundColor Yellow

try {
    & $orchestrateScript -Configuration $Configuration -SkipCleanup:$SkipCleanup
    if ($LASTEXITCODE -ne 0) {
        throw "orchestrate-solutions.ps1 failed with exit code $LASTEXITCODE"
    }

    Write-Host '=== SUCCESS: Main pipeline orchestration completed successfully ===' -ForegroundColor Green
}
catch {
    Write-Error "=== FAILURE: Main pipeline orchestration failed: $_"
    exit 1
}
