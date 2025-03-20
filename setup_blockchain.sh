#!/bin/bash

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

rm -rf ./onchain/data &>/dev/null

echo ""
echo "==============================================="
echo "Running the besu container..."
echo "==============================================="
echo ""

docker run -d \
	--name besu \
	hyperledger/besu:25.2.1	

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
echo "Setup done!"
echo "==============================================="
echo ""
