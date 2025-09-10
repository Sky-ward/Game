Param(
  [string]$ManifestPath = "assets_b64/manifest.json"
)

if (-not (Test-Path $ManifestPath)) { Write-Host "manifest not found"; exit 0 }
$json = Get-Content $ManifestPath -Raw | ConvertFrom-Json
if ($null -eq $json -or $json.Count -eq 0) { Write-Host "manifest empty"; exit 0 }

function Get-FileSha256([string]$FilePath) {
  $hasher = [System.Security.Cryptography.SHA256]::Create()
  $stream = [System.IO.File]::OpenRead($FilePath)
  try {
    $hashBytes = $hasher.ComputeHash($stream)
    ($hashBytes | ForEach-Object { $_.ToString("x2") }) -join ''
  } finally { $stream.Dispose(); $hasher.Dispose() }
}

foreach ($item in $json) {
  $path = $item.path
  $b64 = Join-Path "assets_b64" ($path + ".b64")
  if (-not (Test-Path $b64)) { throw "missing: $b64" }
  $dir = Split-Path $path -Parent
  if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }

  $bytes = [Convert]::FromBase64String((Get-Content $b64 -Raw))
  [IO.File]::WriteAllBytes($path, $bytes)

  $actual = Get-FileSha256 $path
  if ($actual -ne $item.sha256) { throw "sha256 mismatch for $path`n expected: $($item.sha256)`n actual:   $actual" }
  Write-Host "decoded: $path"
}
