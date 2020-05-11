export type AccountingItem = {
    id: number,
    category: string,
    description?: string,
    price: number,
    date: Date
}

export type OperationSuccess = {
    success: boolean
}