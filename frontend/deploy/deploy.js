module.exports = async function ({ getNamedAccounts, deployments }) {
  const { deploy } = deployments;
  const { deployer } = await getNamedAccounts();

  console.log("Deploying Activity contract...");
  const activityResult = await deploy('Activity', {
    from: deployer,
    args: [],
    log: true,
  });

  console.log("Deploying CarbonCredits contract...");
  const carbonCreditsResult = await deploy('CarbonCredits', {
    from: deployer,
    args: [],
    log: true,
  });

  console.log("Activity deployed to:", activityResult.address);
  console.log("CarbonCredits deployed to:", carbonCreditsResult.address);
};

module.exports.tags = ['all'];
