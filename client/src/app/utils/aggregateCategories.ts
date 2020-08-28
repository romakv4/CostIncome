export const aggregateCategories = (accountingItemsArray) => {
  const result = [];
  const chartItems = accountingItemsArray.reduce((chartItem, accountingItem) => {
    chartItem[accountingItem.category] = (chartItem[accountingItem.category] || 0) + accountingItem.price;
    return chartItem;
  }, {});

  const total = getTotalValue(chartItems);

  for (const category of Object.keys(chartItems)) {
    result.push({
      name: `${category} (${((chartItems[category] / total) * 100).toFixed(2)}%)`,
      value: chartItems[category]
    })
  }

  return result;
}

const getTotalValue = (chartItems) => {
  let totalValue = 0;
  for (const category of Object.keys(chartItems)) {
    totalValue += chartItems[category];
  }
  return totalValue;
}
