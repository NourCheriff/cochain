// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import {ERC721} from "@openzeppelin/contracts/token/ERC721/ERC721.sol";

contract Activity is ERC721 {
    uint256 private emissions;
    mapping(address => uint256) totalEmissionBalances;

    constructor() ERC721("CarbonCredits", "CC") {
        _mint(msg.sender, 0);
    }

    function getEmissionsByActivity() public view returns (uint256) {
        return totalEmissionBalances[msg.sender];
    }
}
