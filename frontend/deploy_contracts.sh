#!/bin/bash

echo ""
echo "==============================================="
echo "Clearing the cache and deleting the artifacts..."
echo "==============================================="
echo ""

npx -y hardhat clean
rm -rf ./cache

echo ""
echo "==============================================="
echo "Deploying smart contracts to local network..."
echo "==============================================="
echo ""

npx hardhat deploy --network docker
