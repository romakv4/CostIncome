export const formatDateForTables = (accountingItems) => {
    const result = [];
    for (let item of accountingItems) {
      const tempDate = new Date(item.date);
      result.push({
        category: item.category,
        description: item.description,
        price: item.price,
        date: `${tempDate.getFullYear().toString()}.${(tempDate.getMonth() + 1).toString().padStart(2, '0')}.${tempDate.getDate().toString().padStart(2, '0')}`
      })
    }
    return result;
  }