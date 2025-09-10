Param([Parameter(Mandatory)] [string[]]$Files)

$man = "assets_b64/manifest.json"
if (-not (Test-Path $man)) { Set-Content -Path $man -Value "[]" -NoNewline }
$json = Get-Content $man -Raw | ConvertFrom-Json

function Get-FileSha256([string]$FilePath) {
  $hasher = [System.Security.Cryptography.SHA256]::Create()
  $stream = [System.IO.File]::OpenRead($FilePath)
  try {
    $hashBytes = $hasher.ComputeHash($stream)
    ($hashBytes | ForEach-Object { $_.ToString("x2") }) -join ''
  } finally { $stream.Dispose(); $hasher.Dispose() }
}

foreach ($f in $Files) {
  $rel = $f
  $out = Join-Path "assets_b64" ($rel + ".b64")
  $dir = Split-Path $out -Parent
  if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
  $bytes = [IO.File]::ReadAllBytes($rel)
  $b64   = [Convert]::ToBase64String($bytes)
  Set-Content -Path $out -Value $b64 -NoNewline
  $hash = Get-FileSha256($rel)
  $json += @{ path = $rel; sha256 = $hash }
  Write-Host "encoded: $rel → $out"
}

$json | ConvertTo-Json -Depth 10 | Set-Content -Path $man -NoNewline
