// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract Activity is ERC721, Ownable {
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIds;

    struct Activity {
        uint256 timestamp;
        string categoryId;
        address scp;
        uint256 emissions;
    }

    struct Product {
        string productId;
        string creationDate;
        uint256 expirationDate;
        address scp;
        Activity[] lifecycle;
    }

    // Mappatura da tokenId a Product
    mapping(uint256 => Product) public products;
    // Mappatura degli scp autorizzati
    mapping(address => bool) public authorizedSupplyChainPartners;
    // Mappatura delle emissioni totali per attività
    mapping(address => uint256) public totalEmissionBalances;

    event ProductCreated(uint256 tokenId, string productId, address scp);
    event ActivityAdded(uint256 tokenId, string activityType, address scp);
    event SupplyChainPartnerAuthorized(address scp);
    event SupplyChainPartnerRevoked(address scp);

    constructor() ERC721("ProductTracker", "PROD") Ownable(msg.sender) {
        // Il creatore del contratto è autorizzato per default
        authorizedOperators[msg.sender] = true;
    }

    modifier onlyAuthorized() {
        require(authorizedOperators[msg.sender], "Not authorized");
        _;
    }

    function authorizeSupplyChainPartners(address scp) public onlyOwner {
        authorizedSupplyChainPartners[scp] = true;
        emit SupplyChainPartnerAuthorized(scp);
    }

    function revokeSupplyChainPartner(address scp) public onlyOwner {
        authorizedOperators[scp] = false;
        emit SupplyChainPartnerRevoked(scp);
    }

    function createProduct(
        address scp,
        string memory productId,
        uint256 expirationDate
    ) public onlyAuthorized returns (uint256) {
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
        string categoryId,
        address scp,
        uint256 emissions
    ) public onlyAuthorized {
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

    function getActivityByTokenId(
        uint256 tokenId,
    )
        public
        view
        returns (
            uint256 timestamp,
            string memory categoryId,
            address scp,
            uint256 emissions,
        )
    {
        require(_exists(tokenId), "Product does not exist");

        Activity storage activity = products[tokenId];

        return (
            activity.timestamp,
            activity.categoryId,
            activity.scp,
            activity.emissions,
        );
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
            Lifecycle memory lifecycle
        )
    {
        require(_exists(tokenId), "Product does not exist");

        Product storage product = products[tokenId];

        return (
            product.productId,
            product.expirationDate,
            product.scp,
            product.lifecycle
        );
    }

    function isAuthorized(address scp) public view returns (bool) {
      return authorizedSupplyChainPartners[scp];
    }
}
