// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract SmartWalletAccount {
    uint256 totalCarbonCreditsTransferred = 0;
    mapping(address => uint256) carbonCreditsBalances;

    function getCarbonCreditsTransferred() public view returns (uint256) {
        return totalCarbonCreditsTransferred;
    }

    function getCarbonCreditsBalance() public view returns (uint256) {
        return carbonCreditsBalances[msg.sender];
    }

    function transferCarbonCredits(
        address receiverAddress,
        uint256 amount
    ) public {
        require(carbonCreditsBalances[msg.sender] >= amount);
        carbonCreditsBalances[receiverAddress] += amount;
        carbonCreditsBalances[msg.sender] -= amount;
        totalCarbonCreditsTransferred = totalCarbonCreditsTransferred + amount;
    }
}
