#!/bin/bash

usage()
{
    echo "usage: install.sh [-n|d|r]"
    echo "  -n | --new          deploy a new blockchain"
    # echo "  -r | --run          run the deployed blockchain"
    echo "  -d | --deploy       deploy smart contracts to the blockchain"
    echo "  -h | --help         show this message and exit"
}

echo ""
echo "=============================================="
echo '
                       __          _     
      _________  _____/ /_  ____ _(_)___ 
     / ___/ __ \/ ___/ __ \/ __ `/ / __ \
    / /__/ /_/ / /__/ / / / /_/ / / / / /
    \___/\____/\___/_/ /_/\__,_/_/_/ /_/ 


    Welcome to cochain setup script!
'
echo "=============================================="
echo ""

new=0
deploy=0
# run=0

if [ "$1" == "" ]; then
    usage
    exit 1
fi

while [ "$1" != "" ]; do
    case $1 in
        -n | --new)
            new=1
            ;;
        -d | --deploy)
            deploy=1
            ;;
        # -r | --run)
        #    run=1
        #    ;;
        -h | --help)
            usage
            exit
            ;;
        *)
            usage
            exit 1
            ;;
    esac
    shift
done

if [[ $new == 1 ]]; then
    chmod +x $PWD/scripts/setup_blockchain.sh
    $PWD/scripts/setup_blockchain.sh
fi

: '
if [[ $run == 1 ]]; then
    cd $PWD/onchain/
    docker compose up
    echo "Containers are up!"
    cd $PWD
fi
'

if [[ $deploy == 1 ]]; then
    if [[ -z "$(docker ps -f "name=validator1" -f "status=running" -q )" ]]; then
        echo "Unable to deploy transactions. Make sure that the blockchain is up"
        exit 1
    fi
    chmod +x $PWD/scripts/deploy_contracts.sh
    $PWD/scripts/deploy_contracts.sh
fi