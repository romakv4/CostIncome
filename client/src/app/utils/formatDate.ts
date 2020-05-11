import { AccountingItem } from '../types/AccountingItem';

export const formatDateForTables = (accountingItems) => {
    const result = [];
    for (let item of accountingItems) {
      const tempDate = new Date(item.date);
      result.push({
        id: item.id,
        category: item.category,
        description: item.description,
        price: item.price,
        date: `${tempDate.getFullYear().toString()}.${(tempDate.getMonth() + 1).toString().padStart(2, '0')}.${tempDate.getDate().toString().padStart(2, '0')}`
      })
    }
    return result;
  }

export const formatDateForForms = (accountingItem: AccountingItem) => {
  return {
    id: accountingItem.id,
    category: accountingItem.category,
    description: accountingItem.description,
    price: accountingItem.price,
    date: formatDate(accountingItem.date)
  }
}

export const formatDate = (date) => {
  const tempDate = new Date(date);
  return tempDate.getFullYear().toString() + '-'
  + (tempDate.getMonth() + 1).toString().padStart(2, '0') + '-'
  + tempDate.getDate().toString().padStart(2, '0')
}