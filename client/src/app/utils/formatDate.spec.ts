import {
    formatDateForForms,
    formatDateForTables
} from './formatDate';

describe('Dates formatting', () => {
  it('Format date for forms', () => {
    let accountingItem = {
      id: 1,
      category: 'food',
      description: '',
      price: 100,
      date: new Date('2020-06-29')
    };

    const result = formatDateForForms(accountingItem);
    expect(result).toEqual({
      id: 1,
      category: 'food',
      description: '',
      price: 100,
      date: '2020-06-29'
    });
  });

  it('Format date for tables', () => {
    let accountingItems = [
      {
        id: 1,
        category: 'food',
        description: '',
        price: 100,
        date: new Date('2020-06-29')
      },
      {
        id: 2,
        category: 'food',
        description: '',
        price: 100,
        date: new Date('2020-06-28')
      },
      {
        id: 3,
        category: 'food',
        description: '',
        price: 100,
        date: new Date('2020-05-19')
      }
    ];

    const result = formatDateForTables(accountingItems);
    expect(result).toEqual([
      {
        id: 1,
        category: 'food',
        description: '',
        price: 100,
        date: '2020.06.29'
      },
      {
        id: 2,
        category: 'food',
        description: '',
        price: 100,
        date: '2020.06.28'
      },
      {
        id: 3,
        category: 'food',
        description: '',
        price: 100,
        date: '2020.05.19'
      }
    ])
  });
});