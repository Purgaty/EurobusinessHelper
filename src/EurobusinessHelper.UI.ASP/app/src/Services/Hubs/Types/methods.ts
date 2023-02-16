export type BankTransferNotification = (accountTo: string, amount: number) => void;
export type AccountTransferNotification = (accountFrom: string, accountTo: string, amount: number) => void;
export type RequestBankTransferApproval = (accountId: string, amount: number, requestId: string) => void;
