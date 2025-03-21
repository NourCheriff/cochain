// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC721/ERC721.sol";

contract Activity is ERC721 {
    uint256 private _tokenIdCounter;

    struct Document {
        uint256 timestamp;
        bytes32 documentHash;
    }

    struct ActivityStruct {
        uint256 timestamp;
        string activityId;
        address scp;
        uint256 emissions;
    }

    struct Product {
        string productId;
        uint256 creationDate;
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
        newProduct.creationDate = block.timestamp;
        newProduct.expirationDate = expirationDate;
        newProduct.scp = msg.sender;

        // Conia il token NFT
        _mint(msg.sender, newTokenId);
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
        require(_exists(tokenId), "Product does not exist");

        // Crea una nuova attività
        ActivityStruct memory newActivity = ActivityStruct({
            timestamp: block.timestamp,
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
        require(_exists(tokenId), "Product does not exist");

        // Crea un nuova documento
        Document memory newDocument = Document({
            timestamp: block.timestamp,
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
            uint256[] memory timestamps,
            string[] memory activityId,
            address[] memory scps,
            uint256[] memory emissions
        )
    {
        require(_exists(tokenId), "Product does not exist");

        uint256 length = products[tokenId].activity.length;
        timestamps = new uint256[](length);
        activityId = new string[](length);
        scps = new address[](length);
        emissions = new uint256[](length);

        for (uint256 i = 0; i < length; i++) {
            ActivityStruct memory act = products[tokenId].activity[i];
            timestamps[i] = act.timestamp;
            activityId[i] = act.activityId;
            scps[i] = act.scp;
            emissions[i] = act.emissions;
        }

        return (timestamps, activityId, scps, emissions);
    }

    function getActivity(
        uint256 tokenId,
        string memory activityId
    )
        public
        view
        returns (
            uint256 timestamp,
            string memory id,
            address scp,
            uint256 emissions
        )
    {
        require(_exists(tokenId), "Product does not exist");

        ActivityStruct[] storage activities = products[tokenId].activity;
        bool found = false;

        for (uint256 i = 0; i < activities.length; i++) {
            if (
                keccak256(bytes(activities[i].activityId)) ==
                keccak256(bytes(activityId))
            ) {
                timestamp = activities[i].timestamp;
                id = activities[i].activityId;
                scp = activities[i].scp;
                emissions = activities[i].emissions;
                found = true;
                break;
            }
        }

        require(found, "Activity not found");
        return (timestamp, id, scp, emissions);
    }

    function getDocument(
        uint256 tokenId,
        bytes32 documentHashToFind
    ) public view returns (uint256 timestamp, bytes32 documentHash) {
        require(_exists(tokenId), "Product does not exist");
        Document[] storage documents = products[tokenId].document;
        bool found = false;

        for (uint256 i = 0; i < documents.length; i++) {
            if (documents[i].documentHash == documentHashToFind) {
                timestamp = documents[i].timestamp;
                documentHash = documents[i].documentHash;
                found = true;
                break;
            }
        }

        require(found, "Document not found");
        return (timestamp, documentHash);
    }

    function getDocuments(
        uint256 tokenId
    )
        public
        view
        returns (uint256[] memory timestamps, bytes32[] memory documentHashes)
    {
        require(_exists(tokenId), "Product does not exist");

        uint256 length = products[tokenId].document.length;
        timestamps = new uint256[](length);
        documentHashes = new bytes32[](length);

        for (uint256 i = 0; i < length; i++) {
            Document memory doc = products[tokenId].document[i];
            timestamps[i] = doc.timestamp;
            documentHashes[i] = doc.documentHash;
        }

        return (timestamps, documentHashes);
    }

    function removeDocument(
        uint256 tokenId,
        bytes32 documentHashToRemove
    ) public {
        require(_exists(tokenId), "Product does not exist");
        require(
            ownerOf(tokenId) == msg.sender,
            "Only the owner can remove documents"
        );

        Document[] storage documents = products[tokenId].document;
        uint256 documentsLength = documents.length;
        bool found = false;

        for (uint256 i = 0; i < documentsLength; i++) {
            if (documents[i].documentHash == documentHashToRemove) {
                found = true;

                documents[i] = documents[documentsLength - 1];
                documents.pop();

                emit DocumentRemoved(tokenId, documentHashToRemove, msg.sender);
                break;
            }
        }

        require(found, "Document not found");
    }

    function getEmissionsBySCP(address scp) public view returns (uint256) {
        return totalEmissionBalances[scp];
    }
}
