// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract Transaction {
    event Transfer(
        address indexed emitter,
        address indexed receiver,
        uint256 amount
    );

    function transferFunds(address payable receiver) public payable {
        require(msg.value > 0, "Inserisci un importo positivo");
        require(
            receiver != address(0),
            "Indirizzo del destinatario non valido"
        );

        receiver.transfer(msg.value);

        emit Transfer(msg.sender, receiver, msg.value);
    }
}
