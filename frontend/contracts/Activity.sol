// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/utils/Counters.sol";

contract Activity is ERC721 {
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIds;

    struct Document {
        uint256 timestamp;
        bytes32 documentHash;
        string documentType;
    }

    struct Activity {
        uint256 timestamp;
        string categoryId;
        address scp;
        uint256 emissions;
    }

    struct Product {
        string productId;
        uint256 creationDate;
        uint256 expirationDate;
        address scp;
        Document[] document;
        Activity[] lifecycle;
    }

    // Mappatura da tokenId a Product
    mapping(uint256 => Product) public products;
    // Mappatura delle emissioni totali per attività
    mapping(address => uint256) public totalEmissionBalances;

    event ProductCreated(uint256 tokenId, string productId, address scp);
    event ActivityAdded(uint256 tokenId, string activityType, address scp);
    event DocumentAdded(uint256 tokenId, string documentType);
    event SupplyChainPartnerAuthorized(address scp);
    event SupplyChainPartnerRevoked(address scp);

    constructor() ERC721("CarbonCredits", "CC") {}

    function createProduct(
        address scp,
        string memory productId,
        uint256 expirationDate
    ) public returns (uint256) {
        _tokenIds.increment();
        uint256 newTokenId = _tokenIds.current();

        // Crea un nuovo prodotto
        Product storage newProduct = products[newTokenId];
        newProduct.productId = productId;
        newProduct.creationDate = block.timestamp;
        newProduct.expirationDate = expirationDate;
        newProduct.scp = msg.sender;

        // Conia il token NFT
        _mint(scp, newTokenId);

        emit ProductCreated(newTokenId, productId, msg.sender);

        return newTokenId;
    }

    function addActivity(
        uint256 tokenId,
        string categoryId,
        address scp,
        uint256 emissions
    ) public {
        require(_exists(tokenId), "Product does not exist");

        // Crea una nuova attività
        Activity memory newActivity = Activity({
            timestamp: block.timestamp,
            categoryId: categoryId,
            scp: scp, //msg.sender se chi chiama il metodo è scp che effettua attività
            emissions: emissions
        });

        // Aggiungi l'attività al ciclo di vita del prodotto
        products[tokenId].lifecycle.push(newActivity);
        totalEmissionBalances[msg.sender] += emissions;

        emit ActivityAdded(tokenId, categoryId, scp);
    }

    function addDocument(
        uint256 tokenId,
        bytes32 documentHash,
        string documentType
    ) public {
        require(_exists(tokenId), "Product does not exist");

        // Crea un nuova documento
        Document memory newDocument = Document({
            timestamp: block.timestamp,
            documentType: documentType,
            documentHash: documentHash
        });

        // Aggiungi il documento al prodotto
        products[tokenId].document.push(newDocument);

        emit DocumentAdded(tokenId, documentType);
    }

    function getActivitiesByTokenId(
        uint256 tokenId
    )
        public
        view
        returns (
            uint256[] memory timestamps,
            string[] memory categoryIds,
            address[] memory scps,
            uint256[] memory emissions
        )
    {
        require(_exists(tokenId), "Product does not exist");

        uint256 length = products[tokenId].lifecycle.length;
        timestamps = new uint256[](length);
        categoryIds = new string[](length);
        scps = new address[](length);
        emissions = new uint256[](length);

        for (uint256 i = 0; i < length; i++) {
            Activity storage act = products[tokenId].lifecycle[i];
            timestamps[i] = act.timestamp;
            categoryIds[i] = act.categoryId;
            scps[i] = act.scp;
            emissions[i] = act.emissions;
        }

        return (timestamps, categoryIds, scps, emissions);
    }

    function getDocumentsByTokenId(
        uint256 tokenId
    )
        public
        view
        returns (
            uint256[] memory timestamps,
            bytes32[] memory documentHashes,
            string[] memory documentTypes
        )
    {
        require(_exists(tokenId), "Product does not exist");

        uint256 length = products[tokenId].document.length;
        timestamps = new uint256[](length);
        documentHashes = new bytes32[](length);
        documentTypes = new string[](length);

        for (uint256 i = 0; i < length; i++) {
            Document storage doc = products[tokenId].document[i];
            timestamps[i] = doc.timestamp;
            documentHashes[i] = doc.documentHash;
            documentTypes[i] = doc.documentType;
        }

        return (timestamps, documentHashes, documentTypes);
    }

    function getEmissionsBySCP(address scp) public view returns (uint256) {
        return totalEmissionBalances[scp];
    }

    function getProductDetails(
        uint256 tokenId
    )
        public
        view
        returns (
            string memory productId,
            uint256 expirationDate,
            address scp,
            Lifecycle memory lifecycle,
            Document memory document
        )
    {
        require(_exists(tokenId), "Product does not exist");

        Product storage product = products[tokenId];

        return (
            product.productId,
            product.expirationDate,
            product.scp,
            product.lifecycle,
            product.document
        );
    }
}
