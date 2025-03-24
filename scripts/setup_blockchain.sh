#!/bin/bash

BESU_VERSION=25.2.1

echo ""
echo "==============================================="
echo "Setup blockchain..."
echo "==============================================="
echo ""

echo ""
echo "==============================================="
echo "Cleaning directories..."
echo "==============================================="
echo ""

if docker container inspect besu &>/dev/null; then
	docker container stop besu &>/dev/null && docker container rm besu &>/dev/null
fi

rm -rf $PWD/onchain/data &>/dev/null

echo ""
echo "==============================================="
echo "Running the besu container..."
echo "==============================================="
echo ""

docker run -d \
	--name besu \
	hyperledger/besu:$BESU_VERSION

docker container cp $PWD/scripts/setup.sh besu:/tmp/

docker exec besu sh -c "chmod +x /tmp/setup.sh"

echo ""
echo "==============================================="
echo "Launching setup script on the container..."
echo "==============================================="
echo ""

docker exec besu /tmp/setup.sh

echo ""
echo "==============================================="
echo "Copying output files to host..."
echo "==============================================="
echo ""

docker container cp besu:/tmp/output $PWD/onchain/data

echo ""
echo "==============================================="
echo "Removing container..."
echo "==============================================="
echo ""

docker container stop besu &>/dev/null && docker container rm besu &>/dev/null

echo ""
echo "==============================================="
echo "Setting up genesis.json..."
echo "==============================================="
echo ""

EXTRA_DATA=$(cat $PWD/onchain/data/.env | cut -d'=' -f2)
sed -i '' 's/"extraData":.*,/"extraData": "'"$EXTRA_DATA"'",/' $PWD/onchain/genesis.json

echo ""
echo "==============================================="
echo "Setting up docker-compose.yml..."
echo "==============================================="
echo ""

BOOTNODE_ADDRESS=$(cat $PWD/onchain/data/keys/validator1/key.pub | cut -c 3-)
sed -i '' 's/"--bootnodes=enode:\/\/.*@/"--bootnodes=enode:\/\/'"$BOOTNODE_ADDRESS"'@/g' $PWD/onchain/docker-compose.yml

echo ""
echo "==============================================="
echo "Setup done!"
echo "==============================================="
echo ""
