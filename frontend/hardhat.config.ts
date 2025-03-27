import { HardhatUserConfig } from "hardhat/config";
import "@nomicfoundation/hardhat-ethers";
import "hardhat-deploy";
import * as dotenv from "dotenv";

dotenv.config();

const config: HardhatUserConfig = {
  solidity: {
    version: process.env['SOLIDITY_VERSION']!,
    settings: {
      evmVersion: "london", // required for Besu
    }
  },
  networks: {
    cloud: {
      url: process.env['CLOUD_BLOCKCHAIN_URL'],
      chainId: Number(process.env['CLOUD_CHAIN_ID']),
      gasPrice: 0,
      gas: 0x1ffffffffffffe,
      accounts: [process.env['PRIVATE_KEY_DEPLOYER']!],
    },
    docker: {
      url: process.env['LOCAL_BLOCKCHAIN_URL'],
      chainId: Number(process.env['LOCAL_CHAIN_ID']),
      gasPrice: 0,
      gas: 0x1ffffffffffffe,
      accounts: [process.env['PRIVATE_KEY_DEPLOYER']!],
    },
  },
  namedAccounts: {
    deployer: {
      default: 0,
    },
  },
};

export default config;

