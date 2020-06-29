import { aggregateCategories } from './aggregateCategories';

describe('Categories aggregation tests', () => {
  it('Multiple categories aggregate', () => {
    let accountingItemsArray = [
      {
        id: 1,
        category: 'food',
        description: '',
        price: 100,
        date: new Date()
      },
      {
        id: 2,
        category: 'food',
        description: '',
        price: 45,
        date: new Date()
      },
      {
        id: 3,
        category: 'electronic',
        description: 'IPhone 5 SE',
        price: 270,
        date: new Date()
      },
      {
        id: 4,
        category: 'electronic',
        description: 'playstation 4',
        price: 480,
        date: new Date()
      }
    ];

    const result = aggregateCategories(accountingItemsArray);
    expect(result).toEqual([
      {
        name: 'food',
        value: 145
      },
      {
        name: 'electronic',
        value: 750
      }
    ]);
  });
});