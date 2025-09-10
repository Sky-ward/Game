#!/usr/bin/env bash
set -euo pipefail
root="$(git rev-parse --show-toplevel)"
cd "$root"

man="assets_b64/manifest.json"
if [ ! -f "$man" ]; then
  echo "manifest not found: $man"; exit 0
fi

# 空文件或空数组直接跳过
test -s "$man" || { echo "empty manifest"; exit 0; }

# 需要 jq、sha256sum（macOS 用 shasum -a 256）
platform_sha256sum() {
  if command -v sha256sum >/dev/null 2>&1; then
    sha256sum "$1" | awk '{print $1}'
  else
    shasum -a 256 "$1" | awk '{print $1}'
  fi
}

# 遍历 JSON 数组
len=$(jq length "$man")
if [ "$len" -eq 0 ]; then
  echo "manifest is empty"; exit 0
fi

for i in $(seq 0 $((len - 1))); do
  path=$(jq -r ".[$i].path" "$man")
  b64="assets_b64/${path}.b64"
  expect_hash=$(jq -r ".[$i].sha256" "$man")

  if [ ! -f "$b64" ]; then
    echo "missing: $b64" >&2; exit 1
  fi
  mkdir -p "$(dirname "$path")"
  base64 -d "$b64" > "$path"

  actual_hash=$(platform_sha256sum "$path")
  if [ "$actual_hash" != "$expect_hash" ]; then
    echo "sha256 mismatch for $path" >&2
    echo " expected: $expect_hash" >&2
    echo "   actual: $actual_hash" >&2
    exit 1
  fi
  echo "decoded: $path"

done
