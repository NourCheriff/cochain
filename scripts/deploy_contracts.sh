#!/bin/bash

echo ""
echo "==============================================="
echo "Clearing the cache and deleting the artifacts..."
echo "==============================================="
echo ""

cd $PWD/frontend
npx -y hardhat clean
rm -rf ./artifacts
rm -rf ./cache
rm -rf ./deployments

echo ""
echo "==============================================="
echo "Deploying smart contracts to local network..."
echo "==============================================="
echo ""

npx hardhat deploy --network docker

echo ""
echo "==============================================="
echo "Contracts deployed successfully!"
echo "==============================================="
echo ""
