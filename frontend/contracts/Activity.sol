// SPDX-License-Identifier: MIT
pragma solidity ^0.8.24;

import {ERC721} from "@openzeppelin/contracts/token/ERC721";

contract Activity is ERC721 {
    uint256 private _tokenIdCounter;

    struct Document {
        uint256 blockNumber;
        bytes32 documentHash;
    }

    struct ActivityStruct {
        uint256 blockNumber;
        string activityId;
        address scp;
        uint256 emissions;
    }

    struct Product {
        string productId;
        uint256 blockNumber;
        uint256 expirationDate;
        address scp;
        Document[] document;
        ActivityStruct[] activity;
    }

    // Mappatura da tokenId a Product
    mapping(uint256 => Product) public products;
    // Mappatura delle emissioni totali per attività
    mapping(address => uint256) public totalEmissionBalances;

    event ProductCreated(uint256 tokenId, string productId, address scp);
    event ActivityAdded(uint256 tokenId, string activityId, address scp);
    event DocumentAdded(uint256 tokenId, bytes32 documentHash, address scp);
    event DocumentRemoved(uint256 tokenId, bytes32 documentHash, address scp);

    error ProductNotFound();
    error DocumentNotFound();
    error ActivityNotFound();

    constructor() ERC721("Activity", "ACTY") {}

    function _exists(uint256 tokenId) internal view returns (bool) {
        return ownerOf(tokenId) != address(0);
    }

    function createProduct(
        string memory productId,
        uint256 expirationDate
    ) public returns (uint256) {
        uint256 newTokenId = _tokenIdCounter;

        // Crea un nuovo prodotto
        Product storage newProduct = products[newTokenId];
        newProduct.productId = productId;
        newProduct.blockNumber = block.number;
        newProduct.expirationDate = expirationDate;
        newProduct.scp = msg.sender;

        // Conia il token NFT
        _safeMint(msg.sender, newTokenId);
        unchecked {
            _tokenIdCounter++;
        }

        emit ProductCreated(newTokenId, productId, msg.sender);

        return newTokenId;
    }

    function addActivity(
        uint256 tokenId,
        string memory activityId,
        uint256 emissions
    ) public {
        if (!_exists(tokenId)) {
            revert ProductNotFound();
        }

        // Crea una nuova attività
        ActivityStruct memory newActivity = ActivityStruct({
            blockNumber: block.number,
            activityId: activityId,
            scp: msg.sender,
            emissions: emissions
        });

        // Aggiungi l'attività al ciclo di vita del prodotto
        products[tokenId].activity.push(newActivity);
        totalEmissionBalances[msg.sender] += emissions;

        emit ActivityAdded(tokenId, activityId, msg.sender);
    }

    function addDocument(uint256 tokenId, bytes32 documentHash) public {
        if (!_exists(tokenId)) {
            revert ProductNotFound();
        }

        // Crea un nuova documento
        Document memory newDocument = Document({
            blockNumber: block.number,
            documentHash: documentHash
        });

        // Aggiungi il documento al prodotto
        products[tokenId].document.push(newDocument);

        emit DocumentAdded(tokenId, documentHash, msg.sender);
    }

    function getActivities(
        uint256 tokenId
    )
        public
        view
        returns (
            uint256[] memory blockNumber,
            string[] memory activityId,
            address[] memory scps,
            uint256[] memory emissions
        )
    {
        if (!_exists(tokenId)) {
            revert ProductNotFound();
        }

        uint256 length = products[tokenId].activity.length;
        blockNumber = new uint256[](length);
        activityId = new string[](length);
        scps = new address[](length);
        emissions = new uint256[](length);

        for (uint256 i = 0; i < length; i++) {
            ActivityStruct memory act = products[tokenId].activity[i];
            blockNumber[i] = act.blockNumber;
            activityId[i] = act.activityId;
            scps[i] = act.scp;
            emissions[i] = act.emissions;
        }

        return (blockNumber, activityId, scps, emissions);
    }

    function getActivity(
        uint256 tokenId,
        string memory activityId
    )
        public
        view
        returns (
            uint256 blockNumber,
            string memory id,
            address scp,
            uint256 emissions
        )
    {
        if (!_exists(tokenId)) {
            revert ProductNotFound();
        }

        ActivityStruct[] storage activities = products[tokenId].activity;
        bool found = false;

        for (uint256 i = 0; i < activities.length; i++) {
            if (
                keccak256(bytes(activities[i].activityId)) ==
                keccak256(bytes(activityId))
            ) {
                blockNumber = activities[i].blockNumber;
                id = activities[i].activityId;
                scp = activities[i].scp;
                emissions = activities[i].emissions;
                found = true;
                break;
            }
        }

        if (!found) {
            revert ActivityNotFound();
        }

        return (blockNumber, id, scp, emissions);
    }

    function getDocument(
        uint256 tokenId,
        bytes32 documentHashToFind
    ) public view returns (uint256 blockNumber, bytes32 documentHash) {
        if (!_exists(tokenId)) {
            revert ProductNotFound();
        }

        Document[] storage documents = products[tokenId].document;
        bool found = false;

        for (uint256 i = 0; i < documents.length; i++) {
            if (documents[i].documentHash == documentHashToFind) {
                blockNumber = documents[i].blockNumber;
                documentHash = documents[i].documentHash;
                found = true;
                break;
            }
        }

        if (!found) {
            revert DocumentNotFound();
        }

        return (blockNumber, documentHash);
    }

    function getDocuments(
        uint256 tokenId
    )
        public
        view
        returns (uint256[] memory blockNumber, bytes32[] memory documentHashes)
    {
        if (!_exists(tokenId)) {
            revert ProductNotFound();
        }

        uint256 length = products[tokenId].document.length;
        blockNumber = new uint256[](length);
        documentHashes = new bytes32[](length);

        for (uint256 i = 0; i < length; i++) {
            Document memory doc = products[tokenId].document[i];
            blockNumber[i] = doc.blockNumber;
            documentHashes[i] = doc.documentHash;
        }

        return (blockNumber, documentHashes);
    }

    function getEmissionsBySCP(address scp) public view returns (uint256) {
        return totalEmissionBalances[scp];
    }
}
