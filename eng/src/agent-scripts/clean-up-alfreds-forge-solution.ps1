#!/usr/bin/env pwsh

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot '..\..\..')
$solutionPath = Join-Path $repoRoot 'alfreds-forge.slnx'

dotnet tool restore
if ($LASTEXITCODE -ne 0) {
    throw "dotnet tool restore failed with exit code $LASTEXITCODE"
}

dotnet tool run jb cleanupcode $solutionPath --verbosity=WARN --no-build
if ($LASTEXITCODE -ne 0) {
    throw "cleanupcode failed with exit code $LASTEXITCODE"
}
