export const aggregateCategories = (accountingItemsArray) => {
    const result = [];
    
    let reduced = accountingItemsArray.reduce((c, v) => {
      c[v.category] = (c[v.category] || 0) + v.price;
      return c;
    }, {});

    for (let category of Object.keys(reduced)) {
      result.push({
        name: category,
        value: reduced[category]
      })
    }
    
    return result;
  }