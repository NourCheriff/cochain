// SPDX-License-Identifier: MIT
pragma solidity ^0.8.24;

import {ERC20} from "@openzeppelin/contracts/token/ERC20";

contract CarbonCredits is ERC20 {
    constructor() ERC20("CarbonCredits", "CC") {
        _mint(msg.sender, 0);
    }
}
