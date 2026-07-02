#!/usr/bin/env pwsh

[CmdletBinding()]
param(
    [string]$Configuration = 'Release'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot '..\..\..')
$solutionPath = Join-Path $repoRoot 'alfreds-forge.slnx'

dotnet build $solutionPath --configuration $Configuration --no-incremental -warnaserror
if ($LASTEXITCODE -ne 0) {
    throw "dotnet build failed with exit code $LASTEXITCODE"
}
