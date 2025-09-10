#!/usr/bin/env bash
set -euo pipefail

if [ "$#" -lt 1 ]; then
  echo "Usage: $0 <file1> [file2 ...]"; exit 1
fi

platform_sha256sum() {
  if command -v sha256sum >/dev/null 2>&1; then
    sha256sum "$1" | awk '{print $1}'
  else
    shasum -a 256 "$1" | awk '{print $1}'
  fi
}

mkdir -p assets_b64
man=assets_b64/manifest.json
[ -f "$man" ] || echo "[]" > "$man"

for f in "$@"; do
  rel="$f" # 已在仓库根下
  out="assets_b64/${rel}.b64"
  mkdir -p "$(dirname "$out")"
  base64 -w0 "$rel" > "$out"
  hash=$(platform_sha256sum "$rel")
  tmp=$(mktemp)
  jq ". + [{path:\"$rel\", sha256:\"$hash\"}]" "$man" > "$tmp" && mv "$tmp" "$man"
  echo "encoded: $rel → $out"

done
