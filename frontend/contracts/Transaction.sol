// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract SmartWalletAccount {
    uint256 totalCarbonCreditsTransferred = 0;
    mapping(address => uint256) carbonCreditsBalances;

    function getCarbonCreditsTransferred() public view returns (uint256) {
        return totalCarbonCreditsTransferred;
    }

    function getCarbonCreditsBalance(
        address walletAddress
    ) public view returns (uint256) {
        return carbonCreditsBalances[walletAddress];
    }

    function transferCarbonCredits(address receiverAddress) public payable {
        carbonCreditsBalances[receiverAddress] += msg.value;
        carbonCreditsBalances[msg.sender] -= msg.value;
        totalCarbonCreditsTransferred =
            totalCarbonCreditsTransferred +
            msg.value;
    }
}
