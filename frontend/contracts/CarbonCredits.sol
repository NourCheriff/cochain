// SPDX-License-Identifier: MIT
pragma solidity ^0.8.24;

import {ERC20} from "@openzeppelin/contracts/token/ERC20/ERC20.sol";

contract CarbonCredits is ERC20 {
    constructor() ERC20("CarbonCredits", "CC") {
        _mint(msg.sender, 0);
        _mint(address(0xFE3B557E8Fb62b89F4916B721be55cEb828dBd73), 100);
        _mint(address(0x627306090abaB3A6e1400e9345bC60c78a8BEf57), 40);
        _mint(address(0xf17f52151EbEF6C7334FAD080c5704D77216b732), 20);
    }
}
