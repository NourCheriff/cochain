#!/bin/bash

BESU_VERSION=25.2.1

echo ""
echo "==============================================="
echo "Setup blockchain..."
echo "==============================================="
echo ""

cd onchain

echo ""
echo "==============================================="
echo "Cleaning directories..."
echo "==============================================="
echo ""

if docker container inspect besu &>/dev/null; then
	docker container stop besu &>/dev/null && docker container rm besu &>/dev/null
fi

rm -rf $PWD/data &>/dev/null

echo ""
echo "==============================================="
echo "Running the besu container..."
echo "==============================================="
echo ""

docker run -d \
	--name besu \
	hyperledger/besu:$BESU_VERSION

docker container cp ./setup.sh besu:/tmp/

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

docker container cp besu:/tmp/output ./data


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

export $(cat $PWD/data/.env)

sed -i 's/"extraData":.*,/"extraData": "'"$EXTRA_DATA"'",/' ./genesis.json

# compile and deploy smart contracts to the chain
# GO GO GO GO!

echo ""
echo "==============================================="
echo "Setup done!"
echo "==============================================="
echo ""
