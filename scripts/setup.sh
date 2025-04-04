#!/bin/bash

VALIDATORS=4
NODES=2
OUTPUT_DIR=/tmp/output

mkdir -p $OUTPUT_DIR && cd $OUTPUT_DIR

echo ""
echo "Creating validators..."
echo ""

for i in $(seq 1 $VALIDATORS); do	
	mkdir ./validator$i
	mkdir -p ./keys/validator$i
done

echo ""
echo "Generating keys..."
echo ""

for i in $(seq 1 $VALIDATORS); do
	besu --data-path=./validator$i \
		public-key \
		export-address \
		--to=./keys/validator$i/address.pub
done

echo ""
echo "Copying private keys..."
echo ""

for i in $(seq 1 $VALIDATORS); do
	cp ./validator$i/key ./keys/validator$i/key
done

echo ""
echo "Exporting node ids (public keys)..."
echo ""

for i in $(seq 1 $VALIDATORS); do
	besu --data-path=./validator$i \
		public-key export \
		--node-private-key-file=./keys/validator$i/key \
		--to=./keys/validator$i/key.pub \
		--ec-curve=secp256k1
done

touch toEncode.json

echo "[" >> toEncode.json

for i in $(seq 1 $VALIDATORS); do
	echo -n "\"$(cat ./keys/validator$i/address.pub)\"" >> toEncode.json
	if [[ $i -ne $VALIDATORS ]]; then
		echo "," >> toEncode.json
	fi
done

echo "" >> toEncode.json
echo "]" >> toEncode.json

echo EXTRA_DATA=$(besu rlp encode --from=toEncode.json) > .env

rm toEncode.json

echo ""
echo "Creating nodes..."
echo ""

for i in $(seq 1 $NODES); do
	mkdir -p ./node$i
done

echo ""
echo "Script Completed!..."
echo ""
