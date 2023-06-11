#!/bin/bash

# Build the project for Mac Intel
cargo build --release --target=x86_64-apple-darwin

# Build the project for Mac Apple Silicon
cargo build --release --target=aarch64-apple-darwin

# Check if the Plugins folder exists and create it if it doesn't
if [ ! -d "../../Assets/Plugins/x86_64" ]; then
    mkdir ../../Assets/Plugins/x86_64
fi
if [ ! -d "../../Assets/Plugins/arm64" ]; then
    mkdir ../../Assets/Plugins/arm64
fi

# Copy files to the Plugins folder
cp target/x86_64-apple-darwin/release/libfile_browser.dylib ../../Assets/Plugins/x86_64/libfile_browser.dylib
cp target/aarch64-apple-darwin/release/libfile_browser.dylib ../../Assets/Plugins/arm64/libfile_browser.dylib